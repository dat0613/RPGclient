using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MinNetforUnity;

public class PlayerController : MonoBehaviourMinNet
{
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    float moveSpeed = 10.0f;

    Vector3 moveDirection = Vector3.zero;

    void Awake()
    {
        controller = controller ?? GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        // if(isMine)
        {
            PlayerInput();
            PlayerMove();
        }
    }

    void PlayerInput()
    {
        moveDirection = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
            moveDirection.z += 1.0f;

        if(Input.GetKey(KeyCode.S))
            moveDirection.z -= 1.0f;

        if(Input.GetKey(KeyCode.A))
            moveDirection.x -= 1.0f;

        if(Input.GetKey(KeyCode.D))
            moveDirection.x += 1.0f;

        
    }

    void PlayerMove()
    {
        transform.position += transform.rotation * moveDirection * moveSpeed * Time.fixedDeltaTime;
    }
}
