using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellHandler2 : MonoBehaviour
{
    private NetWorkMgr netWorkMgr;

    private GameObject go1;
    private GameObject go2;
    private Text text1;
    private Text name1;
    private RectTransform pic1;
    private Text text2;
    private Text name2;
    private RectTransform pic2;

    private void Awake()
    {
        netWorkMgr = GameMgr.netWorkMgr;

        go1 = transform.Find("OtherMessage").gameObject;
        go2 = transform.Find("MyMessage").gameObject;

        text1 = transform.Find("OtherMessage/Text").GetComponent<Text>();
        name1 = transform.Find("OtherMessage/Name").GetComponent<Text>();
        pic1 = transform.Find("OtherMessage/TalkArea").GetComponent<RectTransform>();
        text2 = transform.Find("MyMessage/Text").GetComponent<Text>();
        name2 = transform.Find("MyMessage/Name").GetComponent<Text>();
        pic2 = transform.Find("MyMessage/TalkArea").GetComponent<RectTransform>();
    }

    void ScrollCellIndex(int index)
    {
        RectTransform scrollRect = transform.parent.parent as RectTransform;

        LayoutElement le = GetComponent<LayoutElement>();
        le.preferredWidth = scrollRect.rect.width;
        le.preferredHeight = 100;

        List<NetWorkMgr.Message> msgs = netWorkMgr.TryGetLastMessage(30);
        if (msgs.Count == 0)
            return;
        NetWorkMgr.Message message = msgs[index];

        float maxLength = scrollRect.rect.width - 200.0f;

        if (message.player != netWorkMgr.me)
        {
            go1.SetActive(true);
            go2.SetActive(false);
            name1.text = message.player.name;
            text1.text = message.message;
            ModifyText(text1, maxLength);
            pic1.sizeDelta = new Vector2(Mathf.Min(text1.preferredWidth, text1.GetComponent<RectTransform>().sizeDelta.x) + 100, pic1.sizeDelta.y);   
        }
        else
        {
            go2.SetActive(true);
            go1.SetActive(false);
            name2.text = message.player.name;
            text2.text = message.message;
            text2.alignment = TextAnchor.MiddleRight;
            ModifyText(text2, maxLength, true);
            pic2.sizeDelta = new Vector2(Mathf.Min(text2.preferredWidth, text2.GetComponent<RectTransform>().sizeDelta.x) + 100, pic2.sizeDelta.y); 
        }
    }

    void ModifyText(Text t, float maxLength, bool modifyAlignment = false)
    {
        if (t.preferredHeight > 30)
        {
            GetComponent<LayoutElement>().preferredHeight = t.preferredHeight + 70;
            if(modifyAlignment)
            {
                t.alignment = TextAnchor.MiddleLeft;
            }
        }
    }
}
