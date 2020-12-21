#if UNITY_ANDROID && !UNITY_EDITOR
public class DeviceInfoAndroid : DeviceInfo
{
    private const string NATIVE_CLASS = "com.example.android_plugin.AndroidUtil";

    public override string DeviceId
    {
        get
        {
            return AndroidUtil.CallStaticMethod<string>(NATIVE_CLASS, "getAndroidId", new object[] { AndroidUtil.ContextForAndroid });
        }
    }

    public override string StoragePath
    {
        get
        {
            return AndroidUtil.CallStaticMethod<string>(NATIVE_CLASS, "getPrivateAppDataFolder", new object[] { AndroidUtil.ContextForAndroid });
        }
    }
}
#endif