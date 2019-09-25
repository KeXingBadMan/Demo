using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : UIBase
{
    public override void DoOnEntering()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        //SoundManager.Instance.PlayBGM("少年游");
        gameObject.SetActive(true);
        Debug.Log("StartUI Enter");
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
        //Destroy(gameObject, 0);
        gameObject.SetActive(false);
    }

    public void GotoOption()
    {
        UIManager.Instance.PushUIPanel("OptionUI");
    }

    public void GotoNewGame()
    {
        UIManager.Instance.PopUIPanel();
        SceneManager.LoadScene("Play");
        //string Map = RandomMap();
        //UIManager.Instance.PushUIPanel(Map);
    }

    public void GotoContinue()
    {
        //UIManager.Instance.PushUIPanel("Map1-2");
    }

    public void ExitGame()
    {
        //Debug.Log("exit!!!!!");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
             Application.Quit();
        #endif
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
