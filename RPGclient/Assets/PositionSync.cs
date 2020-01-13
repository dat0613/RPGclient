using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MinNetforUnity;

public class PositionSync : MonoBehaviourMinNet
{
    [SerializeField]
    int syncPerSecond = 5;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public override void OnSetID(int objectID)
    {
        // if(isMine)
        // {
        //     InvokeRepeating("AutoSend", 0.0f, (1000/syncPerSecond));
        // }
    }
    
    void AutoSend()
    {
        RPCudp("SetPosition", MinNetRpcTarget.Others, transform.position);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
