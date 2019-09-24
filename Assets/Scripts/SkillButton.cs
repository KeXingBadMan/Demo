using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    public Skill mySkill;
    public BattleSceneManager sceneManager;

    //被点击后，执行IAmUsed
    public void IAmUsed()
    {
        sceneManager.usedSkill = mySkill;
        sceneManager.playerMoved = true;
        print("clickeddededadfasdfasdf");
    }
}
