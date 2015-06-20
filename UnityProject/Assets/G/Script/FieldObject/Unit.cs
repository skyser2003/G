using UnityEngine;

class Unit : MonoBehaviour
{
    private bool isDead = false;
    private int hp;

    public int HP { get { return hp; } }
    public int Group { get; set; }

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