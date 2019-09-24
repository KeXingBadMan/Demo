using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleSceneManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject skillBPrefab;//skill button prefab
    public GameObject buffBPrefab;//buff button prefab
    public GameObject confirmButton;
    public GameObject player;
    public GameObject enemy;
    public List<Skill> playerSkills;
    public List<Buff> playerBuffs;
    public List<GameObject> playerSkillButtons;
    public List<GameObject> playerBuffButtons;
    public int playerSP = 0;
    public int playerHP = 8;
    public int playerMaxHP;
    public int enemySP = 0;
    public int enemyHP = 15;
    public bool playerTurn = true;
    public Skill usedSkill = null;//玩家选择的技能
    public bool battleIsOver = false;
    public bool playerMoved = false;//玩家是否有选择技能；false：玩家尚未在该回合选择技能；true：玩家已选择技能。玩家点击技能按钮后会更改playerMoved 和usedSkill (参见SkillButton)
    public bool pickingBuff = true;
    bool lastPlayerTurn = false;

    Skill enemySkill1;
    Skill enemySkill2;

    // Start is called before the first frame update
    void Start()
    {
        playerSkills.Add(new Skill("武当派", "Basic", 0, 3));
        playerSkills.Add(new Skill("武当派", "BaGua", 3, 7));
        playerSkills.Add(new Skill("武当派", "TaiJi", 10, 30));
        playerSkills.Add(new Skill("武当派", "ChangQuan", 2, 5));
        playerSkills.Add(new Skill("", "Skip", 0, 0));

        playerBuffs.Add(new Buff("武当派", "HunYuan"));//战斗开始时执行的buff
        playerBuffs.Add(new Buff("武当派", "TaiJiXinFa"));//回合结束时执行的buff
        playerBuffs.Add(new Buff("武当派", "YiJinJing"));//回合开始时执行的buff

        InitializeBuffScene();
    }

    void InitializeBuffScene()
    {
        //生成buff按钮
        for (int i = 0; i < playerBuffs.Count; i++)
        {
            GameObject tempBuff = Instantiate(buffBPrefab);
            tempBuff.transform.parent = gameObject.transform;
            tempBuff.GetComponentInChildren<TextMeshProUGUI>().text = playerBuffs[i].mName;
            tempBuff.GetComponent<BuffButton>().myBuff = playerBuffs[i];
            tempBuff.GetComponent<BuffButton>().sceneManager = this;
            tempBuff.transform.position = new Vector2(500, i * 30 + 100);
            playerBuffButtons.Add(tempBuff);
        }
    }

    void InitializeBattleScene()//生成玩家敌人和技能按钮
    {
        playerMaxHP = playerHP;

        BuffOnBattleStart();//判定战斗开始时生效的buff

        //生成玩家与敌人
        player = Instantiate(playerPrefab, gameObject.transform);
        enemy = Instantiate(enemyPrefab, gameObject.transform);
        //生成玩家技能按钮
        for (int i = 0; i < playerSkills.Count; i++)
        {
            GameObject tempSkill = Instantiate(skillBPrefab);
            tempSkill.transform.parent = gameObject.transform;
            tempSkill.GetComponentInChildren<TextMeshProUGUI>().text = playerSkills[i].mName;
            tempSkill.GetComponent<SkillButton>().mySkill = playerSkills[i];
            tempSkill.GetComponent<SkillButton>().sceneManager = this;
            tempSkill.transform.position = new Vector2(300, i * 30 + 100);
            print("new skill added");
            playerSkillButtons.Add(tempSkill);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //玩家完成buff选择后正式开始战斗
        if (!pickingBuff)
        {
            if (playerTurn && !battleIsOver)
                PlayerTurn();
            else if (!playerTurn && !battleIsOver)
                EnemyTurn();
        }
        
    }

    void PlayerTurn()//先更新玩家状态（增加sp）， 之后依据sp判定可用技能，最后依据玩家选择的技能更新玩家和怪物数据。
    {
        print("player turn starts");



        if (!lastPlayerTurn && playerTurn)//确保每次运行playerturn时，一下内容只运行一次
        {
            lastPlayerTurn = playerTurn;
            print("玩家回合>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            playerSP++;
            playerMoved = false;
            BuffOnStart();//判定回合开始时生效的buff
        }

        //判定技能是否可用
        foreach (GameObject button in playerSkillButtons)
        {
            if (button.GetComponent<SkillButton>().mySkill.mSp <= playerSP)
                button.GetComponent<UnityEngine.UI.Button>().interactable = true;
            else
                button.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        //释放技能
        if (playerMoved)
        {
            int actualDamage = usedSkill.mDamage;//实际对敌人造成的伤害；根据buff更改实际伤害

            BuffOnAttack();//判定攻击时生效的buff

            playerSP -= usedSkill.mSp;
            enemyHP -= actualDamage;
            CastSpecialEffect(usedSkill.mName);//特殊效果
            Debug.Log("玩家使用技能" + usedSkill.mName + "小怪-" + usedSkill.mDamage);

            playerHP += 4;

            SetBattleIsOver();

            BuffOnFinish();//判定回合结束时生效的buff

            playerTurn = false;

            lastPlayerTurn = false;

            foreach (GameObject button in playerSkillButtons)
                button.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }

    void EnemyTurn()
    {
        enemySP++;

        print("怪物回合<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");


        //判定使用的技能
        if (enemySP >= 2)
        {
            playerHP -= 6;
            enemySP -= 2;
            Debug.Log("小怪使用2sp技能， 玩家-4hp");
        }
        else
        {
            playerHP -= 2;
            Debug.Log("小怪使用0sp技能， 玩家-2hp");
        }


        playerTurn = true;

        SetBattleIsOver();

    }


    //依据hp判定战斗是否结束
    void SetBattleIsOver()
    {
        if (enemyHP < 0 || playerHP < 0)
        {
            battleIsOver = true;
            Debug.Log("战斗结束");

        }
        else
        {
            Debug.Log("战斗继续");
        }
    }


    //施放特殊效果。在switch的case中添加特殊效果代码。该function在每次玩家释放技能后运行
    void CastSpecialEffect(string skillName)
    {
        switch (skillName)
        {
            case "BaGua":
                //八卦掌的特殊效果ToDo
                break;
            case "TaiJi":
                //太极拳的特殊效果ToDo
                break;
            case "Skip":
                playerTurn = false;//跳过的特殊效果：直接结束玩家回合
                break;
        }
    }

    void BuffOnStart()
    {
        foreach (Buff tempBuff in playerBuffs)
        {
            switch (tempBuff.mName)
            {
                case "YiJinJing":
                    break;
            }
        }
    }

    //玩家点击confirm按钮后结束buff选择阶段
    public void ConfirmBuff()
    {
        pickingBuff = false;
        InitializeBattleScene();
        Destroy(confirmButton);
    }

    //以下为buff判定。根据buff生效时间和buff名字进行判定
    void BuffOnAttack()
    {
        foreach (Buff tempBuff in playerBuffs)
        {
            switch (tempBuff.mName)
            {
                case "buf名字":
                    break;
            }
        }
    }

    void BuffOnFinish()
    {
        foreach (Buff tempBuff in playerBuffs)
        {
            switch (tempBuff.mName)
            {
                case "TaiJiXinFa":
                    break;
            }
        }
    }

    void BuffOnBattleStart()
    {
        foreach (Buff tempBuff in playerBuffs)
        {
            switch (tempBuff.mName)
            {
                case "HunYuan":
                    playerMaxHP += 10;
                    break;
            }
        }
    }
}
