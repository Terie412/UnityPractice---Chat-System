//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class MainSceneTalkTip : MonoBehaviour
//{
//    GameObject talkAreaGO;
//    ScrollViewMgr textArea;
//    GameObject mainSceneMask;
//    GameObject left;

//    GameObject gameMgrGO;
//    GameMgr gameMgr;
//    EventMgr eventMgr;
//    NetWorkMgr netWorkMgr;
//    GlobalData globalData;

//    private void Awake()
//    {
//        talkAreaGO = GameObject.Find("TalkArea.GO");
//        textArea = GameObject.Find("TextArea").GetComponent<ScrollViewMgr>();
//        mainSceneMask = GameObject.Find("/Canvas/MainScene/MainSceneMask");
//        left = GameObject.Find("/Canvas/MainScene/Left");

//        gameMgrGO = GameObject.Find("GameMgr");
//        gameMgr = gameMgrGO.GetComponent<GameMgr>();
//        eventMgr = gameMgrGO.GetComponent<EventMgr>();
//        netWorkMgr = gameMgrGO.GetComponent<NetWorkMgr>();
//        globalData = gameMgrGO.GetComponent<GlobalData>();
//    }

//    void Start()
//    {
//        eventMgr.RegisterEvent("WORLD_MSG_COME", EH_worldMsgCome);
//        talkAreaGO.GetComponent<EventTriggerListener>().onPointUp = OnClick_TalkAreaGO;
//    }

//    private void OnEnable()
//    {
//        Debug.Log("OnEnable 执行...");
//    }

//    void OnClick_TalkAreaGO()
//    {
//        mainSceneMask.SetActive(true);
//        left.SetActive(true);

//        RectTransform leftRect = left.GetComponent<RectTransform>();
//        leftRect.anchoredPosition = new Vector2(-leftRect.rect.width / 2, leftRect.anchoredPosition.y);
//        TweenUtil.ChangeAnchorPositionTo(leftRect, new Vector2(leftRect.rect.width / 2, 0), 0.3f);

//        GameObject packUp = GameObject.Find("/Canvas/MainScene/Left/PackUp");
//        GameObject worldTalkSendButton = GameObject.Find("/Canvas/MainScene/Left/Main/Bottom/Button");
//    }

//    private void EH_worldMsgCome(object[] objs)
//    {
//        NetWorkMgr.Message msg = objs[1] as NetWorkMgr.Message;

//        textArea.mgHandler = (GameObject go, object goInfo) =>
//        {
//            NetWorkMgr.Message m = goInfo as NetWorkMgr.Message;
//            string playerName = msg.player.name;
//            string msgInfo = msg.message;
//            go.GetComponent<Text>().text = $"{playerName}: {msgInfo}";
//        };
//        GameObject th = gameMgr.GetPrefab("WorldTalkThumbnail");
//        textArea.AddCell(th, msg);
//    }
//}
