using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

public class UIBase : MonoBehaviour
{    
    public string script;

    string className;
    LuaMgr luaMgr;
    LuaFunction init;
    LuaFunction awake;
    LuaFunction start;
    LuaFunction update;
    LuaFunction onEnable;
    LuaTable luaTable;

    private void Awake()
    {
        luaMgr = GameMgr.luaMgr;
        luaMgr.DoFile(script);

        var ss = script.Split('/');
        className = ss[ss.Length - 1].Replace(".lua", "");
        luaTable = luaMgr.GetTable(className);

        init = luaTable.GetLuaFunction("init");
        awake = luaTable.GetLuaFunction("awake");
        start = luaTable.GetLuaFunction("start");
        update = luaTable.GetLuaFunction("update");
        onEnable = luaTable.GetLuaFunction("onEnable");

        init.Call(luaTable, gameObject);
        awake.Call(luaTable);
    }

    private void Start()
    {
        start.Call(luaTable);
    }

    private void Update()
    {
        update.Call(luaTable);
    }

    private void OnEnable()
    {
        onEnable.Call(luaTable);
    }
}
