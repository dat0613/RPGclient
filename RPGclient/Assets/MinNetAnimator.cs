using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MinNetforUnity;

public class MinNetAnimator : MonoBehaviourMinNet
{
    public Animator animator;
    public float epsilon = 0.01f;

    void Awake()
    {
        animator = animator ?? GetComponent<Animator>();
    }

    public override void OnSetID(int objectID)
    {
        if(!isMine)
        {
            animator.applyRootMotion = false;
        }
    }

    public void SetBool(string name, bool value)
    {
        if (animator.GetBool(name) != value)
        {// 값이 변경될때만 패킷을 보냄
            animator.SetBool(name, value);
            RPC("RPCBool", MinNetRpcTarget.Others, name, value, MinNetUser.ServerTime);
        }
    }

    public void SetFloat(string name, float value)
    {
        if (Mathf.Abs(animator.GetFloat(name) - value) > epsilon)
        {// 값이 변경될때만 패킷을 보냄
            animator.SetFloat(name, value);
            RPC("RPCFloat", MinNetRpcTarget.Others, name, value, MinNetUser.ServerTime);
        }
    }

    public void SetInteger(string name, int value)
    {
        if (animator.GetInteger(name) != value)
        {
            animator.SetInteger(name, value);
            RPC("RPCInteager", MinNetRpcTarget.Others, name, value, MinNetUser.ServerTime);
        }
    }

    public void SetTrigger(string name)
    {
        if (!animator.GetBool(name))
        {
            animator.SetTrigger(name);
            RPC("RPCTrigger", MinNetRpcTarget.Others, name, MinNetUser.ServerTime);
        }
    }

    public void RPCBool(string name, bool value, int timeStamp)
    {
        animator.SetBool(name, value);

        float timeDifference = (float)(MinNetUser.ServerTime - timeStamp) * 0.001f;
        // animator.Update(timeDifference);// 타임스탬프를 이용하여 네트워크 상에서 지연된 시간만큼 애니메이션을 스킵함
    }

    public void RPCFloat(string name, float value, int timeStamp)
    {
        animator.SetFloat(name, value);

        float timeDifference = (float)(MinNetUser.ServerTime - timeStamp) * 0.001f;
        animator.Update(timeDifference);
    }

    public void RPCInteager(string name, int value, int timeStamp)
    {
        animator.SetInteger(name, value);

        float timeDifference = (float)(MinNetUser.ServerTime - timeStamp) * 0.001f;
        animator.Update(timeDifference);
    }

    public void RPCTrigger(string name, int timeStamp)
    {
        animator.SetTrigger(name);

        float timeDifference = (float)(MinNetUser.ServerTime - timeStamp) * 0.001f;
        animator.Update(timeDifference);
    }
}
