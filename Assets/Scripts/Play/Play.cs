using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("This is debug");
        UIManager.Instance.PushUIPanel("Map1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
