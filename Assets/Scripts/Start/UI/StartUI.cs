using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : UIBase
{
    public override void DoOnEntering()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        SoundManager.Instance.PlayBGM("少年游");
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
        //Debug.Log("exit!!!!!");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void GotoOption()
    {
        UIManager.Instance.PushUIPanel("OptionUI");
    }

    public void GotoNewGame()
    {
        //SceneManager.LoadScene("MapPlay");
        string Map = RandomMap();
        UIManager.Instance.PushUIPanel(Map);
    }

    public void GotoContinue()
    {
        UIManager.Instance.PushUIPanel("Map1-2");
        //UIManager.Instance.PushUIPanel("Level2");
    }

    public string RandomMap()
    {
        string Map = "Map1-";

        //Random.Range(min,max)返回一个随机整数，在min(包含)和max(不包含)之间
        int MapNum = Random.Range(1, 3);
        Debug.Log(MapNum);
        Map = Map + MapNum;
        Debug.Log(Map);
        return Map;
    }
}
