using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameMgr gameMgr;
    GameObject mainScene;
    GameObject talkArea_GO;
    GameObject left;
    GameObject mainSceneMask;
    delegate void myAction(object[] objs);

    GameObject prefab_FriendCell;
    GameObject prefab_MainScene;
    GameObject prefab_MyMessage;
    GameObject prefab_OtherMessage;
    GameObject prefab_WorldTalkThumbnail;

    private void Awake()
    {
        //gameMgr = this.GetComponent<GameMgr>();
        //prefab_FriendCell = gameMgr.GetPrefab("FriendCell");
        //prefab_Launch = gameMgr.GetPrefab("Launch");
        //prefab_MainScene = gameMgr.GetPrefab("MainScene");
        //prefab_MyMessage = gameMgr.GetPrefab("MyMessage");
        //prefab_OtherMessage = gameMgr.GetPrefab("OtherMessage");

        //mgr = this.GetComponent<GameMgr>();
        //gameMgr = GameObject.Find("GameMgr");
        //mainScene = GameObject.Find("/Canvas/MainScene");
        //talkArea_GO = GameObject.Find("/Canvas/MainScene/LeftBottom/TalkArea.GO");
    }



    

    //public void OnClickMainTalkArea()
    //{
    //    if (!mainSceneMask)
    //    {
    //        GameObject m_mainScene = mgr.GetPrefab("MainSceneMask");
    //        mainSceneMask = Instantiate<GameObject>(m_mainScene, mainScene.transform);
    //        mainSceneMask.name = "MainSceneMask";
    //    }
    //    if (!left)
    //    {
    //        GameObject m_left = mgr.GetPrefab("Left");
    //        left = Instantiate<GameObject>(m_left, mainScene.transform);
    //        left.name = "Left";
    //    }
    //    mainSceneMask.SetActive(true);
    //    RectTransform leftRect = left.GetComponent<RectTransform>();
    //    leftRect.anchoredPosition += new Vector2(-leftRect.rect.width, leftRect.anchoredPosition.y);
    //    DOTween.To(
    //        () =>
    //        {
    //            return leftRect.anchoredPosition;
    //        },
    //        v =>
    //        {
    //            leftRect.anchoredPosition = v;
    //        },
    //        new Vector2(leftRect.rect.width / 2, 0), 0.3f
    //    );

    //    GameObject packUp = GameObject.Find("/Canvas/MainScene/Left/PackUp");
    //    GameObject worldTalkSendButton = GameObject.Find("/Canvas/MainScene/Left/Main/Bottom/Button");
    //    packUp.GetComponent<EventTriggerListener>().onPointUp = OnClickPackUp;
    //    worldTalkSendButton.GetComponent<EventTriggerListener>().onPointUp = OnClickWorldTalkSendButton;

    //    StartCoroutine(DelayFrameOperate(RefreshWorldMessage));
    //}

    //public void RefreshWorldMessage()
    //{
    //    ScrollViewMgr smgr = GameObject.Find("/Canvas/MainScene/Left/Main/Center/TotalTalkWindow/Scroll View").GetComponent<ScrollViewMgr>();
    //    GameMgr gmgr = gameMgr.GetComponent<GameMgr>();
    //    List<NetWorkMgr.Message> lastWorldMsg = gameMgr.GetComponent<NetWorkMgr>().TryGetLastMessage(50);
    //    GameObject otherMessagePrefab = gmgr.GetPrefab("OtherMessage");
    //    for (int i = 0; i < lastWorldMsg.Count; i++)
    //    {
    //        Debug.Log("添加元素");
    //        smgr.AddCell(otherMessagePrefab, lastWorldMsg[i]);
    //    }
    //}

    //public void OnClickPackUp()
    //{
    //    Debug.Log("OnClickPackUp");
    //    RectTransform leftRect = left.GetComponent<RectTransform>();
    //    DOTween.To(
    //        () =>
    //        {
    //            return leftRect.anchoredPosition;
    //        },
    //        v =>
    //        {
    //            leftRect.anchoredPosition = v;
    //        },
    //        new Vector2(-leftRect.rect.width / 2, 0), 0.3f
    //    );
    //    mainSceneMask.SetActive(false);
    //}

    //public void OnClickWorldTalkSendButton()
    //{
    //    InputField inputField = GameObject.Find("/Canvas/MainScene/Left/Main/Bottom/InputField").GetComponent<InputField>();
    //    string s = inputField.text;
    //    NetWorkMgr.Message msg = new NetWorkMgr.Message(gameMgr.GetComponent<NetWorkMgr>().me, s);
    //    ScrollViewMgr smgr = GameObject.Find("/Canvas/MainScene/Left/Main/Center/TotalTalkWindow/Scroll View").GetComponent<ScrollViewMgr>();
    //    smgr.AddCell(gameMgr.GetComponent<GameMgr>().GetPrefab("MyMessage"), msg);
    //}

    //IEnumerator DelayFrameOperate(Action action)
    //{
    //    yield return null;
    //    action();
    //}

    //IEnumerator DelayFrameOperate(myAction action, object[] objs)
    //{
    //    yield return null;
    //    action(objs);
    //}
}
