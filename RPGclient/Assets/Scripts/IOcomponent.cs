using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MinNetforUnity;

public class IOcomponent : MonoBehaviourMinNet
{
    Button queryButton;
    Button tcpButton;
    Button udpButton;
    Button p2pButton;
    Button p2pAndServerButton;

    void Start()
    {
        if(isMine)
        {
            queryButton = GameObject.Find("QueryButton").GetComponent<Button>();
            queryButton.onClick.AddListener(delegate { SendQuery(); });

            tcpButton = GameObject.Find("TCPButton").GetComponent<Button>();
            tcpButton.onClick.AddListener(delegate { SendTCP(); });

            udpButton = GameObject.Find("UDPButton").GetComponent<Button>();
            udpButton.onClick.AddListener(delegate { SendUDP(); });

            p2pButton = GameObject.Find("P2PButton").GetComponent<Button>();
            p2pButton.onClick.AddListener(delegate { Sendp2pPacket(); });

            p2pAndServerButton = GameObject.Find("P2PandServerButton").GetComponent<Button>();
            p2pAndServerButton.onClick.AddListener(delegate { Sendp2pAndServerPacket(); });
        }
    }

    public override void OnSetID(int objectID)
    {

    }

    void Update()
    {
    }

    void SendQuery()
    {
        RPC("SetQuery", MinNetRpcTarget.Server, "select * from `player` limit 3;");
    }

    void SendTCP()
    {
        RPC("TCPtest", MinNetRpcTarget.AllViaServer, "TCP잘가냐?123!@#ㅁㄴㅇㄹasdf");
    }

    void SendUDP()
    {
        RPCudp("UDPtest", MinNetRpcTarget.AllViaServer, "UDP잘가냐?123!@#ㅁㄴㅇㄹasdf");
    }

    public void QueryEnd()
    {
        MinNetDebug.Log("쿼리 완료");
    }

    void Sendp2pPacket()
    {
        RPCudp("p2pTest", MinNetRpcTarget.P2Pgroup, "P2P잘가냐?!@#123asdf");
    }

    void Sendp2pAndServerPacket()
    {
        RPCudp("p2pAndServerTest", MinNetRpcTarget.P2PgroupAndServer, "P2PAndServer잘가냐?!@#123asdf");
    }


    public void p2pTest(string message)
    {
        MinNetDebug.Log("p2p : " + message);
    }

    public void UDPtest(string message)
    {
        MinNetDebug.Log("udp : " + message);
    }

    public void TCPtest(string message)
    {
        MinNetDebug.Log("tcp : " + message);
    }

    public void p2pAndServerTest(string message)
    {
        MinNetDebug.Log("p2pAndServer : " + message);
    }
}
