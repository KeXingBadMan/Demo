using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launch : MonoBehaviour
{
    Text history1;
    Text history2;

    void Awake()
    {
        //SoundManager.Instance.PlayBGM("少年游");
        history2 = transform.Find("Text2").GetComponent<Text>();
        history2.color = new Color(1f, 1f, 1f, 0f);
    }

    void Start()
    {
        history1 = transform.Find("Text1").GetComponent<Text>();
        StartCoroutine(TextManage());
    }

    public IEnumerator TextManage()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 18; i++)
        {
            history1.color = new Color(1f, 1f, 1f, (255 - 15 * i) / 255f);
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < 11; i++)
        {
            history2.color = new Color(1f, 1f, 1f, (5 + 25 * i) / 255f);
            yield return new WaitForSeconds(0.1f);
        }


    }

    public void EnterGame()
    {
        //Application.LoadLevel("Start")；
        //Debug.Log("Load Scene Start");
        SceneManager.LoadScene("Start");
    }
}
