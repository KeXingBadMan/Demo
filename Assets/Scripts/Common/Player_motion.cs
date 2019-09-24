using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_motion : MonoBehaviour
{
    public float WalkHorizontalSpeed;
    public float WalkVerticalSpeed;

    //玩家朝向，默认朝下
    Vector2 LookDir = new Vector2(0, -1);

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("LookX", 0);
        anim.SetFloat("LookY", -1);
        anim.SetFloat("speed", 0);
        //GameObject.Find("Main Camera").GetComponent<GameRoot>().Player = this.gameObject;
        //GameObject.Find("Main Camera").GetComponent<GameRoot>().WalkHorizontalSpeed = this.WalkHorizontalSpeed;
        //GameObject.Find("Main Camera").GetComponent<GameRoot>().WalkVerticalSpeed = this.WalkVerticalSpeed;
        //GameObject.Find("Manager").GetComponent<GameRoot>().Player = this.gameObject;
        //GameObject.Find("Manager").GetComponent<GameRoot>().WalkHorizontalSpeed = this.WalkHorizontalSpeed;
        //GameObject.Find("Manager").GetComponent<GameRoot>().WalkVerticalSpeed = this.WalkVerticalSpeed;
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
        camera.x = this.transform.position.x;
        camera.y = this.transform.position.y;
        //Vector2 camera = this.transform.position;
        //GameObject.Find("Main Camera").GetComponent<Transform>().position = this.transform.position;
        GameObject.Find("Main Camera").GetComponent<Transform>().position = camera;

        // target为主角，缩放旋转的参照物  
        /*if (target)
        {
            // 重置摄像机的位置  
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position + new Vector3(0f, 0.6f, 0f);
            //  Debug.Log();
            transform.rotation = rotation;
            transform.position = position;
        }*/
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
