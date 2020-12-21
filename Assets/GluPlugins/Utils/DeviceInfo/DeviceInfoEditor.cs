#if UNITY_EDITOR
public class DeviceInfoEditor : DeviceInfo
{
    public override string DeviceId
    {
        get { return "Unity-Editor"; }
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