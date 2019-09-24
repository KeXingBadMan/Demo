using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public GameObject Player;
    public float WalkHorizontalSpeed;
    public float WalkVerticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("This is debug");
        UIManager.Instance.PushUIPanel("StartUI");
        //DontDestroyOnLoad(this.gameObject);
    }

    /*void Update()
    {
        if(Player != null)
        {
            Walk();
        }
    }*/

    /*public void Walk()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        //float moveX = 1f;
        //float moveY = 1f;

        Vector2 position = transform.position;
        position.x += moveX * WalkHorizontalSpeed * Time.deltaTime;
        position.y += moveY * WalkVerticalSpeed * Time.deltaTime;
        transform.position = position;
    }*/
}
