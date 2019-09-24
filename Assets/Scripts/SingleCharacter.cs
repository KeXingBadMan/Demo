using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCharacter : MonoBehaviour
{
    public bool spelling = false;//true：在字槽中 false：不在字槽中
    public bool lastSpelling = false;
    public GameObject speller;//字槽GameObject， 用于根据距离判定是否被吸附到字槽中
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SetSpelling();
    }

    void SetSpelling()//判定是否在拼写（是否被吸附到字槽中）
    {
        lastSpelling = spelling;
        if (Mathf.Abs(transform.position.x - speller.transform.position.x) < 2 && gameObject.GetComponent<DragAndDrop>().selected == false)
        {
            spelling = true;
        }
        else
        {
            spelling = false;
        }
        
    }
}
