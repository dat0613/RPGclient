using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MinNetforUnity;

public class MinNetTransform : MonoBehaviourMinNet
{
    [Range(1, 20)]
    public int syncPerSecond = 1;

    float lastSyncTime = 0.0f;

    [Tooltip("이 이상의 거리차이가 나면 순간이동")]
    public float teleportDistance = 5.0f;

    float syncTimeSum = 0.0f;
    uint syncCount = 0;// 지금까지 동기화한 횟수

    [Range(1.0f, 10.0f)]
    public float rotationLerp = 1.0f;

    public float SyncRateAvg
    {
        get
        {
            if(syncCount < 1 || syncTimeSum < 0.0001f)
            {
                return 0.01f;
            }
            else
            {
                return  syncTimeSum / syncCount;
            }
        }
    }

    Vector3 lastPosition = Vector3.zero;
    Quaternion lastRotation = Quaternion.identity;

    Vector3 targetPosition = Vector3.zero;
    Quaternion targetRotation = Quaternion.identity;

    public override void OnSetID(int objectID)
    {
        if(isMine)
        {
            InvokeRepeating("AutoSend", 0.0f, 1.0f / syncPerSecond);
        }
    }

    void AutoSend()
    {
        RPC("SyncPosition", MinNetRpcTarget.Others, transform.position, transform.rotation.eulerAngles);
    }

    void FixedUpdate()
    {
        if(!isMine)
        {
            Vector3 move = (targetPosition - lastPosition);
            float ratio = (Time.time - lastSyncTime) / SyncRateAvg;

            transform.position = move * ratio + lastPosition;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationLerp);
        }
    }

    public void SyncPosition(Vector3 position, Vector3 rotation)
    {
        float syncTimeGap = Time.time - lastSyncTime;

        lastSyncTime = Time.time;

        syncTimeSum += syncTimeGap;
        syncCount++;
        
        if(Vector3.SqrMagnitude(position - transform.position) < teleportDistance * teleportDistance)
        {// 순간이동 하지 않음
            // Debug.Log("순간이동 안함");
            lastPosition = targetPosition;
            targetPosition = position;

            lastRotation = targetRotation;
            targetRotation = Quaternion.Euler(rotation);
        }
        else
        {// 순간이동함
            // Debug.Log("순간이동 함");
            lastPosition = targetPosition = transform.position = position;
            lastRotation = targetRotation = transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
