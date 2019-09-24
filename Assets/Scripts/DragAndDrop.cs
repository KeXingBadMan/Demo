using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//点击抓取，松开放下的script
//使用script需要在gameobject上添加collider并且设定为trigger
//
public class DragAndDrop : MonoBehaviour
{
    public bool selected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            selected = false;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
        }
    }
}
