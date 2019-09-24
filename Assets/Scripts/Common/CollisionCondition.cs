using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionCondition : MonoBehaviour
{
    public UnityEvent OnCollisionHandler;
  
    /*void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Event");
        //OnCollisionHandler.Invoke();
    }*/

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Enter");
        UIManager.Instance.PushUIPanel("Battle");
        
        OnCollisionHandler.Invoke();

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Collision Exit");
    }

}
