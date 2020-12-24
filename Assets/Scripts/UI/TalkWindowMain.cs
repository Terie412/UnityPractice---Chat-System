//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class TalkWindowMain : MonoBehaviour
//{
//    GameObject gameMgrGO;
//    GameMgr gameMgr;
//    EventMgr eventMgr;
//    NetWorkMgr netWorkMgr;
//    GlobalData globalData;

//    GameObject packup;
//    GameObject totalTalkSendButton;
//    ScrollViewMgr totalSmgr;
//    GameObject mainSceneMask;
//    InputField talkInputField;

//    GameObject otherMessagePrefab;
//    GameObject myMessagePrefab;
//    private void Awake()
//    {
//        gameMgrGO = GameObject.Find("GameMgr");
//        gameMgr = gameMgrGO.GetComponent<GameMgr>();
//        eventMgr = gameMgrGO.GetComponent<EventMgr>();
//        netWorkMgr = gameMgrGO.GetComponent<NetWorkMgr>();
//        globalData = gameMgrGO.GetComponent<GlobalData>();

//        packup = GameObject.Find("PackUp");
//        totalTalkSendButton = GameObject.Find("Main/Bottom/Button");
//        totalSmgr = GameObject.Find("Main/Center/TotalTalkWindow/Scroll View").GetComponent<ScrollViewMgr>();
//        talkInputField = GameObject.Find("Main/Bottom/InputField").GetComponent<InputField>();
//        mainSceneMask = GameObject.Find("/Canvas/MainScene/MainSceneMask");

//        otherMessagePrefab = gameMgr.GetPrefab("OtherMessage");
//        myMessagePrefab = gameMgr.GetPrefab("MyMessage");
//    }

//    private void OnEnable()
//    {
//        totalSmgr.mgHandler = (GameObject go, object goInfo) =>
//        {
//            NetWorkMgr.Message m = goInfo as NetWorkMgr.Message;
//            Transform got = go.transform;
//            Text gotName = got.Find("Name").GetComponent<Text>();
//            Text gotText = got.Find("Text").GetComponent<Text>();
//            gotName.text = m.player.name;
//            gotText.text = m.message;
//        };

//        packup.GetComponent<EventTriggerListener>().onPointUp = OnClick_PackUp;
//        totalTalkSendButton.GetComponent<EventTriggerListener>().onPointUp = OnClick_TotalTalkWindowSendButton;

//        RefreshWorldMessage();
//    }

//    public void RefreshWorldMessage()
//    {
//        List<NetWorkMgr.Message> lastWorldMsg = gameMgr.GetComponent<NetWorkMgr>().TryGetLastMessage(50);
//        for (int i = 0; i < lastWorldMsg.Count; i++)
//        {
//            totalSmgr.AddCell(otherMessagePrefab, lastWorldMsg[i]);
//        }
//        eventMgr.RegisterEvent("WORLD_MSG_COME", EH_onReceiveWorldMsg);
//    }

//    void OnClick_PackUp()
//    {
//        eventMgr.UnRegisterEvent("WORLD_MSG_COME", EH_onReceiveWorldMsg);
//        RectTransform leftrect = this.GetComponent<RectTransform>();
//        TweenUtil.ChangeAnchorPositionTo(leftrect, new Vector2(-leftrect.rect.width / 2, 0), 0.3f);
//        StartCoroutine(DisableAfterSeconds(new GameObject[] { mainSceneMask.gameObject, leftrect.gameObject }, 0.35f));
//    }

//    IEnumerator DisableAfterSeconds(GameObject[] goes, float sec)
//    {
//        yield return new WaitForSeconds(sec);
//        foreach (var go in goes)
//        {
//            Debug.Log($"设置 {go.name} 为false");
//            go.SetActive(false);
//        }
//    }

//    void OnClick_TotalTalkWindowSendButton()
//    {
//        NetWorkMgr.Message msg = new NetWorkMgr.Message(netWorkMgr.me, talkInputField.text);
//        totalSmgr.AddCell(myMessagePrefab, msg);
//    }

//    void EH_onReceiveWorldMsg(object[] objs)
//    {
//        NetWorkMgr.Message msg = objs[1] as NetWorkMgr.Message;
//        totalSmgr.AddCell(otherMessagePrefab, msg);
//    }
//}
