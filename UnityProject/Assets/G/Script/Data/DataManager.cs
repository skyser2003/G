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

	public List<string> GetAttackPatternStringList(string _id)
	{
		ObjectBalanceDataRow data = GetObjectBalance(_id);
		if(data != null)
		{
			return data.AttackPatternList;
		}
		return new List<string>();
	}

	public ObjectStat GetObjectStat(string _name)
	{
		ObjectBalanceDataRow data = GetObjectBalance(_name);

		ObjectStat stat = new ObjectStat();
		if(data != null)
		{
			stat.Mass = (float)data.Mass;
			stat.Attack = (float)data.Attack;
			stat.Defense = (float)data.Defense;
			stat.Health = (float)data.Health;
			stat.MaxMovementSpeed = (float)data.MaxMovementSpeed;
			stat.MoveForce = (float)data.MoveForce;
			stat.MoveResistance = (float)data.MoveResistance;
			stat.JumpForce = (float)data.JumpForce;
			stat.MaxJumpCount = data.MaxJumpCount;
			stat.MaxFallSpeed = (float)data.MaxFallSpeed;
			stat.GravityResistance = (float)data.GravityResistance;
		}

		return stat;
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

	public GAttackPatternObject GetGAttackPattern(string _id)
	{
		AttackPatternDataRow data = GetAttackPattern(_id);
		if(data != null)
		{
			GAttackPatternObject gendata = new GAttackPatternObject();
			gendata.ID = data.ID;
			gendata.Name = data.Name;
			gendata.TotalTime = (float)data.TotalTime;
			gendata.CoolTime = (float)data.CoolTime;
			gendata.AttackPatternObjectTimeList = new List<float>();
			for(int iter = 0; iter < data.AttackObjectTimeList.Count; iter++)
			{
				gendata.AttackPatternObjectTimeList.Add((float)data.AttackObjectTimeList[iter]);
			}
			gendata.AttackObjectDataList = data.AttackObjectDataList;
			gendata.Reset();
			return gendata;
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