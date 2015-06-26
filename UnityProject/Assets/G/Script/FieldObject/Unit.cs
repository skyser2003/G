using UnityEngine;

class Unit : MonoBehaviour
{
    static private int NextUID = 0;

    private bool isDead = false;
    private int hp;
    private bool isJumpPossible;

    public int UID { get; private set; }
    public int HP { get { return hp; } }
    public int Group { get; set; }
    public bool IsJumping { get; set; }
    public bool IsJumpPossible
    {
        get
        {
            return isJumpPossible;
        }
        set
        {
            isJumpPossible = value;
            JumpContinueTime = 0.2f;
        }
    }

    public UnitPhysicsInfo Physics;
    public Vector2 Velocity;
    public float JumpContinueTime;

    public Unit()
    {
        UID = NextUID++;
    }

    public void Init(int hp)
    {
        this.hp = hp;
    }

    public void SetPhysicsInfo(UnitPhysicsInfo physics)
    {
        Physics = physics;
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
    }

    public void Jump()
    {
        if (IsJumpPossible == false)
        {
            return;
        }

        IsJumping = true;
        Velocity.y = Physics.jumpSpeed / Physics.weight;
    }

    public void ContinueJump(float dt)
    {
        Debug.Log(JumpContinueTime);

        if (IsJumping == false || JumpContinueTime <= 0.0f)
        {
            return;
        }

        Velocity.y += Physics.jumpContinueSpeed / Physics.weight * dt;
        JumpContinueTime -= dt;
    }

    private void Start()
    {
        hp = 1000;
        Velocity = new Vector2();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            isDead = true;
        }

        if (isDead == true)
        {
            Object.Destroy(gameObject);
        }

        var delta = Velocity * Time.deltaTime;
        transform.localPosition += new Vector3(delta.x, delta.y, 0.0f);
    }
}