using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffButton : MonoBehaviour
{
    public Buff myBuff;
    public BattleSceneManager sceneManager;
    public bool selected;//该buff是否被玩家选中

    // Update is called once per frame
    void Update()
    {
        if (!sceneManager.pickingBuff)
        {
            if(!selected)
            {
                sceneManager.playerBuffButtons.Remove(this.gameObject);
                sceneManager.playerBuffs.Remove(myBuff);
                Destroy(this.gameObject);
            }
            else
            {
                this.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
        }
    }

    //每次点击后运行SetSelected，更新是否被选中
    public void SetSelected()
    {
        selected = !selected;
    }
}
