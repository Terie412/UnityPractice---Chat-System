using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public System.Random rnd = new System.Random();
    public string[] msgs =
    {
        "上分车队，快上车！！！",
        "真心互赞，你们却不赞，不爱了。",
        "有没有战队收人？？？",
        "有人匹配一起吗？",
        "刚失恋，找个不渣的徒弟，本人萝莉，加Q156489618",
        "互赞",
        "互赞互赞互赞互赞互赞互赞互赞互赞互赞互赞互赞互赞",
        "有没有小哥哥一起玩？",
        "深夜网易云好烦呀",
        "我是富婆",
        "互赞秒回",
        "来个瑶瑶~~",
        "有没有搞错，会不会辅助，刚才那个！"
    };

    private readonly List<string> m_allEvents = new List<string>()
    {
        "WORLD_MSG_COME",                   // 每次收到世界消息
        "OPEN_FRIEND_TALK_WINDOW",           // 打开好友消息窗口
        "ME_FRIEND_MSG_COME",               // 朋友收到来自我的消息
        "FRIEND_MSG_COME",                  // 我收到来自朋友的消息
    };
    public List<string> allEvents
    {
        get { return m_allEvents; }
    }

    public List<WaitForSeconds> waitForSeconds = new List<WaitForSeconds>()
    {
        new WaitForSeconds(1f),
        new WaitForSeconds(2f),
        new WaitForSeconds(3f),
        new WaitForSeconds(4f),
        new WaitForSeconds(5f),
    };
}
