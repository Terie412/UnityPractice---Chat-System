using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetWorkMgr : MonoBehaviour
{
    public enum PlayerState
    {
        Offline,
        Online,
        Playing,
        OnQueue
    }

    public class Player
    {
        public string name;
        public bool gender;             // true 男，false 女
        public PlayerState state;
        public float playingTimeInSec;
        public int portrait;

        public Player(string _name, bool _gender, PlayerState _state, float _playingTimeInSec)
        {
            name = _name;
            gender = _gender;
            state = _state;
            playingTimeInSec = _playingTimeInSec;
            System.Random r = new System.Random();
            portrait = r.Next(0, 10);
        }
    }

    public class Message
    {
        public Player player;
        public string message;

        public Message(Player _player, string _message)
        {
            player = _player;
            message = _message;
        }
    }

    public GlobalData globalData;
    public EventMgr eventMgr;
    public GameMgr gameMgr;
    public Player me = new Player("我", true, PlayerState.Online, 0f);
    public List<Player> friends = new List<Player>();
    private List<Player> allPlayers = new List<Player>();
    private System.Random rnd = new System.Random();
    public List<Message> worldMsg = new List<Message>();
    public List<Message> friendMsg = new List<Message>();
    public Player friendToTalk;
    public Dictionary<Player, List<Message>> friendTalkMsgs = new Dictionary<Player, List<Message>>();

    private void Awake()
    {
        globalData = this.GetComponent<GlobalData>();
        eventMgr = this.GetComponent<EventMgr>();
        gameMgr = this.GetComponent<GameMgr>();
    }

    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            string randomName = CreateRandomName();
            bool gender = rnd.Next(0, 2) == 0;
            PlayerState state = (PlayerState)rnd.Next(0, 4);
            float t = rnd.Next(1, 3600);
            Player p = new Player(randomName, gender, state, t);

            bool ifFriend = rnd.Next(0, 2) == 0;
            if (ifFriend)
                friends.Add(p);
            allPlayers.Add(p);
        }
        eventMgr.RegisterEvent("ME_FRIEND_MSG_COME", EH_onRecieveMeMessage);
        StartCoroutine(OutputMessageToWorld());
    }

    IEnumerator OutputMessageToWorld()
    {
        int wfsCount = globalData.waitForSeconds.Count;
        while (true)
        {
            int pi = rnd.Next(0, allPlayers.Count);
            int mi = rnd.Next(0, globalData.msgs.Length);

            Player p = allPlayers[pi];
            string s = globalData.msgs[mi];
            Message msg = new Message(p, s);
            worldMsg.Add(msg);
            if(worldMsg.Count > 2000)
            {
                worldMsg.RemoveRange(0, 1000);
            }
            Debug.Log($"{msg.player.name}：发送消息：{msg.message}");
            eventMgr.DispatchEvent("WORLD_MSG_COME", new object[]{gameMgr.GetPrefab("OtherMessage"), msg });
            yield return globalData.waitForSeconds[rnd.Next(0, wfsCount)];
        }
    }

    public string CreateRandomName()
    {
        string LastName =
            "赵钱孙李周吴郑王冯陈褚卫蒋沈韩杨朱秦尤施"
            + "张孔曹严华金魏陶姜戚谢邹喻柏水窦章云苏"
            + "潘葛奚范彭郎鲁韦昌马苗凤花方俞任袁柳酆"
            + "鲍史唐费廉岑薛雷贺倪汤滕殷罗毕郝邬安常"
            + "乐于时傅皮卞齐康伍余元卜顾孟平黄和穆萧"
            + "尹姚邵湛汪祁毛禹狄米贝明臧计伏成戴谈宋"
            + "茅庞熊纪舒屈项祝董梁杜阮蓝闵席季麻强贾"
            + "路娄危江童颜郭梅盛林刁钟徐邱骆高夏蔡田"
            + "樊胡凌霍虞万支柯昝管卢莫经房裘缪干解应"
            + "宗丁宣贲邓郁单杭洪包诸左石崔吉钮龚程嵇"
            + "邢滑裴陆荣翁荀羊於惠甄曲家封芮羿储靳汲"
            + "邴糜松井段富巫乌焦巴弓牧隗山谷车侯宓蓬"
            + "全郗班仰秋仲伊宫宁仇栾暴甘钭厉戎祖武符"
            + "刘景詹束龙叶幸司韶郜黎蓟薄印宿白怀蒲邰"
            + "从鄂索咸籍赖卓蔺屠蒙池乔阴胥能苍双闻莘"
            + "党翟谭贡劳逄姬申扶堵冉宰郦雍郤璩桑桂濮"
            + "牛寿通边扈燕冀郏浦尚农温别庄晏柴瞿阎充"
            + "慕连茹习宦艾鱼容向古易慎戈廖庾终暨居衡"
            + "步都耿满弘匡国文寇广禄阙东欧殳沃利蔚越"
            + "夔隆师巩厍聂晁勾敖融冷訾辛阚那简饶空曾"
            + "毋沙乜养鞠须丰巢关蒯相查後荆红游竺权逯"
            + "盖益桓公许何吕";

        int strlength = rnd.Next(1, 3);
        string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
        object[] bytes = new object[strlength];
        for (int i = 0; i < strlength; i++)
        {
            int r1 = rnd.Next(11, 14);
            string str_r1 = rBase[r1].Trim();
            int r2;
            if (r1 == 13)
            {
                r2 = rnd.Next(0, 7);
            }
            else
            {
                r2 = rnd.Next(0, 16);
            }
            string str_r2 = rBase[r2].Trim();
            int r3 = rnd.Next(10, 16);
            string str_r3 = rBase[r3].Trim();
            int r4;
            if (r3 == 10)
            {
                r4 = rnd.Next(1, 16);
            }
            else if (r3 == 15)
            {
                r4 = rnd.Next(0, 15);
            }
            else
            {
                r4 = rnd.Next(0, 16);
            }
            string str_r4 = rBase[r4].Trim();
            byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
            byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
            byte[] str_r = new byte[] { byte1, byte2 };
            bytes.SetValue(str_r, i);
        }

        Encoding gb = Encoding.GetEncoding("gb2312");
        string randomName = "";
        for(int i=0;i<bytes.Length;i++)
        {
            randomName += gb.GetString((byte[])Convert.ChangeType(bytes[i], typeof(byte[])));
        }
        string ln = LastName[rnd.Next(0, LastName.Length)].ToString();
        return ln + randomName;
    }

    public List<Message> TryGetLastMessage(int num)
    {
        int startIndex = 0;
        if (num <= 0) return null;
        startIndex = num > worldMsg.Count ? worldMsg.Count : num;

        List<Message> mss = new List<Message>();
        for(int i=worldMsg.Count - startIndex;i<worldMsg.Count;i++)
        {
            mss.Add(worldMsg[i]);
        }
        return mss;
    }

    public List<Message> TryGetLastFriendMessage(int num)
    {
        List<Message> fMsg = friendTalkMsgs[friendToTalk];

        int startIndex = 0;
        if (num <= 0) return null;
        startIndex = num > fMsg.Count ? fMsg.Count : num;

        List<Message> mss = new List<Message>();
        for (int i = fMsg.Count - startIndex; i < fMsg.Count; i++)
        {
            mss.Add(fMsg[i]);
        }
        return mss;
    }

    public int GetFriendMsgCount(Player friend)
    {
        if (!friendTalkMsgs.ContainsKey(friendToTalk))
        {
            return 0;
        }
        return friendTalkMsgs[friend].Count;
    }

    public void AddFriendMessage(Message message)
    {
        if(!friendTalkMsgs.ContainsKey(friendToTalk))
        {
            friendTalkMsgs[friendToTalk] = new List<Message>();
        }
        friendTalkMsgs[friendToTalk].Add(message);
    }

    public void EH_onRecieveMeMessage(object[] objs)
    {
        Player friend = objs[0] as Player;
        Message message = objs[1] as Message;

        StartCoroutine(GetFriendMessageFromRobot(friend, message));
    }

    IEnumerator GetFriendMessageFromRobot(Player friend, Message message)
    {
        string msg = message.message;
        using (UnityWebRequest webRequest = new UnityWebRequest($"http://api.qingyunke.com/api.php?key=free&appid=0&msg={msg}", UnityWebRequest.kHttpVerbGET))
        {
            DownloadHandler handler = new DownloadHandlerBuffer();
            webRequest.downloadHandler = handler;

            yield return webRequest.SendWebRequest();
            if(webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError($"请求机器人消息错误 {webRequest.error}");
            }
            else
            {
                msg = webRequest.downloadHandler.text;
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(msg);
                dict.TryGetValue("content", out string content);
                Debug.Log(content);
                Message fMessage = new Message(friend, content);
                friendTalkMsgs[friend].Add(fMessage);

                eventMgr.DispatchEvent("FRIEND_MSG_COME", new object[] { friend });
            }
        }
    }
}
