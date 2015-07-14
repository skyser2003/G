using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GAttackComponentBase {

	public ObjectStat Stat;
	public List<GAttackPatternObject> AttackPatternList = new List<GAttackPatternObject>();

	public void SetStat(ObjectStat _stat)
	{
		Stat = _stat;
	}

	public void Init(List<string> _idlist)
	{
		AttackPatternList.Clear();
		//Debug.Log("Init attack: " + _idlist.Count);
		for(int iter = 0; iter < _idlist.Count; iter++)
		{
			//Debug.Log("Try add attack pattern id: " + _idlist[iter]);
			GAttackPatternObject data = DataManager.Inst.GetGAttackPattern(_idlist[iter]);
			if(data != null)
			{
				AttackPatternList.Add(data);
			}
		}
	}

	public virtual bool PlayAttack(int _index)
	{
		//Debug.Log("try attack: " + _index);
		if(_index < AttackPatternList.Count)
		{
			if(AttackPatternList[_index].CoolIsReady())
			{
				AttackPatternList[_index].Play();
				return true;
			}
		}

		return false;
	}

	public virtual void Process(float _timer)
	{
		for(int iter = 0; iter < AttackPatternList.Count; iter++)
		{
			GAttackPatternObject curobject = AttackPatternList[iter];
			curobject.Process(_timer);
		}
	}
}

[System.Serializable]
public class GAttackPatternObject
{
	public string ID;
	public string Name;
	public float TotalTime;
	public float CurTimer;
	public float CoolTime;
	public float CoolTimer;

	public List<float> AttackPatternObjectTimeList = new List<float>();
	protected List<bool> AttackPatternObjectFlagList = new List<bool>();
	public List<string> AttackObjectDataList = new List<string>();

	public bool IsPlaying;

	public void Reset()
	{
		AttackPatternObjectFlagList.Clear();
		CoolTimer = CoolTime;
		CurTimer = 0f;
	}

	public void Play()
	{
		AttackPatternObjectFlagList.Clear();
		IsPlaying = true;
		CurTimer = 0f;
		CoolTimer = 0f;
	}

	public void Process(float _timer)
	{
		CoolTimer += _timer;
		if(IsPlaying)
		{
			CurTimer += _timer;

			//check attackpattern and create
			for(int iter = 0; iter < AttackPatternObjectTimeList.Count; iter++)
			{
				float curratio = AttackPatternObjectTimeList[iter];
				if(CurTimer / TotalTime > curratio && AttackPatternObjectFlagList.Count <= iter)
				{
					//if ratio is done and didn't created before, create attack object
					AttackPatternObjectFlagList.Add(true);
					Debug.Log("Create attack object id: " + AttackObjectDataList[iter]);
				}
			}

			if(CurTimer > TotalTime)
			{
				IsPlaying = false;
				CoolTimer = 0f;
			}
		}
	}

	public bool CoolIsReady()
	{
		if(CoolTimer > CoolTime && !IsPlaying)
		{
			return true;
		}

		return false;
	}
}
