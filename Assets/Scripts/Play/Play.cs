﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //UIManager.Instance.PushUIPanel("Map1");
        UIManager.Instance.PushUIPanel(RandomMap());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string RandomMap()
    {
        string Map = "Map";

        //Random.Range(min,max)返回一个随机整数，在min(包含)和max(不包含)之间
        int MapNum = Random.Range(1, 3);
        Debug.Log(MapNum);
        Map = Map + MapNum;
        Debug.Log(Map);
        return Map;
    }
}
