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

    public float attackPreDelay = 0.2f;
    public float attackPostDelay = 1.0f;
    public float strongAttackDelay = 1.0f;
    public float weakAttackSpeed = 1.0f;
    public float strongAttackSpeed = 3.0f;
    public float attackCooltime = 0.0f;
    public int strongAttackBestCharge = 5;

    private void Start()
    {
        walk = GetComponent<WalkObject>();
        unit = GetComponent<Unit>();
        attackCooltime = 0.0f;

        var info = DataManager.Inst.GetObjectBalance("ID_1");
        unit.SetPhysicsInfo(info);

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
        Walk.Direction = direction;
        SetState(STATE.WALK);
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
        if (walk.Velocity >= 0.0f)
        {
            Move(-1);
        }
    }

    public void MoveRight()
    {
        if (walk.Velocity <= 0.0f)
        {
            Move(1);
        }
    }

    public void Stop()
    {
        if (walk.Speed != 0)
        {
            Walk.Stop();
            SetState(STATE.IDLE);
        }
    }

    public void Attack()
    {
        if (attackCooltime > 0.0f)
        {
            return;
        }

        SetState(STATE.ATTACK);
    }

    public void StrongAttack(float chargeTime)
    {
        if (attackCooltime > 0.0f)
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