using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUI : UIBase
{
    public override void DoOnEntering()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        gameObject.SetActive(true);
        RandomMonster();
    }

    public override void DoOnPausing()
    {
        gameObject.SetActive(false);
    }

    public override void DoOnResuming()
    {
        gameObject.SetActive(true);
    }

    public override void DoOnExiting()
    {
        gameObject.SetActive(false);
    }

    public void GotoStart()
    {
        UIManager.Instance.PopUIPanel();
    }

    public void RandomMonster()
    {
        GameObject randommoster = GameObject.Find("RandomMonster");
        if(Random.Range(1,11) < 6)
        {
            randommoster.SetActive(true);
        }
        else
        {
            randommoster.SetActive(false);
        }
    }



}
