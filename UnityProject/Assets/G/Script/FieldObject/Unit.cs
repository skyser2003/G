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
			if(isJumpPossible)
			{
				JumpContinueTimer = JumpContinueTime;
			}
        }
    }

    public ObjectBalanceDataRow Info;

    public Vector2 Velocity;
    public float JumpContinueTime = 0.2f;
	protected float JumpContinueTimer;

    public Unit()
    {
        UID = NextUID++;
    }

    public void Init(int hp)
    {
        this.hp = hp;
    }

    public void SetPhysicsInfo(ObjectBalanceDataRow info)
    {
        Info = info;
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
        Velocity.y = (float)(Info.JumpForce / Info.Mass);
        GetComponent<Animator>().SetBool("StartJump", true);
    }

    public void ContinueJump(float dt)
    {
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