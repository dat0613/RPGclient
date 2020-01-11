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
        MinNetDebug.FindContent();
    }

    void Start()
    {
        MinNetUser.ConnectToServer("192.168.137.1", 8200, 8201);
        // MinNetUser.ConnectToServer("10.230.12.176", 8200, 8201);
        // MinNetUser.ConnectToServer("34.97.67.118", 8200, 8201);
    }

    public override void UserEnterRoom(int roomNumber, string roomName)
    {
        if(roomName == "Main")
        {
            Debug.Log("Main 룸에 입장");
            MinNetUser.Instantiate(prefab);
            //text.text = MinNetUser.RemoteEndpoint.Address.ToString() + " : " + MinNetUser.RemoteEndpoint.Port.ToString();
        }
    }
}
