﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class TweenUtilWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(TweenUtil), typeof(Singleton<TweenUtil>));
		L.RegFunction("ChangeAnchorPositionTo", ChangeAnchorPositionTo);
		L.RegFunction("ChangeSliderValueTo", ChangeSliderValueTo);
		L.RegFunction("New", _CreateTweenUtil);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateTweenUtil(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				TweenUtil obj = new TweenUtil();
				ToLua.Push(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: TweenUtil.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ChangeAnchorPositionTo(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.RectTransform arg0 = (UnityEngine.RectTransform)ToLua.CheckObject(L, 1, typeof(UnityEngine.RectTransform));
			UnityEngine.Vector2 arg1 = ToLua.ToVector2(L, 2);
			float arg2 = (float)LuaDLL.luaL_checknumber(L, 3);
			TweenUtil.ChangeAnchorPositionTo(arg0, arg1, arg2);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ChangeSliderValueTo(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.UI.Slider arg0 = (UnityEngine.UI.Slider)ToLua.CheckObject<UnityEngine.UI.Slider>(L, 1);
			float arg1 = (float)LuaDLL.luaL_checknumber(L, 2);
			float arg2 = (float)LuaDLL.luaL_checknumber(L, 3);
			TweenUtil.ChangeSliderValueTo(arg0, arg1, arg2);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

