using UnityEngine;

class PlayerCharacter : MonoBehaviour
{
    private STATE state;
    private AbstractFSM FSM = null;
    private WalkObject walk;
    private Unit unit;

    public WalkObject Walk { get { return walk; } }
    public bool IsJumping { get; set; }

    public float WalkVelocity
    {
        get
        {
            return walk == null ? 0.0f : walk.Velocity;
        }
    }

    public float WalkSpeed { get { return walk == null ? 0.0f : walk.Speed; } }

    public float attackPreDelay = 0.2f;
    public float attackPostDelay = 1.0f;
    public float strongAttackDelay = 1.0f;
    public float attackSpeed = 1.0f;
    public float attackCooltime = 0.0f;

    private void Start()
    {
        walk = GetComponent<WalkObject>();
        unit = GetComponent<Unit>();
        attackCooltime = 0.0f;

        var physics = new UnitPhysicsInfo();
        physics.weight = 1;
        physics.moveAcceleration = 0.5f;
        physics.maxMoveSpeed = 1.0f;
        physics.moveFriction = 0.25f;
        physics.jumpSpeed = 5.0f;
        physics.jumpContinueSpeed = 5.0f;
        physics.jumpFriction = 0.0f;
        unit.SetPhysicsInfo(physics);

        Stop();
    }

    private void Update()
    {
        // FSM
        if (FSM != null)
        {
            FSM.OnUpdate();
        }

        attackCooltime -= Time.deltaTime;
    }

    private void Move(int direction)
    {
        if (state == STATE.ATTACK)
        {
            return;
        }

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
            case STATE.STRONG_ATTACK:
                {
                    FSM = new FSM_StrongAttack();
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
        if (walk.Direction != -1)
        {
            Move(-1);
        }
    }

    public void MoveRight()
    {
        if (walk.Direction != 1)
        {
            Move(1);
        }
    }

    public void Stop()
    {
        if (walk.Direction != 0)
        {
            Move(0);
        }
    }

    public void Attack()
    {
        if(attackCooltime > 0.0f)
        {
            return;
        }

        SetState(STATE.ATTACK);
    }

    public void StrongAttack(float chargeTime)
    {
        if(attackCooltime > 0.0f)
        {
            return;
        }

        SetState(STATE.STRONG_ATTACK);

        if (FSM.GetType() == typeof(FSM_StrongAttack))
        {
            var strAtkFSM = (FSM_StrongAttack)FSM;
            strAtkFSM.ChargeTime = chargeTime;
        }
    }
}