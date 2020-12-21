using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AndroidUtil
{
    public static ReturnType CallStaticMethod<ReturnType>(string className, string methodName, params object[] parameters)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass(className);
            ReturnType result = jc.CallStatic<ReturnType>(methodName, parameters);
            jc.Dispose();
            return result;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
#endif
        return default(ReturnType);
    }

    public static object GetStaticJavaObject(string className, string variableName)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            AndroidJavaClass javaClass = new AndroidJavaClass(className);
            if (javaClass != null)
            {
                object obj = javaClass.GetStatic<AndroidJavaObject>(variableName);
                javaClass.Dispose();
                return obj;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
#endif
        return default(object);
    }

    public static object ContextForAndroid
    {
        get
        {
            return GetStaticJavaObject("com.unity3d.player.UnityPlayer", "currentActivity");
        }
    }
}
