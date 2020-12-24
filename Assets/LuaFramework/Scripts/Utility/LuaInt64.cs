using System;

class LuaInt64Helper
{
    public const string X36 = "0123456789abcdefghijklmnopqrstuvwxyz";
    //private const string X36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private static ulong[] powList = new ulong[16];
    private static bool isInitialized = false;
    private static void Initialize()
    {
        isInitialized = true;

        powList[0] = 1;

        for (int i = 1; i < powList.Length; ++ i)
        {
            powList[i] = powList[i - 1] * 36;
        }
    }

    public static ulong GetPow36(int n)
    {
        if (!isInitialized)
            Initialize();

        if (n >= 0 && n < powList.Length)
            return powList[n];
        else
            return 1UL;
    }
    public static int GetChar36Index(char ch)
    {
        //return X36.IndexOf(ch);

        if (ch >= '0' && ch <= '9')
            return ch - '0';
        else if (ch >= 'a' && ch <= 'z')
            return ch - 'a' + 10;
        else
            return 0;
    }
}

public class LuaInt64
{
    private static char[] s_covertTempArray = new char[16];

    ulong Value = 0;
    public ulong GetValue()
    {
        return Value;
    }
    public bool LessThan(LuaInt64 other)
    {
        return this.Value < other.Value;
    }
	public LuaInt64()
	{

	}
    public LuaInt64(byte[] data)
    {
        Value = BitConverter.ToUInt64(data, 0);
    }

    public LuaInt64(ulong data)
	{
		Value = data;
	}
    public static LuaInt64 FromString(string str)
    {
        ulong ret = 0;
        ulong.TryParse(str, out ret);
        return new LuaInt64(ret);
    }
    public static LuaInt64 FromStringShort(string str)
    {
        ulong ret = 0;
        int len = str.Length;

        for (int i = len; i > 0; i--)
        {
            ret += (ulong)LuaInt64Helper.GetChar36Index(str[i - 1]) * LuaInt64Helper.GetPow36(len - i);
        }

        return new LuaInt64(ret);
    }
    public static string ToString(byte[] data)
	{
		ulong value = BitConverter.ToUInt64(data, 0);
		return value.ToString();
	}

    [LuaInterface.NoToLua]
    public static string ToStringShort(ulong val)
    {
        int index = 15;
        while (val > 0 && index >= 0)
        {
            s_covertTempArray[index--] = LuaInt64Helper.X36[(int)(val % 36)];
            val /= 36;
        }
        return new string(s_covertTempArray, index + 1, 15 - index);
    }

    public static string ToStringShort(byte[] data)
    {
        ulong value = BitConverter.ToUInt64(data, 0);
        return LuaInt64.ToStringShort(value);
    }
    
	public LuaInterface.LuaByteBuffer ToFixed64()
	{
        /*byte[] bytes = new byte[8];
        bytes[0] = (byte)(Value >> 56);
        bytes[1] = (byte)((Value >> 48) & 255);
        bytes[2] = (byte)((Value >> 40) & 255);
        bytes[3] = (byte)((Value >> 32) & 255);
        bytes[4] = (byte)((Value >> 24) & 255);
        bytes[5] = (byte)((Value >> 16) & 255);
        bytes[6] = (byte)((Value >> 8) & 255);
        bytes[7] = (byte)(Value & 255);*/
        return new LuaInterface.LuaByteBuffer(BitConverter.GetBytes(Value));
    }

	public static LuaInt64 operator +(LuaInt64 a, LuaInt64 b)
	{
		return new LuaInt64(a.Value + b.Value);
	}

	public static LuaInt64 operator -(LuaInt64 a, LuaInt64 b)
	{
		return new LuaInt64(a.Value - b.Value);
	}

	public static bool operator== (LuaInt64 a, LuaInt64 b)
	{
		return a.Value == b.Value;
	}

	public static bool operator!= (LuaInt64 a, LuaInt64 b)
	{
		return a.Value != b.Value;
	}

    public static float operator /(LuaInt64 a, LuaInt64 b)
    {
        if (b.Value != 0)
        {
            return (float)((double)a.Value / (double)b.Value);
        }
        return 0f;
    }

	public bool CheckFlag(int flag)
	{
		return (Value & ((1uL) << flag)) != 0;
	}

    public override string ToString()
    {
        return Value.ToString();
    }

    public string ToStringShort()
    {
        return ToStringShort(Value);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
