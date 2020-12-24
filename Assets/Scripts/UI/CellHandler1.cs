using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellHandler1 : MonoBehaviour
{
    private Text text;
    private NetWorkMgr netWorkMgr;

    private void Awake()
    {
        text = this.GetComponent<Text>();
        netWorkMgr = GameMgr.netWorkMgr;
    }

    void ScrollCellIndex(int index)
    {
        var message = netWorkMgr.worldMsg[netWorkMgr.worldMsg.Count - 1];
        string str = $"{message.player.name} : {message.message}";
        text.text = str;
    }
}
