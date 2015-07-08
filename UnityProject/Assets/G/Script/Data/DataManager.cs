using LitJson;
using System.Collections.Generic;
using UnityEngine;

class DataManager : MonoBehaviour
{
    static public DataManager Inst;

    public ObjectBalanceDataDatabase ObjectBalanceList { get; private set; }
    public AttackPatternDataDatabase AttackPatternList { get; private set; }
    public AttackObjectDataDatabase AttackObjectList { get; private set; }

    public ObjectBalanceDataRow GetObjectBalance(string name)
    {
        foreach (var info in ObjectBalanceList.ObjectBalanceDataRow)
        {
            if (info.ID == name)
            {
                return info;
            }
        }

        return null;
    }

    public AttackPatternDataRow GetAttackPattern(string name)
    {
        foreach (var info in AttackPatternList.AttackPatternDataRow)
        {
            if (info.ID == name)
            {
                return info;
            }
        }

        return null;
    }

    public AttackObjectDataRow GetAttackObject(string name)
    {
        foreach (var info in AttackObjectList.AttackObjectDataRow)
        {
            if (info.ID == name)
            {
                return info;
            }
        }

        return null;
    }

    private void Awake()
    {
        Inst = this;

        ObjectBalanceList = new ObjectBalanceDataDatabase();
        AttackPatternList = new AttackPatternDataDatabase();
        AttackObjectList = new AttackObjectDataDatabase();

        var json = Resources.Load<TextAsset>("ObjectBalanceData/ObjectBalanceData");
        ObjectBalanceList.ObjectBalanceDataRow = new List<ObjectBalanceDataRow>(JsonMapper.ToObject<ObjectBalanceDataRow[]>(json.text));

        json = Resources.Load<TextAsset>("AttackPatternData/AttackPatternData");
        AttackPatternList.AttackPatternDataRow = new List<AttackPatternDataRow>(JsonMapper.ToObject<AttackPatternDataRow[]>(json.text));
        
        json = Resources.Load<TextAsset>("AttackObjectData/AttackObjectData");
        AttackObjectList.AttackObjectDataRow = new List<AttackObjectDataRow>(JsonMapper.ToObject<AttackObjectDataRow[]>(json.text));
    }
}