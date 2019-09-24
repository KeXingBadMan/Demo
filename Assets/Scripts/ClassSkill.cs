using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public string mFaction;//所属门派名称
    public string mName;//技能名字
    public int mSp;//技能sp
    public int mDamage;//造成伤害

    public virtual void mSpecialEffects()
    {

    }

    public Skill(string faction, string name, int sp, int damage)
    {
        mFaction = faction;
        mName = name;
        mSp = sp;
        mDamage = damage;
    }
}
