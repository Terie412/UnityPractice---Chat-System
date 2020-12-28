using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LuaInterface;
using UnityEngine.Networking;
using Newtonsoft.Json;

[RequireComponent(typeof(EventTriggerListener))]
public class Test : MonoBehaviour
{
    private void Start()
    {
        var e = GetComponent<EventTriggerListener>();
        if(e == null)
        {
            e = this.gameObject.AddComponent<EventTriggerListener>();
        }

        e.onPointUp = OnPointUp;
    }

    void OnPointUp()
    {
        var r = GetComponent<CanvasRenderer>();
        if(r != null) {
            Debug.Log(r.absoluteDepth);
        }
    }

}
