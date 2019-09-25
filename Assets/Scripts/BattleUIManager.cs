using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager instance { get; private set; }
    public GameObject DialogUI;
    public Text Message;
    public Button ConfirmButton;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DialogUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDialogUI(string _message)
    {
        DialogUI.SetActive(true);
        Message.text = _message;
    }
    public void CloseDialogUI()
    {
        DialogUI.SetActive(false);
    }

}
