using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff
{
    public string mFaction;//所属门派名称
    public string mName;//buff名字
    public bool isSelected = false;

    public Buff(string faction, string name)
    {
        mFaction = faction;
        mName = name;
    }
}
