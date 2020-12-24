using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyMessageFitter : CellFitter
{
    public RectTransform content;

    GameObject talkArea;
    GameObject textGO;
    //private void Awake()
    //{
    //    talkArea = transform.Find("TalkArea").gameObject;
    //    textGO = transform.Find("Text").gameObject;
    //    content = transform.parent as RectTransform;
    //}

    //private void Start()
    //{
    //}

    //public void ResizeBg()
    //{
    //    Text text = textGO.GetComponent<Text>();
    //    string msg = text.text;
    //    if (text.preferredWidth > content.rect.width * 3.0 / 4.0)
    //    {
    //        int index = (int)((msg.Length * content.rect.width * 2.0) / (text.preferredWidth * 4.0));
    //        msg = msg.Insert(index, "\n");
    //    }
    //    text.text = msg;

    //    RectTransform talkAreaRect = talkArea.GetComponent<RectTransform>();
    //    talkAreaRect.sizeDelta = new Vector2(text.preferredWidth + 80, talkAreaRect.sizeDelta.y);
    //}

    public override void Fit()
    {
        
    }
}
