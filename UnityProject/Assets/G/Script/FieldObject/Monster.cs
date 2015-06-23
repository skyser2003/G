using UnityEngine;

class Monster : MonoBehaviour
{
    private MonsterInfo info;

    private void Start()
    {
        MonsterManager.Inst.Add(this);

        info = DataManager.Inst.GetMonsterInfo(name);
        GetComponent<Unit>().Init(info.hp);

        var physics = new UnitPhysicsInfo();
        physics.weight = 10;
        physics.moveAcceleration = 0.5f;
        physics.maxMoveSpeed = 1.0f;
        physics.moveFriction = 0.25f;
        physics.jumpSpeed = 1.0f;
        physics.jumpFriction = 0.0f;
        GetComponent<Unit>().SetPhysicsInfo(physics);
    }
}