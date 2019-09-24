using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//负责整个拼字过程

public class SpellingSceneManager : MonoBehaviour
{
    public List<string> charsInBackPack;//背包里的字（string）
    public List<GameObject> charGOInBackPack;//依据背包里的字（string）生成的字的GameObject
    public List<GameObject> spellingChars;//正在拼写的字
    public GameObject speller;//字符槽，用于获取字符槽位置，判断字符是否被吸附
    public GameObject SingleCharacterPreFab;
    float yPosOffset = 4;//控制第一个字符的位置
    float charHeight = 1.2f;//控制字符间隔；
    int lastSCCount = 0;//last spellingChars count 用于判定spellingChars有没有更新
    int currSCCount = 0;

    // Start is called before the first frame update
    void Start()
    {

        //加载背包里的字

        charsInBackPack.Add("1");
        charsInBackPack.Add("2");
        charsInBackPack.Add("3");
        charsInBackPack.Add("4");
        charsInBackPack.Add("5");
        charsInBackPack.Add("6");
        charsInBackPack.Add("7");

        LoadSingleCharacters(charsInBackPack);
    }

    // Update is called once per frame
    void Update()
    {
        if(charGOInBackPack.Count != 0)//如果背包中有字符，则运行一下function
            SetSpellingCharsOrder(charGOInBackPack);
        if (lastSCCount != currSCCount)//以下functions只在字符槽有变化时运行
        {
            SetSpellingCharsPos();
            CheckNewSkill(spellingChars);
        }
    }

    void LoadSingleCharacters(List<string> charsToLoad)//生成字符GameObject并加入charGOInBackPack
    {
        for(int i = 0; i< charsToLoad.Count; i++)
        {
            charGOInBackPack.Add(Instantiate(SingleCharacterPreFab, new Vector2(5, i * 0.6f), Quaternion.Euler(0,0,0)));
            charGOInBackPack[i].GetComponent<TextMeshPro>().text = charsToLoad[i];
            charGOInBackPack[i].GetComponent<SingleCharacter>().speller = speller;
        }
    }

    void SetSpellingCharsOrder(List<GameObject> charsToLoad)//设定正在拼写的字的上下顺序（spellingChars的顺序）
    {
        lastSCCount = currSCCount;
        for (int i = 0; i < charsToLoad.Count; i++)
        {
            if (charsToLoad[i].GetComponent<SingleCharacter>().spelling &&
                !charsToLoad[i].GetComponent<SingleCharacter>().lastSpelling)
            {
                if (spellingChars.Count != 0 && charsToLoad[i].GetComponent<RectTransform>().position.y < spellingChars[spellingChars.Count-1].GetComponent<RectTransform>().position.y)//字符加在字槽底部
                {
                    spellingChars.Add(charsToLoad[i]);
                }
                else if (InsertInBetween(charsToLoad[i]))//字符加在字槽中间
                {

                }
                else//字符加在字槽顶部
                {
                    spellingChars.Insert(0, charsToLoad[i]);
                }
            }
        }
        for (int j = 0; j< spellingChars.Count; j++)//移除不在拼写的字符
        {
            if (!spellingChars[j].GetComponent<SingleCharacter>().spelling && spellingChars[j].GetComponent<SingleCharacter>().lastSpelling)
            {
                spellingChars.Remove(spellingChars[j]);
            }
        }
        currSCCount = spellingChars.Count;
    }

    bool InsertInBetween(GameObject charToLoad)
    {
        bool retVal = false;
        
        for (int i = 0; i < spellingChars.Count-1; i++)
        {
            if(charToLoad.GetComponent<RectTransform>().position.y < spellingChars[i].GetComponent<RectTransform>().position.y && 
                charToLoad.GetComponent<RectTransform>().position.y > spellingChars[i + 1].GetComponent<RectTransform>().position.y &&
                retVal == false)
            {
                spellingChars.Insert(i + 1, charToLoad);
                retVal = true;
            }
        }
        return retVal;
    }

    void SetSpellingCharsPos()//依据spellingChars的顺序设定字符位置
    {
        for (int i = 0; i< spellingChars.Count; i++)
        {
            spellingChars[i].GetComponent<Transform>().position = new Vector2(transform.position.x, transform.position.y + yPosOffset - i * charHeight);
        }
    }

    int CheckNewSkill(List<GameObject> charsToCheck)//根据字符槽中的技能名称，用switch判定是否有新技能
    {
        int retVal = 0;//默认返回值，返回0说明没有拼出新技能
        string currentName = "";//当前字符槽中的技能名称（将字符槽中的文字转换为string便于判断是否拼字成功）

        for(int i = 0; i < charsToCheck.Count; i++)
        {
            currentName += charsToCheck[i].GetComponent<TextMeshPro>().text;
        }

        switch (currentName)
        {
            case ("1234567"):
                print("获得1234567技能");
                retVal = 1234567;
                break;
            case ("7654321"):
                print("获得7654321技能");
                retVal = 7654321;
                break;
            case ("1234"):
                print("获得1234技能");
                retVal = 1234;
                break;
            case ("567"):
                print("获得567技能");
                retVal = 567;
                break;
        }

        if(retVal != 0)//如果获得了新技能，则删除所有字符槽中的字符
        {
            for(int i = 0; i < spellingChars.Count; i++)
            {
                charGOInBackPack.Remove(spellingChars[i]);
                Destroy(spellingChars[i]);
            }
            spellingChars.Clear();
        }

        return retVal;//retVal为新获得的技能编号
    }
}
