using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_motion : MonoBehaviour
{
    public float WalkHorizontalSpeed;
    public float WalkVerticalSpeed;

    //Vector2 LookDir = new Vector2(0, -1);
    Vector2 LookDir;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        GameObject Map = this.gameObject.transform.parent.gameObject;
        Debug.Log(Map.name);
        if(Map.name == "Map1") {
            //玩家朝向，默认朝下
            anim.SetFloat("LookX", 0);
            anim.SetFloat("LookY", -1);
        }
        else if(Map.name == "Map2")
        {
            //玩家朝向，默认朝左
            anim.SetFloat("LookX", -1);
            anim.SetFloat("LookY", 0);
        }
        else if (Map.name == "Map3")
        {
            //玩家朝向，默认朝左
            anim.SetFloat("LookX", -1);
            anim.SetFloat("LookY", 0);
        }
        else if (Map.name == "Map4")
        {
            //玩家朝向，默认朝上
            anim.SetFloat("LookX", 1);
            anim.SetFloat("LookY", 0);
        }
        anim.SetFloat("speed", 0);
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    // Update方法一旦调用结束以后进入这里算出重置摄像机的位置  
    void LateUpdate()
    {
        Vector3 camera = GameObject.Find("Main Camera").GetComponent<Transform>().position;
        //Vector3 camera = GameObject.Find("PlayerCamera").GetComponent<Transform>().position;
        camera.x = this.transform.position.x;
        camera.y = this.transform.position.y;
        //Vector2 camera = this.transform.position;
        //GameObject.Find("Main Camera").GetComponent<Transform>().position = this.transform.position;
        GameObject.Find("Main Camera").GetComponent<Transform>().position = camera;

        
    }

    void Walk()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 MoveVector = new Vector2(moveX, moveY);
        if(MoveVector.x != 0 || MoveVector.y != 0)
        {
            LookDir = MoveVector;
            anim.SetFloat("LookX", LookDir.x);
            anim.SetFloat("LookY", LookDir.y);
            anim.SetFloat("speed", LookDir.magnitude);
        }
        else
        {
            anim.SetFloat("speed", 0);
        }
        
        //Debug.Log(MoveVector.x+ "    " + MoveVector.y + " " + MoveVector.magnitude);
        //float moveX = 1f;
       // float moveY = 1f;

        Vector2 position = transform.position;
        position.x += moveX * WalkHorizontalSpeed * Time.deltaTime;
        position.y += moveY * WalkVerticalSpeed * Time.deltaTime;
        transform.position = position;
    }
}
