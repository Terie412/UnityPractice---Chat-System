using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCellHandler : MonoBehaviour
{
    public string luaHandler;

    string className;
    LuaMgr luaMgr;
    LuaFunction awake;
    LuaFunction start;
    LuaFunction update;
    LuaFunction onEnable;
    LuaFunction scrollCellIndex;
    LuaTable launchTable;

    private void Awake()
    {
        luaMgr = GameMgr.luaMgr;
        luaMgr.DoFile(luaHandler);

        var ss = luaHandler.Split('/');
        className = ss[ss.Length - 1].Replace(".lua", "");
        launchTable = luaMgr.GetTable(className);

        awake = launchTable.GetLuaFunction("awake");
        start = launchTable.GetLuaFunction("start");
        update = launchTable.GetLuaFunction("update");
        onEnable = launchTable.GetLuaFunction("onEnable");

        awake.Call(launchTable);
    }

    private void Start()
    {
        start.Call(launchTable);
    }

    private void Update()
    {
        update.Call(launchTable);
    }

    private void OnEnable()
    {
        onEnable.Call(launchTable);
    }
}
