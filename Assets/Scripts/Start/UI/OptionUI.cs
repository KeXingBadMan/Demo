using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : UIBase
{
    public Slider MusicSlider;
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
        //Debug.Log("GotoStart");
        UIManager.Instance.PopUIPanel();
    }

    public void SetBGMMute(bool mute)
    {
        SoundManager.Instance.Mute = mute;
    }
    public void SetBGMVolume()
    {
        SoundManager.Instance.BGMVolume = MusicSlider.value;
    }

}
