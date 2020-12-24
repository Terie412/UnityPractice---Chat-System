using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T>: UnityEngine.Object where T:UnityEngine.Object
{
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    Debug.LogError("Failed to get instance of " + typeof(T));
                }
            }
            return instance;
        }
    } 
}
