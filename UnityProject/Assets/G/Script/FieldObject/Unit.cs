using UnityEngine;

class Unit : MonoBehaviour
{
    static private int NextUID = 0;

    private bool isDead = false;
    private int hp;
    private UnitPhysicsInfo physics;

    public int UID { get; private set; }
    public int HP { get { return hp; } }
    public int Group { get; set; }

    public UnitPhysicsInfo Physics { get { return physics; } }

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
        this.physics = physics;
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
    }

    private void Start()
    {
        hp = 1000;
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
    }
}