using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class PlayerCharacter : MonoBehaviour
{
    private float speed = 0.0f;
    private bool jumpPossible = true;
    private bool isJumping = false;
    private float jumpTime = 0.0f;

    private STATE state;
    private AbstractFSM FSM = null;

    private void Start()
    {
        Stop();
    }

    private void Update()
    {
        var velocity = new Vector3();
        velocity.x = speed;

        if(isJumping == true)
        {
            jumpTime -= Time.deltaTime;
            if(jumpTime <= 0.0f)
            {
                isJumping = false;
            }

            velocity.y = 1.0f;
        }

        var dx = velocity * Time.deltaTime;
        transform.localPosition += new Vector3(dx.x, dx.y, 0.0f);

        // FSM
        if(FSM != null)
        {
            FSM.OnUpdate();
        }
    }

    private void Move(float speed)
    {
        this.speed = speed;
        SetState(STATE.WALK);
    }
    
    private void SetState(STATE state)
    {
        var oldFSM = FSM;

        this.state = state;
        switch (state)
        {
            case STATE.IDLE:
                {
                    FSM = new FSM_Idle();
                }
                break;
            case STATE.ATTACK:
                {
                    FSM = new FSM_BaseAttack();
                }
                break;
            case STATE.WALK:
                {
                    FSM = new FSM_Walk();
                }
                break;
            default:
                {
                    FSM = null;
                }
                break;
        }

        if (oldFSM != null)
        {
            oldFSM.OnEnd();
        }

        if (FSM != null)
        {
            FSM.Init(this);
            FSM.OnBegin();
        }
    }
    
    public void MoveLeft()
    {
        Move(-1);
    }

    public void MoveRight()
    {
        Move(1);
    }

    public void Stop()
    {
        Move(0);
        SetState(STATE.IDLE);
    }

    public void Jump()
    {
        if(jumpPossible == false)
        {
            return;
        }

        isJumping = true;
        jumpTime = 1.0f;
    }

    public void Attack()
    {
        SetState(STATE.ATTACK);
    }
   
    public float GetSpeed()
    {
        return speed;
    }

    public void SetJumpPossible(bool isPossible)
    {
        jumpPossible = isPossible;
    }
}