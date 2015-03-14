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

    private MOVE_STATE moveState;
    private AbstractFSM msFSM = null;

    private ACTION_STATE actionState;
    private AbstractFSM asFSM = null;

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
        if(msFSM != null)
        {
            msFSM.OnUpdate();
        }

        if(asFSM != null)
        {
            asFSM.OnUpdate();
        }
    }

    private void Move(float speed)
    {
        this.speed = speed;
        SetMoveState(MOVE_STATE.MS_WALK);
    }
    
    private void SetMoveState(MOVE_STATE state)
    {
        var oldFSM = msFSM;

        moveState = state;
        switch (state)
        {
            case MOVE_STATE.MS_IDLE:
                {
                    msFSM = new MoveFSM_Idle();
                }
                break;
            case MOVE_STATE.MS_WALK:
                {
                    msFSM = new MoveFSM_Walk();
                }
                break;
            default:
                {
                    msFSM = null;
                }
                break;
        }

        if (oldFSM != null)
        {
            oldFSM.OnEnd();
        }

        if (msFSM != null)
        {
            msFSM.Init(this);
            msFSM.OnBegin();
        }
    }

    private void SetActionState(ACTION_STATE state)
    {
        var oldFSM = asFSM;

        actionState = state;
        switch (state)
        {
            case ACTION_STATE.AS_IDLE:
                {
                    asFSM = new AttackFSM_Idle();
                }
                break;
            case ACTION_STATE.AS_ATTACK:
                {
                    asFSM = new AttackFSM_BaseAttack();
                }
                break;
            default:
                {
                    asFSM = null;
                }
                break;
        }

        if (oldFSM != null)
        {
            oldFSM.OnEnd();
        }

        if (asFSM != null)
        {
            asFSM.Init(this);
            asFSM.OnBegin();
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
        SetMoveState(MOVE_STATE.MS_IDLE);
        SetActionState(ACTION_STATE.AS_IDLE);
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
        SetActionState(ACTION_STATE.AS_ATTACK);
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