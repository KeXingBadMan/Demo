using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public List<Skill> playerBuffSkills;
    public List<Buff> playerBuffs;
    public List<GameObject> playerSkillButtons;
    public List<GameObject> playerBuffButtons;
    public int playerSP = 0;
    public int playerHP = 20;
    public int playerMaxHP = 20;
    public int enemySP = 0;
    public int enemyHP = 15;
    public bool playerTurn = true;
    public Skill usedSkill = null;//玩家选择的技能
    public bool battleIsOver = false;
    public bool playerMoved = false;//玩家是否有选择技能；false：玩家尚未在该回合选择技能；true：玩家已选择技能。玩家点击技能按钮后会更改playerMoved 和usedSkill (参见SkillButton)
    public bool pickingBuff = true;
    public GameObject dataBaseObj = null;
    private DataBase dataBase = null;
    bool lastPlayerTurn = false;
    private bool HoldUpByUI = false;
    List<Item> charDic = new List<Item>();
    List<Item> charLevel1 = new List<Item>();
    List<Item> charLevel2 = new List<Item>();
    List<Item> charLevel3 = new List<Item>();
    List<Item> charLevel4 = new List<Item>();

    Skill enemySkill1;
    Skill enemySkill2;

    // Start is called before the first frame update
    void Start()
    {
        //dataBaseObj = GameObject.Find("DataBase");
        dataBase = dataBaseObj.GetComponent<DataBase>();
        //Debug.Log(dataBase.GetSkillData()[1].Detail_DEC);
        InitSkills();
        //playerSkills.Add(new Skill("武当派", "Basic", 0, 3));
        //playerSkills.Add(new Skill("武当派", "BaGua", 3, 7));
        //playerSkills.Add(new Skill("武当派", "TaiJi", 10, 30));
       // playerSkills.Add(new Skill("武当派", "ChangQuan", 2, 5));
      //  playerSkills.Add(new Skill("", "Skip", 0, 0));

        playerBuffs.Add(new Buff("武当派", "HunYuan"));//战斗开始时执行的buff
        playerBuffs.Add(new Buff("武当派", "TaiJiXinFa"));//回合结束时执行的buff
        playerBuffs.Add(new Buff("武当派", "YiJinJing"));//回合开始时执行的buff
        InitializeBattleScene();
        InitializePlayerAndEnemy();
        IntDic();
        //InitializeBuffScene();
    }
    void IntDic()
    {
        charDic = dataBase.GetItemData();
        foreach (Item tempChar in charDic)
        {
            if (tempChar.Level == 1)
                charLevel1.Add(tempChar);
            else if (tempChar.Level == 2)
                charLevel2.Add(tempChar);
            else if (tempChar.Level == 3)
                charLevel3.Add(tempChar);
            else if (tempChar.Level == 4)
                charLevel4.Add(tempChar);
        }
    }
    void InitSkills() {
        playerSkills.Add(new Skill("武当派", "普通攻击", 0, 3,0));
        playerSkills.Add(new Skill("", "跳过", 0, 0,0));
        List<int> skillsLearned = dataBase.GetSkillHad();
        List<CSkill> SkillData = dataBase.GetSkillData();
       // List<Item> ItemData = dataBase.GetItemData();

        //Debug.Log("uuuu " + ItemData[1].Name);
        for (int i = 0; i < skillsLearned.Count; i++) {
            foreach (CSkill tempSkill in SkillData) {
                Debug.Log("uuuu " + tempSkill.Name);
                if (skillsLearned[i] == tempSkill.ID && tempSkill.Type == 1) {
                    playerSkills.Add(new Skill("武当派", tempSkill.Name, tempSkill.SpCost, tempSkill.Damage,tempSkill.Sp_Add));
                }
                if (skillsLearned[i] == tempSkill.ID && tempSkill.Type == 2)
                {
                    playerBuffSkills.Add(new Skill("武当派", tempSkill.Name, tempSkill.SpCost, tempSkill.Damage, tempSkill.Sp_Add));
                }
            }
        }
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

    void InitializePlayerAndEnemy()
    {
       // if (PlayerPrefs.HasKey("PlayerHp")) {
       //     playerHP = PlayerPrefs.GetInt("PlayerHp");
       // }
       
        if (PlayerPrefs.HasKey("EnemyLevel")) {
            int enemyLevel = PlayerPrefs.GetInt("EnemyLevel");
            if (enemyLevel == 1)
            {
                enemyHP = 15;
            }
            else if (enemyLevel == 2)
            {
                enemyHP = 20;
            }
            else {
                enemyHP = 30;
            }
        }
        //敵人的相關設定
    }

    void InitializeBattleScene()//生成玩家敌人和技能按钮
    {
        Destroy(confirmButton);
        pickingBuff = false;
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
            //tempSkill.GetComponentInChildren<TextMeshProUGUI>().text = playerSkills[i].mName;
            tempSkill.GetComponentInChildren<Text>().text= playerSkills[i].mName;
            tempSkill.GetComponent<SkillButton>().mySkill = playerSkills[i];
            tempSkill.GetComponent<SkillButton>().sceneManager = this;
            tempSkill.transform.position = new Vector2(900, i * 30 + 100);
            print("new skill added");
            playerSkillButtons.Add(tempSkill);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //玩家完成buff选择后正式开始战斗
        if (!pickingBuff&&HoldUpByUI == false)
        {
            if (playerTurn && !battleIsOver)
                PlayerTurn();
            else if (!playerTurn && !battleIsOver)
                EnemyTurn();
        }
        
    }

    void PlayerTurn()//先更新玩家状态（增加sp）， 之后依据sp判定可用技能，最后依据玩家选择的技能更新玩家和怪物数据。
    {
        //print("player turn starts");



        if (!lastPlayerTurn && playerTurn)//确保每次运行playerturn时，一下内容只运行一次
        {
            foreach(Skill tempbuff in playerBuffSkills)
            {
                if (tempbuff.mSpAdd > 0)
                {
                    playerSP += tempbuff.mSpAdd;
                }
            }
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
            playerSP += usedSkill.mSpAdd;
            enemyHP -= actualDamage;
            CastSpecialEffect(usedSkill.mName);//特殊效果
            Debug.Log("玩家使用技能" + usedSkill.mName + "小怪-" + usedSkill.mDamage);
            HoldUpByUI = true;
            BattleUIManager.instance.ConfirmButton.onClick.AddListener(delegate() {
                PlayerRoundClick();
            });
            string message = "玩家使用技能 " + usedSkill.mName + " 敌人失去" + usedSkill.mDamage+"点血";
            BattleUIManager.instance.ShowDialogUI(message);
            //playerHP += 4;

            //SetBattleIsOver();

            //BuffOnFinish();//判定回合结束时生效的buff

            //playerTurn = false;

            // lastPlayerTurn = false;

            //foreach (GameObject button in playerSkillButtons)
            //  button.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }
    void PlayerRoundClick()
    {
        if (playerTurn == true) {
            
            PlayerRoundOver();
        }
            
    }

    void PlayerRoundOver() //玩家回合结算
    {
        

        BuffOnFinish();//判定回合结束时生效的buff

        playerTurn = false;

        lastPlayerTurn = false;

        foreach (GameObject button in playerSkillButtons)
            button.GetComponent<UnityEngine.UI.Button>().interactable = false;
        BattleUIManager.instance.ConfirmButton.onClick.RemoveAllListeners();
        BattleUIManager.instance.CloseDialogUI();
        HoldUpByUI = false;
        SetBattleIsOver();
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
            Debug.Log("小怪使用2sp技能， 玩家-6hp");
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
            if (enemyHP < 0)
            {
                playerHP += 5;
                if (playerHP > playerMaxHP) {
                    playerHP = playerMaxHP;
                }
                PlayerPrefs.SetInt("PlayerHp", playerHP);
                List<Item> reward = GetWinReward();
                string message = "战斗胜利！   恭喜获得：";
                foreach (Item temp in reward)
                {
                    dataBase.AddItem(temp.ID);
                    message += temp.Name + ",";
                    //Debug.Log("congratulations");
                    //Debug.Log(temp.Name);
                }
                BattleUIManager.instance.ShowDialogUI(message);
                Debug.Log("结算UI");
                BattleUIManager.instance.ConfirmButton.onClick.AddListener(delegate ()
                {
                    BattleWinOnClick();
                }
                   );
                //win  恭喜獲得
            }
            else {
                List<string> makeup = GetDeathMakeup();
                foreach(string temp in makeup)
                {
                    //dataBase.AddItem(temp);
                    Debug.Log("congratulations");
                    Debug.Log(temp);
                }
                //lose
            }
            Debug.Log("战斗结束");
            //
            //SceneManager.LoadScene("Start");
            
        }
        else
        {
            Debug.Log("战斗继续");
        }
    }

    void BattleWinOnClick()
    {
        BattleUIManager.instance.ConfirmButton.onClick.RemoveAllListeners();
        BattleUIManager.instance.CloseDialogUI();
        SceneManager.LoadScene("Play");
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

    List<Item> GetWinReward() {
        int charNum = 5;
        int extra = Random.Range(0, 5);
        List<Item> reward = new List<Item>();
        charNum += extra;
        int enemyLevel = 1;
        int level2SpecialNum = 2;
        int level3SpecialNum = 3;
        if (PlayerPrefs.HasKey("EnemyLevel"))
        {
            enemyLevel = PlayerPrefs.GetInt("EnemyLevel");
        }
        if (enemyLevel == 3) {
            charNum = 8;
            extra = Random.Range(0, 2);
            charNum += extra;
        }
        for (int i = 0; i < charNum; i++) {
            int level;
            if (enemyLevel == 2 && level2SpecialNum > 0)
            {
                level = GetLevel2Special();
                level2SpecialNum -= 1;
            }
            else if (enemyLevel == 3 && level3SpecialNum > 0)
            {
                level = GetLevel3Special();
                level3SpecialNum -= 1;
            }
            else
            {
                level = GetLevelNorm(enemyHP);
            }
            reward.Add(SelectCharByLevel(level));
        }
        return reward;
    }

    Item SelectCharByLevel(int _level)
    {
        Item rewardChar = new Item();
        int index;
        switch (_level)
        {
            case 1:
                    index = Random.Range(0, charLevel1.Count - 1);
                    rewardChar = charLevel1[index];
                    break;
            case 2:
                    index = Random.Range(0, charLevel2.Count - 1);
                    rewardChar = charLevel2[index];
                    break;
            case 3:
                    index = Random.Range(0, charLevel3.Count - 1);
                    rewardChar = charLevel3[index];
                    break;
            case 4:
                    index = Random.Range(0, charLevel4.Count - 1);
                    rewardChar = charLevel4[index];
                    break;
        }
        return rewardChar;
    }

    int GetLevelNorm(int _enemyLevel) {
        int rateSelect = Random.Range(1,100);
        int ans = 1;
        switch (_enemyLevel) {
            case 1:
                if (rateSelect < 70)
                {
                    ans = 1;
                }
                else if (rateSelect >=  70 && rateSelect < 85)
                {
                    ans = 2;
                }
                else if (rateSelect >= 85 && rateSelect < 95)
                {
                    ans = 3;
                }
                else
                {
                    ans = 4;
                }
                break;
            case 2:
                if (rateSelect < 15)
                {
                    ans = 1;
                }
                else if (rateSelect >= 15 && rateSelect < 50)
                {
                    ans = 2;
                }
                else if (rateSelect >= 50 && rateSelect < 85)
                {
                    ans = 3;
                }
                else
                {
                    ans = 4;
                }
                break;
            case 3:
                if (rateSelect < 10)
                {
                    ans = 1;
                }
                else if (rateSelect >= 10 && rateSelect < 40)
                {
                    ans = 2;
                }
                else if (rateSelect >= 40 && rateSelect < 80)
                {
                    ans = 3;
                }
                else
                {
                    ans = 4;
                }
                break;
        }
        return ans;
    }
    int GetLevel2Special() {
        int rateSelect = Random.Range(1, 100);
        int ans = 1;
        if (rateSelect < 20)
        {
            ans = 1;
        }
        else if (rateSelect >= 20 && rateSelect < 90)
        {
            ans = 2;
        }
        else if (rateSelect >= 90 && rateSelect < 100)
        {
            ans = 3;
        }
        else
        {
            ans = 4;
        }
        return ans;
    }

    int GetLevel3Special()
    {
        int rateSelect = Random.Range(1, 100);
        int ans = 1;
        if (rateSelect < 20)
        {
            ans = 1;
        }
        else if (rateSelect >= 20 && rateSelect < 40)
        {
            ans = 2;
        }
        else if (rateSelect >= 40 && rateSelect < 100)
        {
            ans = 3;
        }
        else
        {
            ans = 4;
        }
        return ans;
    }

    List<string> GetDeathMakeup()
    {
        List<string> makeup = new List<string>();
        foreach (int tempId in dataBase.GetSkillHad())
        {
            foreach (CSkill tempSkill in dataBase.GetSkillData())
            {
                if (tempSkill.ID == tempId)
                {
                    foreach (char tempChar in tempSkill.Name)
                    {
                        string temp = tempChar.ToString();
                        makeup.Add(temp);
                    }
                }
            }
        }
        foreach (int charId in dataBase.GetItemHad())
        {
            foreach (Item tempItem in charDic)
            {
                if (tempItem.ID == charId)
                {
                    string tmp = tempItem.Name;
                    makeup.Add(tmp);
                }
             }         
        }
        List<string> tempMakeup = new List<string>();
        int makeupNum = 3;
        if (makeupNum > makeup.Count) {
            makeupNum = makeup.Count;
        }
        for (int i = 0; i < makeupNum; i++) {
            int index = Random.Range(0, makeup.Count - 1);
            tempMakeup.Add(makeup[index]);
            makeup.Remove(makeup[index]);
        }
        dataBase.ClearAllSave();
        foreach (string itemName in tempMakeup)
        {
            dataBase.AddItemByName(itemName);
        }
        return tempMakeup;
    }
}
