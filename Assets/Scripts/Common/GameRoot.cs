using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public static bool Exist = false; //是否第一次打开Start.Scene的标志位

    public GameObject StartUI;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("This is debug");
        if (Exist != false)
        {

            UIManager.Instance.PushUIPanel("StartUI");
            //GameObject StartUI_temp = Instantiate(StartUI);
            //UIManager.Instance.PushUIPanel("StartUI");
        }
        else
        {
            Exist = true;
            SoundManager.Instance.PlayBGM("少年游");
            DontDestroyOnLoad(this.gameObject);
            UIManager.Instance.PushUIPanel("StartUI");
            //DontDestroyOnLoad(GameObject.Find("StartUI"));
        }
        
    }

    
}
