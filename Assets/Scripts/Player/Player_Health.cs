using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    // Start is called before the first frame update
    private int maxHp = 20;
    private int curHp = 20;
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerHp"))
            curHp = PlayerPrefs.GetInt("PlayerHp");
        else {
            PlayerPrefs.SetInt("PlayerHp", maxHp);
            curHp = PlayerPrefs.GetInt("PlayerHp");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
