using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeviceInfo
{
    private static DeviceInfo _instance;

    private static DeviceInfo Initialize()
    {
#if UNITY_IOS && !UNITY_EDITOR
        return new DeviceInfoIOS();
#elif UNITY_ANDROID && !UNITY_EDITOR
        return new DeviceInfoAndroid();
#else
        return new DeviceInfoEditor();
#endif
    }

    public static DeviceInfo Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Initialize();
            }
            return _instance;
        }
    }

    public abstract string DeviceId { get; }
    public abstract string StoragePath { get; }

}
