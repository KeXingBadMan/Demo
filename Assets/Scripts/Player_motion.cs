using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_motion : MonoBehaviour
{
    public float WalkHorizontalSpeed;
    public float WalkVerticalSpeed;
    // Start is called before the first frame update
    void Start()
    {
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
        WalkForward();
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

    void WalkForward()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //float moveX = 1f;
       // float moveY = 1f;

        Vector2 position = transform.position;
        position.x += moveX * WalkHorizontalSpeed * Time.deltaTime;
        position.y += moveY * WalkVerticalSpeed * Time.deltaTime;
        transform.position = position;
    }
}
