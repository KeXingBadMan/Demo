using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayUI : UIBase
{
    public static bool Exist = false;

    public override void DoOnEntering()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        gameObject.SetActive(true);
        if(Exist == false)
        {
            //RandomMonster();
            Exist = true;
        }
        //DontDestroyOnLoad(gameObject);
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
        Exist = false;
        UIManager.Instance.PopUIPanel();
        Debug.Log("PopUIPanel----------------");
        if(GameObject.Find("SaveMap"))
        {
            Destroy(GameObject.Find("SaveMap"), 0);
        }
        this.transform.name = "SaveMap";
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("Start");
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
