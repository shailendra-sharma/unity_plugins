#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;

public class DeviceInfoIOS : DeviceInfo
{
    [DllImport("__Internal")]
    public static extern string _GetUUID();

    public override string DeviceId
    {
        get
        {
            return _GetUUID();
        }
    }

    public override string StoragePath
    {
        get
        {
            return UnityEngine.Application.persistentDataPath;
        }
    }

}
#endif