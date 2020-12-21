using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    private GUIStyle lblStyle;
    private GUIStyle btnStyle;

    private string deviceUUID;

    private string inputText = "";
    private string savedData = "";

    void Start()
    {
        deviceUUID = string.Format("UUID = {0}", DeviceInfo.Instance.DeviceId);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnGUI()
    {
        GUIStyle lblStyle = new GUIStyle { fontSize = 50 };
        GUIStyle btnStyle = new GUIStyle("Button") { fontSize = 30 };
        GUIStyle textAreaStyle = new GUIStyle("TextArea") { fontSize = 50 };

        lblStyle.normal.textColor = Color.white;

        GUI.Label(new Rect(20, 50, Screen.width, 50), deviceUUID, lblStyle);

        GUI.Label(new Rect(20, 100, Screen.width, 50), "Enter data to save", lblStyle);

        inputText = GUI.TextArea(new Rect(20, 150, Screen.width - 20, 200), inputText, textAreaStyle);

        if (GUI.Button(new Rect(20, 350, 400, 50), "Save Player Data", btnStyle))
        {
            SaveData.Instance.WriteData(inputText);
        }

        if (GUI.Button(new Rect(20, 450, 400, 50), "View Saved Player Data", btnStyle))
        {
            savedData = SaveData.Instance.ReadData() ?? "No Data Found";
        }

        GUI.Label(new Rect(20, 550, Screen.width, 200), savedData, lblStyle);
    }
}
