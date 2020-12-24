using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

// 同时充当 AssetMgr，负责资源加载
public class GameMgr : MonoBehaviour
{
    Dictionary<string, GameObject> prefab_list;
    public List<GameObject> prefabs;
    Dictionary<string, GameObject> m_prefab_list;
    private StreamWriter writer;

    private static GameMgr m_gameMgr;
    public static GameMgr gameMgr
    {
        get
        {
            if (m_gameMgr == null)
            {
                var gr = FindObjectOfType(typeof(GameMgr)) as GameMgr;
                if (gr == null)
                {
                    GameObject go = new GameObject("GameMgr", typeof(GameMgr));
                    go.AddComponent<UIManager>();
                    go.AddComponent<GlobalData>();
                    go.AddComponent<LuaMgr>();
                    go.AddComponent<EventMgr>();
                    gr = go.GetComponent<GameMgr>();
                }
                m_gameMgr = gr;
            }          
            return m_gameMgr;
        }
    }

    private static UIManager m_uiMgr;
    public static UIManager uiMgr
    {
        get
        {
            if(m_uiMgr == null)
            {
                m_uiMgr = gameMgr.GetComponent<UIManager>();
            }
            return m_uiMgr;
        }
    }

    private static GlobalData m_globalData;
    public static GlobalData globalData
    {
        get
        {
            if (m_globalData == null)
            {
                m_globalData = gameMgr.GetComponent<GlobalData>();
            }
            return m_globalData;
        }
    }

    private static LuaMgr m_luaMgr;
    public static LuaMgr luaMgr
    {
        get
        {
            if (m_luaMgr == null)
            {
                m_luaMgr = gameMgr.GetComponent<LuaMgr>();
            }
            return m_luaMgr;
        }
    }

    private static EventMgr m_eventMgr;
    public static EventMgr eventMgr
    {
        get
        {
            if (m_eventMgr == null)
            {
                m_eventMgr = gameMgr.GetComponent<EventMgr>();
            }
            return m_eventMgr;
        }
    }

    private static SG.ResourceManager m_resourceMgr;
    public static SG.ResourceManager resourceMgr
    {
        get
        {
            if (m_resourceMgr == null)
            {
                m_resourceMgr = gameMgr.GetComponent<SG.ResourceManager>();
            }
            return m_resourceMgr;
        }
    }

    private static NetWorkMgr m_netWorkMgr;
    public static NetWorkMgr netWorkMgr
    {
        get
        {
            if (m_netWorkMgr == null)
            {
                m_netWorkMgr = gameMgr.GetComponent<NetWorkMgr>();
            }
            return m_netWorkMgr;
        }
    }

    private void Awake()
    {
        string dir = Application.persistentDataPath;
        writer = new StreamWriter(dir + "/log.txt", true);
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.logMessageReceived += LogCallback;
#endif
        prefab_list = new Dictionary<string, GameObject>();
        m_prefab_list = new Dictionary<string, GameObject>();

        DontDestroyOnLoad(this);

        foreach (GameObject go in prefabs)
        {
            m_prefab_list.Add(go.name, go);
        }

        dir = Application.streamingAssetsPath;
        StartCoroutine(LoadAssestBundle(dir + "/ui_main"));
    }

    private void Start()
    {
        string[] poolNames =
        {
            "Text",
            "Cell",
            "Cell2",
            "FriendCell"
        };
        foreach(var n in poolNames)
        {
            resourceMgr.InitPool(n, 10, SG.PoolInflationType.DOUBLE, GetPrefab(n));
        }

        GameObject uiroot = GameObject.Find("/Canvas");
        GameObject launchGO = uiroot.transform.Find("Launch").gameObject;       
        launchGO.SetActive(true);
    }

    IEnumerator LoadAssestBundle(string path)
    {
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(path);
        yield return request;
        AssetBundle ab = request.assetBundle;
        Object[] objs = ab.LoadAllAssets();

        string ret = "";
        for (int i = 0; i < objs.Length; i++)
        {
            GameObject go = objs[i] as GameObject;
            prefab_list.Add(go.name, go);
            ret += go.name + ", ";
        }
        Debug.Log($"已完成资源加载：{ret}");
    }

    public GameObject GetPrefab(string name)
    {
#if UNITY_EDITOR
        // 编辑器模式下直接加载预置体
        if (!m_prefab_list.ContainsKey(name))
        {
            Debug.LogWarning("没有成功获取到预置体：{name}");
            return null;
        }
        return m_prefab_list[name];
#else
        if (!prefab_list.ContainsKey(name))
            return null;
        return prefab_list[name];
#endif
    }

    void LogCallback(string condition, string stackTrace, LogType type)
    {
        string content = "";
        content += System.DateTime.Now + ":" + type.ToString() + ": " + "\r\n" +
         "condition" + ": " + condition + "\r\n" +
         "stackTrace" + ": " + stackTrace + "\r\n" +
         "--------------------------------------" + "\r\n";
        writer.Write(content);
        writer.Flush();
    }

    private void OnApplicationQuit()
    {
        Application.logMessageReceived -= LogCallback;
    }

    // ---------------- GameHelper ---------------------------
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); //gameObject.AddComponent(System.Type.GetType("NetWorkMgr"));       
    }

    public Vector2 NewVector2(float x, float y)
    {
        return new Vector2(x, y);
    }

    public Vector3 NewVector3(float x, float y, float z)
    {
        return new Vector3(x, y, z);
    }

    public Color NewColor(float r, float g, float b, float a)
    {
        return new Color(r, g, b, a);
    }

    public NetWorkMgr.Player NewPlayer(string name, bool gender, NetWorkMgr.PlayerState state, float playingTimeInSec)
    {
        return new NetWorkMgr.Player(name, gender, state, playingTimeInSec);
    }

    public NetWorkMgr.Message NewMessage(NetWorkMgr.Player p, string message)
    {
        return new NetWorkMgr.Message(p, message);
    }
}
