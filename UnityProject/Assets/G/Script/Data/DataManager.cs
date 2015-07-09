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

        JsonData data;

        var json = Resources.Load<TextAsset>("ObjectBalanceData/ObjectBalanceData");
        data = JsonMapper.ToObject(json.text);
        ObjectBalanceList.ObjectBalanceDataRow = new List<ObjectBalanceDataRow>(JsonMapper.ToObject<ObjectBalanceDataRow[]>(data["ObjectBalanceDataRow"].ToJson()));

        json = Resources.Load<TextAsset>("AttackPatternData/AttackPatternData");
        data = JsonMapper.ToObject(json.text);
        AttackPatternList.AttackPatternDataRow = new List<AttackPatternDataRow>(JsonMapper.ToObject<AttackPatternDataRow[]>(data["AttackPatternDataRow"].ToJson()));
        
        json = Resources.Load<TextAsset>("AttackObjectData/AttackObjectData");
        data = JsonMapper.ToObject(json.text);
        AttackObjectList.AttackObjectDataRow = new List<AttackObjectDataRow>(JsonMapper.ToObject<AttackObjectDataRow[]>(data["AttackObjectDataRow"].ToJson()));
    }
}