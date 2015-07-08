using UnityEngine;

class Monster : MonoBehaviour
{
    private ObjectBalanceDataRow info;

    private void Start()
    {
        MonsterManager.Inst.Add(this);

        info = DataManager.Inst.GetObjectBalance("ID_2");
        GetComponent<Unit>().SetPhysicsInfo(info);
    }
}