using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public int maxHP = 100;//最大血量
    public int currentHP = 100;//当前血量
    public int maxSP = 100;//最大能量值
    public int currentSP = 0;//当前能量值
    RectTransform HPTransform;
    RectTransform SPTransform;
    private float HPwidth; //记录血量宽度
    private float SPwidth; //记录能量宽度

    // Start is called before the first frame update
    void Start()
    {
        GameObject HP = GameObject.Find("HP/HP_pros");
        HPTransform = HP.GetComponent<RectTransform>();
        HPwidth = HPTransform.sizeDelta.x;
        GameObject SP = GameObject.Find("SP/SP_pros");
        SPTransform = SP.GetComponent<RectTransform>();
        SPwidth = SPTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }
        if (currentHP <= 0)
        {
            currentHP = 0;
        }
        currentHP--;
        Debug.Log(currentHP);

        HPTransform.sizeDelta = new Vector2(HPwidth / maxHP * currentHP, HPTransform.sizeDelta.y);

        if (currentSP >= maxSP)
        {
            currentSP = maxSP;
        }
        if (currentSP <= 0)
        {
            currentSP = 0;
        }
        SPTransform.sizeDelta = new Vector2(SPwidth / maxSP * currentSP, SPTransform.sizeDelta.y);

    }
}
