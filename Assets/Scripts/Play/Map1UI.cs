using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map1UI : UIBase
{
    public static bool Exist = false;
    public static bool RandomBegger = false;
    public static bool RandomElite = false;

    public override void DoOnEntering()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        gameObject.SetActive(true);
        if (Exist == false)
        {
            RandomMonster();
            Exist = true;
        }
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
        if (GameObject.Find("SaveMap"))
        {
            Destroy(GameObject.Find("SaveMap"), 0);
        }
        this.transform.name = "SaveMap";
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("Start");
    }

    public void RandomMonster()
    {
        GameObject random = GameObject.Find("begger");
        if (Random.Range(1, 11) < 6)
        {
            random.SetActive(true);
            RandomBegger = true;
        }
        else
        {
            random.SetActive(false);
        }
        random = GameObject.Find("elite");
        if (Random.Range(1, 11) < 6)
        {
            random.SetActive(true);
            RandomElite = true;
        }
        else
        {
            random.SetActive(false);
        }

    }

}
