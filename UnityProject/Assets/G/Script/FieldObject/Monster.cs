using UnityEngine;

class Monster : MonoBehaviour
{
    private MonsterInfo info;

    private void Start()
    {
        MonsterManager.Inst.Add(this);

        info = DataManager.Inst.GetMonsterInfo(name);
    }
}