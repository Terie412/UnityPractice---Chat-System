using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMgr : MonoBehaviour
{
    GlobalData globalData;
    public delegate void EventHandler(object[] objs);
    Dictionary<string, EventHandler> eventRegistry = new Dictionary<string, EventHandler>();
    void Awake()
    {
        globalData = this.GetComponent<GlobalData>();
    }

    public void RegisterEvent(string e, EventHandler handler)
    {
        if(!globalData.allEvents.Contains(e))
        {
            Debug.LogWarning($"没有声明过事件 {e}，注册失败...");
            foreach(var s in globalData.allEvents)
            {
                Debug.LogWarning($"已有事件：{s}");
            }
            return;
        }
        Debug.Log($"注册事件{e}");
        if(eventRegistry.ContainsKey(e))
        {
            eventRegistry[e] += handler;
        }
        else
        {
            eventRegistry[e] = handler;
        }
    }

    public void UnRegisterEvent(string e, EventHandler handler)
    {
        if(!globalData.allEvents.Contains(e))
        {
            Debug.LogWarning($"没有声明过事件 {e}，注册失败...");
            return;
        }
        Debug.Log($"注销事件{e}");
        if (eventRegistry.ContainsKey(e))
        {
            eventRegistry[e] -= handler;
        }
    }

    public void DispatchEvent(string e, object[] objs)
    {
        if(!eventRegistry.ContainsKey(e))
        {
            Debug.LogWarning($"没有注册过事件：{e}");
            return;
        }
        eventRegistry[e].Invoke(objs);
    }
}
