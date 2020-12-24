using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellHandler3 : MonoBehaviour
{
    private NetWorkMgr netWorkMgr;
    private EventMgr eventMgr;

    private Text tName;
    private Text tState;
    private DateTime clickTime;
    private NetWorkMgr.Player thisPlayer;

    private void Awake()
    {
        clickTime = DateTime.MinValue;
        netWorkMgr = GameMgr.netWorkMgr;
        eventMgr = GameMgr.eventMgr;

        tName = transform.Find("Info/Name").GetComponent<Text>();
        tState = transform.Find("Info/State").GetComponent<Text>();       
    }

    private void OnEnable()
    {
        this.GetComponent<EventTriggerListener>().onPointerDown = OnPointerDown_FriendCell;
        this.GetComponent<EventTriggerListener>().onPointUp = OnPointerUp_FriendCell;
    }

    void OnPointerDown_FriendCell()
    {
        Image image = this.GetComponent<Image>();
        Color color = new Color(image.color.r, image.color.g, image.color.b, 0.3f);
        image.color = color;
        clickTime = DateTime.Now;
    }

    void OnPointerUp_FriendCell()
    {
        TimeSpan tOffset = DateTime.Now - clickTime;
        Image image = this.GetComponent<Image>();
        Color color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        image.color = color;
        if (tOffset.Milliseconds < 100)
        {
            eventMgr.DispatchEvent("OPEN_FRIEND_TALK_WINDOW", new object[] { this.thisPlayer});  
        }
    }

    public void ScrollCellIndex(int index)
    {
        RectTransform scrollRect = transform.parent.parent as RectTransform;

        LayoutElement le = GetComponent<LayoutElement>();
        le.preferredWidth = scrollRect.rect.width;

        NetWorkMgr.Player p = netWorkMgr.friends[index];
        thisPlayer = p;
        tName.text = p.name;
        string state = "";
        switch (p.state)
        {
            case NetWorkMgr.PlayerState.Offline:
                state = "离线";
                break;
            case NetWorkMgr.PlayerState.Online:
                state = "在线";
                break;
            case NetWorkMgr.PlayerState.OnQueue:
                state = "队列中";
                break;
            case NetWorkMgr.PlayerState.Playing:
                float sec = p.playingTimeInSec;
                state = $"开局{Mathf.Round(sec / 60)}分钟";
                break;
            default:
                state = "离线";
                break;
        }
        tState.text = state;
    }
}
