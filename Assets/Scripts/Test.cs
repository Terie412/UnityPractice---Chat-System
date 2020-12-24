using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LuaInterface;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Text t = GetComponent<Text>();
        t.text = "啊啊啊";
        Debug.Log(t.preferredWidth);
        t.text = "啊啊啊啊啊啊";
        Debug.Log(t.preferredWidth);
        t.text = "啊啊啊啊啊啊啊啊啊";
        Debug.Log(t.preferredWidth);
        t.text = "啊啊啊啊啊啊啊啊啊啊啊啊";
        Debug.Log(t.preferredWidth);
        t.text = "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊";
        Debug.Log(t.preferredWidth);
    }
}
