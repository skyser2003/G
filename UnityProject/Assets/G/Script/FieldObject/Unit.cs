using UnityEngine;

class Unit : MonoBehaviour
{
    static private int NextUID = 0;

    private bool isDead = false;
    private int hp;

    public int UID { get; private set; }
    public int HP { get { return hp; } }
    public int Group { get; set; }
    public UnitPhysicsInfo Physics;

    public Vector2 Velocity;

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