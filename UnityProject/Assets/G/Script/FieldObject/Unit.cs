using UnityEngine;

class Unit : MonoBehaviour
{
    static private int NextUID = 0;

    private bool isDead = false;
    private int hp;

    public int UID { get; private set; }
    public int HP { get { return hp; } }
    public int Group { get; set; }

    public Unit()
    {
        UID = NextUID++;
    }

    public void Init(MonsterInfo info)
    {
        hp = info.hp;
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