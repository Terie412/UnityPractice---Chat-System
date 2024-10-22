﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class NetWorkMgr_MessageWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(NetWorkMgr.Message), typeof(System.Object));
		L.RegFunction("New", _CreateNetWorkMgr_Message);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("player", get_player, set_player);
		L.RegVar("message", get_message, set_message);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateNetWorkMgr_Message(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				NetWorkMgr.Player arg0 = (NetWorkMgr.Player)ToLua.CheckObject<NetWorkMgr.Player>(L, 1);
				string arg1 = ToLua.CheckString(L, 2);
				NetWorkMgr.Message obj = new NetWorkMgr.Message(arg0, arg1);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: NetWorkMgr.Message.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_player(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			NetWorkMgr.Message obj = (NetWorkMgr.Message)o;
			NetWorkMgr.Player ret = obj.player;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index player on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_message(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			NetWorkMgr.Message obj = (NetWorkMgr.Message)o;
			string ret = obj.message;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index message on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_player(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			NetWorkMgr.Message obj = (NetWorkMgr.Message)o;
			NetWorkMgr.Player arg0 = (NetWorkMgr.Player)ToLua.CheckObject<NetWorkMgr.Player>(L, 2);
			obj.player = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index player on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_message(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			NetWorkMgr.Message obj = (NetWorkMgr.Message)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.message = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index message on a nil value");
		}
	}
}

