using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System;

public class LuaMgr : MonoBehaviour
{
    private LuaState lua;
    private LuaLooper loop;
    private void Awake()
    {
        lua = new LuaState();
        this.OpenLibs();
        lua.LuaSetTop(0);

        LuaBinder.Bind(lua);
        DelegateFactory.Init();
        LuaCoroutine.Register(lua, this);

        InitStart();
    }

    public void InitStart()
    {
        InitLuaPath();
        this.lua.Start();
        this.StartMain();
        this.StartLooper();
    }

    public void StartMain()
    {   

        lua.DoFile("global_data");
        LuaTable gd = lua.GetTable("global_data");
        LuaFunction main = gd.GetLuaFunction("init");
        main.Invoke<LuaTable, LuaTable>(gd);
        main.Dispose();
        main = null;
    }

    void StartLooper()
    {
        loop = gameObject.AddComponent<LuaLooper>();
        loop.luaState = lua;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LuaOpen_Socket_Core(IntPtr L)
    {
        return LuaDLL.luaopen_socket_core(L);
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LuaOpen_Mime_Core(IntPtr L)
    {
        return LuaDLL.luaopen_mime_core(L);
    }

    protected void OpenCJson()
    {
        lua.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
        lua.OpenLibs(LuaDLL.luaopen_cjson);
        lua.LuaSetField(-2, "cjson");

        lua.OpenLibs(LuaDLL.luaopen_cjson_safe);
        lua.LuaSetField(-2, "cjson.safe");
    }

    // 初始化加载第三方库
    void OpenLibs()
    {
        lua.OpenLibs(LuaDLL.luaopen_pb);
        lua.OpenLibs(LuaDLL.luaopen_sproto_core);
        lua.OpenLibs(LuaDLL.luaopen_protobuf_c);
        lua.OpenLibs(LuaDLL.luaopen_lpeg);
        lua.OpenLibs(LuaDLL.luaopen_bit);
        lua.OpenLibs(LuaDLL.luaopen_socket_core);

#if UNITY_EDITOR
        lua.BeginPreLoad();
        lua.RegFunction("socket.core", LuaOpen_Socket_Core);
        lua.RegFunction("mime.core", LuaOpen_Mime_Core);
        lua.EndPreLoad();
#endif

#if LuaProfiler
        lua.OpenLibs(LuaDLL.luaopen_snapshot);
        lua.OpenLibs(LuaDLL.luaopen_profiler);
#endif
        this.OpenCJson();
    }

    public void Dispose()
    {
        lua.Dispose();
    }

    void InitLuaPath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        lua.AddSearchPath(Application.persistentDataPath + "/lua");
#else
        lua.AddSearchPath(Application.dataPath + "/Scripts/Lua");
#endif
    }


    public void DoFile(string filename)
    {
        lua.DoFile(filename);
    }

    public LuaFunction GetFunction(string funcName)
    {
        return lua.GetFunction(funcName);
    }

    public LuaTable GetTable(string tableName)
    {
        return lua.GetTable(tableName);
    }
}
