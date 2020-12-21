using UnityEngine;

public class SaveData
{
    private const string KEY = "dpzB18Hs+PAKd+egxzUJ/Vf6MIypL+hDbsFXi0IVJsU=";
    private const string IV = "dqm9cJbOM3wAhS1yWlyNpA==";

    private static SaveData _instance;

    public static SaveData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveData();
            }
            return _instance;
        }
    }

    public void WriteData(string dataToWrite)
    {
        if (!string.IsNullOrEmpty(dataToWrite))
        {
            byte[] encryptedBytes = Util.EncryptData(dataToWrite, KEY, IV);

            if (encryptedBytes != null)
            {
                byte[] compressedBytes = Util.CompressToBytes(encryptedBytes);

                if (compressedBytes != null)
                {
                    Util.WriteFile(FilePath, compressedBytes);
                    Debug.LogFormat("SaveData->WriteData->>Data saved to {0}", FilePath);
                }
                else
                {
                    Debug.Log("SaveData->WriteData->>Error in Compression");
                }
            }
            else
            {
                Debug.Log("SaveData->WriteData->>Error in Encryption");
            }
        }
        else
        {
            Debug.Log("SaveData->WriteData->>input data is null");
        }
    }

    public string ReadData()
    {
        byte[] savedData = Util.ReadFile_Bytes(FilePath);

        if (savedData != null)
        {
            byte[] decompressedbytes = Util.DecompressBytes(savedData);

            if (decompressedbytes != null)
            {
                string decryptedData = Util.DecryptBytes(decompressedbytes, KEY, IV);

                if(decryptedData != null)
                {
                    return decryptedData;
                }
                else
                {
                    Debug.Log("SaveData->ReadData->>Error in Decryption");
                }
            }
            else
            {
                Debug.Log("SaveData->ReadData->>Error in Decompression");
            }
        }
        else
        {
            Debug.Log("SaveData->ReadData->>Error in reading last saved data or no data present");
        }

        return null;
    }

    private string FilePath
    {
        get
        {
            return DeviceInfo.Instance.StoragePath + "/player_data.bin";
        }
    }

}
