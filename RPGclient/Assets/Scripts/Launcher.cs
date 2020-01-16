using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MinNetforUnity;

public class Launcher : MonoBehaviourMinNetCallBack
{
    public GameObject prefab;
    public Text text;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // MinNetDebug.FindContent();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        MinNetUser.ConnectToServer("192.168.137.1", 8200, 8201);
        // MinNetUser.ConnectToServer("10.230.12.176", 8200, 8201);
        // MinNetUser.ConnectToServer("34.97.67.118", 8200, 8201);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        text.text = MinNetUser.ServerTime.ToString() + " : " + MinNetUser.Ping;
        // Debug.Log(MinNetUser.ServerTime);
    }

    public override void UserEnterRoom(int roomNumber, string roomName)
    {
        if(roomName == "Main")
        {
            Debug.Log("Main 룸에 입장");
            MinNetUser.Instantiate(prefab, new Vector3(0.0f, 5.0f, 0.0f), Quaternion.identity);
            //text.text = MinNetUser.RemoteEndpoint.Address.ToString() + " : " + MinNetUser.RemoteEndpoint.Port.ToString();
        }
    }
}
