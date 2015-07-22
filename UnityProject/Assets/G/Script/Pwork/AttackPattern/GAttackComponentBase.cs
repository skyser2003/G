using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GAttackComponentBase : MonoBehaviour {

	public Transform PivotTransform;
	public ObjectStat Stat;

	public string ID;
	public string Name;
	public float TotalTime;
	public float CurTimer;
	public float CoolTime;
	public float CoolTimer;
	
	public List<float> AttackPatternObjectTimeList = new List<float>();
	protected List<bool> AttackPatternObjectFlagList = new List<bool>();
	//public List<string> AttackObjectDataList = new List<string>();
	public List<Object> AttackObjectPrefabList = new List<Object>();

	public bool IsPlaying;
	
	public float DamageMulti = 1f;
	public float AttackSpeed = 1f;
	public List<int> HitGroupIDList = new List<int>();

	public void SetStat(ObjectStat _stat)
	{
		Stat = _stat;
	}

	public void Init()
	{
		GAttackPatternObject gpatterndata = DataManager.Inst.GetGAttackPattern(ID);
		ID = gpatterndata.ID;
		Name = gpatterndata.Name;
		TotalTime = gpatterndata.TotalTime;
		CoolTime = gpatterndata.CoolTime;
		AttackPatternObjectTimeList = gpatterndata.AttackPatternObjectTimeList;
		Reset ();
		//AttackObjectDataList = gpatterndata.AttackObjectDataList;
	}

	public virtual bool PlayAttack()
	{
		//Debug.Log("try attack: " + _index);
		if(CoolIsReady())
		{
			//Debug.Log("ATtack: ");
			Play();
			return true;
		}

		return false;
	}

	public virtual bool CanAttack()
	{
		if(CoolIsReady())
		{
			//AttackPatternList[_index].Play();
			return true;
		}

		return false;
	}

	public virtual void Reset()
	{
		AttackPatternObjectFlagList.Clear();
		CoolTimer = CoolTime;
		CurTimer = 0f;
	}
	
	public virtual void Play()
	{
		//Debug.Log("Play attack");
		AttackPatternObjectFlagList.Clear();
		IsPlaying = true;
		CurTimer = 0f;
		CoolTimer = 0f;
	}

	public virtual void Process(float _timer)
	{
		CoolTimer += _timer;
		if(IsPlaying)
		{
			CurTimer += _timer * AttackSpeed;
			
			//check attackpattern and create
			for(int iter = 0; iter < AttackPatternObjectTimeList.Count; iter++)
			{
				float curratio = AttackPatternObjectTimeList[iter];
				if(CurTimer / TotalTime > curratio && AttackPatternObjectFlagList.Count <= iter)
				{
					//if ratio is done and didn't created before, create attack object
					AttackPatternObjectFlagList.Add(true);
					//Debug.Log("Create attack object id: " + AttackObjectDataList[iter]);
					//string curattackobjectid = AttackObjectDataList[iter];
					//GAttackObjectCreateManager.Instance.CreateAttackObject(curattackobjectid,
					//                                                       Stat.Attack * DamageMulti,
					//                                                       PivotTransform.position,
					//                                                       PivotTransform.right,
					//                                                       HitGroupIDList);
					CreateAttackObject(iter);
				}
			}
			
			if(CurTimer > TotalTime)
			{
				IsPlaying = false;
				CoolTimer = 0f;
			}
		}
	}

	protected virtual void CreateAttackObject(int _index)
	{
		Object CurAttackObject = AttackObjectPrefabList[_index];
		GameObject newgo = Instantiate(CurAttackObject) as GameObject;

		GAttackObjectBase attackobjectbase = newgo.GetComponent<GAttackObjectBase>();
		if(attackobjectbase != null)
		{
			GAttackObjectData data = DataManager.Inst.GetGAttackObjectData(attackobjectbase.ID);
			attackobjectbase.Init(PivotTransform.transform.position, data.MoveLocalSpeed, Stat.Attack * DamageMulti * data.DamageMulti,
			                  data.RemainFrame + 1, data.Attack_Effect, data.Hit_Effect, HitGroupIDList);
		}
	}
	
	public virtual bool CoolIsReady()
	{
		if(CoolTimer > CoolTime && !IsPlaying)
		{
			return true;
		}
		
		return false;
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

}
