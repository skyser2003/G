using LitJson;
using System.Collections.Generic;
using UnityEngine;

class DataManager : MonoBehaviour
{
    static public DataManager Inst;

    public Dictionary<string, MonsterInfo> monsterList = new Dictionary<string, MonsterInfo>();
    public Dictionary<string, AttackInfo> attackList = new Dictionary<string, AttackInfo>();

    public AttackInfo GetAttackInfo(string name)
    {
        AttackInfo info;
        attackList.TryGetValue(name, out info);
        return info;
    }

    public MonsterInfo GetMonsterInfo(string name)
    {
        MonsterInfo info;
        monsterList.TryGetValue(name, out info);
        return info;
    }

    private void Awake()
    {
        Inst = this;

        InitAttack();
        InitMonster();
    }

    private void InitAttack()
    {
        var json = Resources.Load<TextAsset>("attack");
        var obj = JsonMapper.ToObject<AttackInfo[]>(json.text);

        foreach (var info in obj)
        {
            attackList.Add(info.name, info);
        }
    }

    private void InitMonster()
    {
        var json = Resources.Load<TextAsset>("monster");
        var obj = JsonMapper.ToObject<MonsterInfo[]>(json.text);

        foreach (var info in obj)
        {
            monsterList.Add(info.name, info);
        }
    }
}