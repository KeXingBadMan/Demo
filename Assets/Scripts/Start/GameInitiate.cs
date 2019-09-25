using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitiate : MonoBehaviour
{
    public static bool ManagerExist = false; //是否第一次打开Start.Scene的标志位
    public GameObject Manager;//Manager实例化
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("This is debug");
        if (ManagerExist == false)
        {
            ManagerExist = true;
            GameObject ForeverManager = Instantiate(Manager);
        }
    }
    
}
