using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharsGenerator : MonoBehaviour
{
    public int enemyLevel = 2;// 1,2,3对应普通，精英，boss怪
    public List<string> CharsDropped;
    public List<string> Lv1Chars;//所有1级字符
    public List<string> Lv2Chars;//2级
    public List<string> Lv3Chars;//3级
    public GameObject droppedCharacterPreFab;
    int charsCount;
    int charLevel;
    int Lv1;
    int Lv2;
    int Lv3;

    // Start is called before the first frame update
    void Start()
    {
        Lv1Chars = new List<string> { "1A", "1B", "1C", "1D", "1F" };//所有1级字符
        Lv2Chars = new List<string> { "2A", "2B", "2C", "2D", "2F" };//2级
        Lv3Chars = new List<string> { "3A", "3B", "3C", "3D", "3F" };//3级

        LoadSingleCharacters();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadSingleCharacters()
    {
        List<string> tempList = GenerateChars();//生成并储存所有字符string

        //生成字符GameObject并加力，实现井喷效果
        for (int i = 0; i < tempList.Count; i++)
        {
            GameObject tempGO = Instantiate(droppedCharacterPreFab);
            tempGO.GetComponent<TextMeshPro>().text = tempList[i];
            tempGO.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3,3), Random.Range(7, 12)), ForceMode2D.Impulse);
        }
    }

    //生成所有字符string
    List<string> GenerateChars()
    {
        CharsDropped = new List<string> { };

        //根据敌人等级判定生成字符数量和不同字符等级概率
        switch (enemyLevel)
        {
            case 1:
                charsCount = Random.Range(5, 10);
                Lv1 = 70;
                Lv2 = 20;
                Lv3 = 10;
                break;
            case 2:
                Lv1 = 30;
                Lv2 = 35;
                Lv3 = 35;
                charsCount = Random.Range(5, 10);
                break;
            case 3:
                Lv1 = 20;
                Lv2 = 35;
                Lv3 = 45;
                charsCount = Random.Range(8, 10);
                break;
        }

        //生成单个字符
        for (int i = 0; i < charsCount; i++) {

            int j = Random.Range(0, 100);//随机字符等级
            string temp = "null";//temp为生成的字符

            //字符等级判定
            if(enemyLevel == 3 && i < 2)//怪物等级为3（boss）时的前三个字符等级判定
            {
                if (j < 20)
                    charLevel = 1;
                else if (j > 20 && j < 40)
                    charLevel = 2;
                else
                    charLevel = 3;
            }
            else//怪物等级为2/1（精英/普通）时的字符等级判定
            {   
                if (j < Lv1)
                    charLevel = 1;
                else if (j > Lv1 && j < Lv1 + Lv2)
                    charLevel = 2;
                else
                    charLevel = 3;
            }

            //依据字符等级选择具体字符
            switch (charLevel)
            {
                case 1:
                    temp = Lv1Chars[Random.Range(0,Lv1Chars.Count)];
                    break;
                case 2:
                    temp = Lv2Chars[Random.Range(0, Lv2Chars.Count)];
                    break;
                case 3:
                    temp = Lv3Chars[Random.Range(0, Lv3Chars.Count)];
                    break;
            }

            //添加字符
            CharsDropped.Add(temp);
        }

        return CharsDropped;
    }

}
