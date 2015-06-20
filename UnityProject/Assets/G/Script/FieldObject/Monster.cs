using UnityEngine;

class Monster : MonoBehaviour
{
    private void Start()
    {
        MonsterManager.Inst.Add(this);
    }
}