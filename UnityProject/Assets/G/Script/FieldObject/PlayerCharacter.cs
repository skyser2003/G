using UnityEngine;

class PlayerCharacter : MonoBehaviour
{
    private bool jumpPossible = true;
    private bool isJumping = false;
    private float jumpTime = 0.0f;

    private STATE state;
    private AbstractFSM FSM = null;
    private WalkObject walk;

    public WalkObject Walk { get { return walk; } }

    public float WalkVelocity
    {
        get
        {
            return walk == null ? 0.0f : walk.Velocity;
        }
    }

    public float WalkSpeed { get { return walk == null ? 0.0f : walk.Speed; } }

    private void Start()
    {
        walk = GetComponent<WalkObject>();
                
        var physics = new UnitPhysicsInfo();
        physics.weight = 10;
        physics.moveAcceleration = 0.5f;
        physics.maxMoveSpeed = 1.0f;
        physics.moveFriction = 0.25f;
        physics.jumpSpeed = 1.0f;
        physics.jumpFriction = 0.0f;
        GetComponent<Unit>().SetPhysicsInfo(physics);

        Stop();
    }

    private void Update()
    {
        var velocity = new Vector3();

        if (isJumping == true)
        {
            jumpTime -= Time.deltaTime;
            if (jumpTime <= 0.0f)
            {
                isJumping = false;
            }

            velocity.y = GetComponent<Unit>().Physics.jumpSpeed;
        }

        var dx = velocity * Time.deltaTime;
        transform.localPosition += new Vector3(dx.x, dx.y, 0.0f);

        // FSM
        if (FSM != null)
        {
            FSM.OnUpdate();
        }
    }

    private void Move(int direction)
    {
        Walk.Direction = direction;

        if (direction == 0)
        {
            SetState(STATE.IDLE);
        }
        else
        {
            SetState(STATE.WALK);
        }
    }

    public void SetState(STATE state)
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
        if(walk.Direction != -1)
        {
            Move(-1);
        }
    }

    public void MoveRight()
    {
        if(walk.Direction != 1)
        {
            Move(1);
        }
    }

    public void Stop()
    {
        if(walk.Direction != 0)
        {
            Move(0);
        }
    }

    public void Jump()
    {
        if (jumpPossible == false)
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

    public void SetJumpPossible(bool isPossible)
    {
        jumpPossible = isPossible;
    }
}