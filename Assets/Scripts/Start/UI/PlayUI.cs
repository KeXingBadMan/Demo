using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUI : UIBase
{
    public override void DoOnEntering()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        gameObject.SetActive(true);
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

}
