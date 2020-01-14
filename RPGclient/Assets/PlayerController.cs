using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MinNetforUnity;

public class PlayerController : MonoBehaviourMinNet
{
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    Animator animator;
    [SerializeField]
    AttackRangeCollider attackRange;
    [SerializeField]
    float lerp = 8.0f;
    [SerializeField]
    float airborneCutline = -1.0f;

    Vector3 moveDirection = Vector3.zero;
    Vector3 playerRotation = Vector3.zero;
    bool airborne = false;
    bool attack = false;

    void Awake()
    {
        controller = controller ?? GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        // if(isMine)
        {
            PlayerInput();
        }
    }

    void PlayerInput()
    {
        BasicMovement();
        SpecialMovement();
        AttackMovement();
        FinalMovement();
    }

    void BasicMovement()// 앞뒤좌우 움직임
    {
        moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            moveDirection.z += 1.0f;

        if (Input.GetKey(KeyCode.S))
            moveDirection.z -= 1.0f;

        if (Input.GetKey(KeyCode.A))
            moveDirection.x -= 1.0f;

        if (Input.GetKey(KeyCode.D))
            moveDirection.x += 1.0f;

        moveDirection = moveDirection.normalized;

        float moveSpeed = 0.0f;

        if (Mathf.Abs(moveDirection.z) > 0.1f || Mathf.Abs(moveDirection.x) > 0.1f)
        {// 플레이어의 움직임이 입력됨
            moveSpeed = 0.3f;
            playerRotation.y = FollowCamera.Instance.vector.y + Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;// 앞뒤좌우 움직임
        }
        else
        {
            moveSpeed = 0.0f;
        }

        animator.SetFloat("MoveSpeed", moveSpeed);
    }

    void SpecialMovement()// 구르기, 점프 등 특수 동작
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(!airborne && !attack)
                animator.SetTrigger("Roll");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!airborne && !attack)
                animator.SetTrigger("Jump");
        }

        if (controller.velocity.y < airborneCutline || controller.velocity.y > -airborneCutline)
        {// 공중에 떠 있음
            if (!airborne)
            {
                airborne = true;
                animator.SetBool("Airborne", airborne);
            }
        }
        else
        {
            if (airborne)
            {
                airborne = false;
                animator.SetBool("Airborne", airborne);
            }
        }
    }

    void AttackMovement()// 공격, 스킬 등
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if (!attack)
            {
                attack = true;
                animator.SetBool("Attack", attack);
            }
        }
        else
        {
            if (attack)
            {
                attack = false;
                animator.SetBool("Attack", attack);
            }
        }
    }

    void FinalMovement()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, playerRotation.y, 0.0f), lerp * Time.fixedDeltaTime);
    }

    public void Attack()
    {
        // var objectList = attackRange.GetGameObjectInCollider().;
    }
}
