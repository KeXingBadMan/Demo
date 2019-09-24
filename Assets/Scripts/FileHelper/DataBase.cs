using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;
using System;


public class DataBase : MonoBehaviour
{

    private JsonData itemData;
    private JsonData skillData;
    private JsonData skills;
    private JsonData items;
    private List<Item> itemDatabase = new List<Item>();
    private List<CSkill> skillDatabase = new List<CSkill>();
    private List<int> skillLearned = new List<int>();
    private List<int> itemHad = new List<int>();
    //private StringBuilder sb = new StringBuilder();
    

    // Start is called before the first frame update
    void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Scripts/FileHelper/Files/item.json"));
        skillData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Scripts/FileHelper/Files/skill.json"));
        items = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Scripts/FileHelper/Files/itemwardrobe.json"));
        skills = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Scripts/FileHelper/Files/skillwardrobe.json"));
        ConstructDatabase();
        //Debug.Log(itemDatabase[0].Name);
        //Debug.Log(skillDatabase[0].Detail);
        //AddSkill(4006);
        AddSkill(3004);
        AddSkill(2001);
        //AddSkill(1000);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ConstructDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            itemDatabase.Add(new Item((int)itemData[i]["id"], itemData[i]["name"].ToString(), (int)itemData[i]["level"], itemData[i]["detail"].ToString(),
                (int)itemData[i]["compose_num"], (int)itemData[i]["decompose_num"], itemData[i]["path"].ToString()));
        }

        for (int i = 0; i < skillData.Count; i++)
        {
            skillDatabase.Add(new CSkill((int)skillData[i]["id"], skillData[i]["name"].ToString(), (int)skillData[i]["type"], skillData[i]["detail_bg"].ToString(),
               skillData[i]["detail_desc"].ToString(),(int)skillData[i]["sect"], (int)skillData[i]["sp_consump"],skillData[i]["path"].ToString(),(int)skillData[i]["level"],
               (int)skillData[i]["damage"],(int)skillData[i]["sp_add"]));
        }

        if (PlayerPrefs.HasKey("items"))
        {
            string itemsHad = PlayerPrefs.GetString("items");
            string[] itemsArray = itemsHad.Split(new string[] { "," },  StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in itemsArray)
            {
                itemHad.Add(int.Parse(item));
            }
        }

        if (PlayerPrefs.HasKey("skills"))
        {
            string skillsHad = PlayerPrefs.GetString("skills");
            string[] skillsArray = skillsHad.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string skill in skillsArray)
            {
                itemHad.Add(int.Parse(skill));
            }
        }

    }

    public List<Item> GetItemData()//得到所有的物品数据
    {
        return itemDatabase;
    }

    public List<CSkill> GetSkillData() // 得到所有的技能数据
    {
        return skillDatabase;
    }

    public List<int> GetItemHad()   //得到已经获得的技能ID List
    {
        return itemHad;
    }

    public List<int> GetSkillHad()   //得到已经获得的技能ID List
    {
        return skillLearned;
    }

    public void AddItem(int _itemId)
    {
        itemHad.Add(_itemId);
        string old_items;
        if (PlayerPrefs.HasKey("items"))
        {
            old_items = PlayerPrefs.GetString("items");
        }
        else
        {
            old_items = "";
        }
        string new_items = old_items  + _itemId + ",";
        PlayerPrefs.SetString("items", new_items);
    }

    public void AddSkill(int _skillId)
    {
        skillLearned.Add(_skillId);
        string old_skills;
        if (PlayerPrefs.HasKey("skills"))
        {
            old_skills = PlayerPrefs.GetString("items");
        }
        else
        {
            old_skills = "";
        }
        string new_skills = old_skills + _skillId + ",";
        PlayerPrefs.SetString("skills", new_skills);
    }

    public Item FetchItemDataByID(int _id)
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            if (_id == itemDatabase[i].ID)
                return itemDatabase[i];
        }
        return null;
    }

    public CSkill FetchSkillDataByID(int _id)
    {
        for (int i = 0; i < skillData.Count; i++)
        {
            if (_id == skillDatabase[i].ID)
                return skillDatabase[i];
        }
        return null;
    }

    public void RemoveItemById(int _itemId)
    {
        foreach (int itemToRemove in itemHad)
        {
            if (itemToRemove == _itemId)
            {
                itemHad.Remove(itemToRemove);
                break;
            }
        }
        string new_item = "";
        foreach (int itemToRemove in itemHad)
        {
            new_item += itemToRemove + ",";
        }
        PlayerPrefs.SetString("items", new_item);
    }

    public void ClearAllSave()
    {
        PlayerPrefs.DeleteKey("skills");
        PlayerPrefs.DeleteKey("items");
    }
}


public class Item
{

    public int ID { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public string Detail { get; set; }
    public int ComposeNum { get; set; }
    public int DecomposeNum { get; set; }
    public string Path { get; set; }

    public Item(int _id, string _name, int _level, string _detail, int _composenum, int _decomposenum, string _path)
    {
        this.ID = _id;
        this.Name = _name;
        this.Level = _level;
        this.Detail = _detail;
        this.ComposeNum = _composenum;
        this.DecomposeNum = _decomposenum;
        this.Path = _path;
    }

    public Item() {
        this.ID = -1;
    }
}

public class CSkill
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public string Detail_BG { get; set; }
    public string Detail_DEC { get; set; }
    public int Sect { get; set; }
    public int SpCost { get; set; }
    public string Path { get; set; }
    public int Level { get; set; }
    public int Damage { get; set; }
    public int Sp_Add { get; set; }

    public CSkill(int _id, string _name, int _type, string _detail_bg, string _detail_dec, int _sect, int _sp_consumption, string _path , int _level, int _damage, int _sp_add)
    {
        this.ID = _id;
        this.Name = _name;
        this.Type = _type;
        this.Detail_BG = _detail_bg;
        this.Detail_DEC = _detail_dec;
        this.Sect= _sect;
        this.SpCost = _sp_consumption;
        this.Path = _path;
        this.Level = _level;
        this.Damage = _damage;
        this.Sp_Add = _sp_add;
    }

    public CSkill() {
        this.ID = -1;
    }
}