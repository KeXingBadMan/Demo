using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : UIBase
{
    public override void DoOnEntering()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        gameObject.SetActive(true);

        //GameObject Pause = gameObject.transform.Find("Pause").gameObject;
        //Pause.Find("Pause");
        //Debug.Log("Set Pause Active");
        SoundManager.Instance.PlayBGM("打击战斗");
    }

    public override void DoOnPausing()
    {
        Time.timeScale = 0;
        //gameObject.SetActive(false);
    }

    public override void DoOnResuming()
    {
        Time.timeScale = 1f;
        //gameObject.SetActive(true);
    }

    public override void DoOnExiting()
    {
        SoundManager.Instance.PlayBGM("少年游");
        gameObject.SetActive(false);
    }

    public void ExitBattle()
    {
        Time.timeScale = 1f;
        //Debug.Log("PopUI");
        UIManager.Instance.PopUIPanel();
    }
}
