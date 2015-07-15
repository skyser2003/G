using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectBase : MonoBehaviour {

	public Vector3 DirectionPos;
	public string BalanceID = "ID_1";
	public int ID;
	public int GroupID;

	public float CurHealth;
	void Start()
	{
		Create ();
	}

	public void Create()
	{
		InitInputObject();
		InitAttackComp();

		ObjectStat stat = DataManager.Inst.GetObjectStat(BalanceID);
		SetBaseStat(stat);

		GameObjectManager.Instance.AddToObjectList(this);

		CurHealth = Stat.Health;
	}

	public virtual void Process(float _deltatime)
	{
		ProcessInput(_deltatime);
		MoveObject.Process(_deltatime);
		ProcessAttack(_deltatime);
		ProcessAnimation(_deltatime);
	}

	public virtual void Hit(float _damage)
	{
		CurHealth -= _damage;
		Debug.Log("name: " + name + " got damage: " + _damage + " health: " + CurHealth);
	}

	#region Input things
	protected void InitInputObject()
	{
		GInputManager.Instance.AddInputObject(this);
	}

	public List<GInputType> InputReceivedList = new List<GInputType>();

	public void AddInput(GInputType _type)
	{
		InputReceivedList.Add(_type);
	}

	protected virtual void ProcessInput(float _deltatime)
	{

	}

	public void ClearInput()
	{
		InputReceivedList.Clear();
	}
	#endregion


	#region Things for stat
	public MoveObjectBase MoveObject;

	public ObjectStat Stat;
	public ObjectStat BaseStat;
	public List<ObjectBuffData> BuffDataList = new List<ObjectBuffData>();

	public void SetBaseStat(ObjectStat _stat)
	{
		BaseStat = _stat;
		ReCalcStat();
	}

	public virtual void AddBuffData(ObjectBuffData _data)
	{
		BuffDataList.Add(_data);
		ReCalcStat();
	}

	public virtual void RemoveBuffData(ObjectBuffData _data)
	{
		BuffDataList.Remove (_data);
		ReCalcStat();
	}

	public virtual void ReCalcStat()
	{
		//recalc Stat
		Stat = BaseStat;
		for(int iter = 0; iter < BuffDataList.Count; iter++)
		{
			ObjectBuffData curbuff = BuffDataList[iter];
			Stat.AddStat(curbuff.Stat);
		}

		MoveObject.Init(Stat.Mass,
		                Stat.MaxMovementSpeed,
		                Stat.MoveForce,
		                Stat.MoveResistance,
		                Stat.JumpForce,
		                Stat.MaxJumpCount,
		                Stat.MaxFallSpeed,
		                Stat.GravityResistance
		                );
		AttackComp.SetStat(Stat);

		CurHealth = Mathf.Min(CurHealth, Stat.Health);
	}

	#endregion

	#region AnimationComponent
	public GAnimationBase AnimationComp;
	protected virtual void ProcessAnimation(float _timer)
	{

	}
	#endregion

	#region AttackComponent
	public GAttackComponentBase AttackComp;
	protected virtual void InitAttackComp()
	{
		AttackComp = new GAttackComponentBase();
		AttackComp.Init(DataManager.Inst.GetAttackPatternStringList(BalanceID));
	}
	protected virtual void ProcessAttack(float _timer)
	{
		AttackComp.UpdateState(transform.position, DirectionPos);
		AttackComp.Process(_timer);
	}
	#endregion
}

public struct ObjectStat
{
	public float Health;
	public float Attack;
	public float Defense;
	public float Mass;
	public float MaxMovementSpeed;
	public float MoveForce;
	public float MoveResistance;
	public float JumpForce;
	public int MaxJumpCount;
	public float MaxFallSpeed;
	public float GravityResistance;

	public void AddStat(ObjectStat _stat)
	{
		Health += _stat.Health;
		Attack += _stat.Attack;
		Defense += _stat.Defense;
		Mass += _stat.Mass;
		MaxMovementSpeed += _stat.MaxMovementSpeed;
		MoveForce += _stat.MoveForce;
		MoveResistance += _stat.MoveResistance;
		JumpForce += _stat.JumpForce;
		MaxJumpCount += _stat.MaxJumpCount;
		MaxFallSpeed += _stat.MaxFallSpeed;
		GravityResistance += _stat.GravityResistance;
	}
}