using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectBase : MonoBehaviour {

	public Vector3 DirectionPos;
	public string BalanceID = "ID_1";
	public int ID;
	public GObjectGroup GroupID;

	public float CurHealth;
	public float HealthRegenTimer;
	public float CurStamina;
	public float StaminaRegenTimer;
	public bool IsDead = false;
	void Start()
	{
		Create ();
	}

	public virtual void Create()
	{
		InitInputObject();
		InitAttackComp();

		//ObjectStat stat = DataManager.Inst.GetObjectStat(BalanceID);
		SetBaseStat(BaseStat);

		GameObjectManager.Instance.AddToObjectList(this);

		CurHealth = Stat.Health;
		CurStamina = Stat.MaxStamina;
		InitDamageDetector();
	}

	public virtual void Process(float _deltatime)
	{
		ProcessInput(_deltatime);
		MoveObject.Process(_deltatime);
		ProcessAttack(_deltatime);
		ProcessAnimation(_deltatime);
		ProcessHealthRegen(_deltatime);
		ProcessStaminaRegen(_deltatime);
	}

	protected virtual void ProcessHealthRegen(float _deltatime)
	{
		if(!IsDead)
		{
			HealthRegenTimer += _deltatime;
			if(HealthRegenTimer >= Stat.HealthRegenDelayTime)
			{
				if(CurHealth < Stat.Health)
				{
					CurHealth += Stat.HealthRegen * _deltatime;
				}
			}
		}
	}

	protected virtual void ProcessStaminaRegen(float _deltatime)
	{
		if(!IsDead)
		{
			StaminaRegenTimer += _deltatime;
			if(StaminaRegenTimer >= Stat.StaminaRegenDelayTime)
			{
				if(CurStamina < Stat.MaxStamina)
				{
					CurStamina += Stat.StaminaRegen * _deltatime;
				}
			}
		}
	}

	protected virtual void UseStamina(float _value)
	{
		CurStamina -= _value;
		CurStamina = Mathf.Max(0f, CurStamina);
		StaminaRegenTimer = 0f;
	}

	public virtual void Hit(float _damage)
	{
		CurHealth -= _damage;
		HealthRegenTimer = 0f;
		if(CurHealth <= 0f)
		{
			IsDead = true;
		}else
		{
			IsDead = false;
		}
		//Debug.Log("name: " + name + " got damage: " + _damage + " health: " + CurHealth);
	}

	public List<GameObject_DamageDetector> DamageDetectorList = new List<GameObject_DamageDetector>();
	public void InitDamageDetector()
	{
		for(int iter = 0; iter < DamageDetectorList.Count; iter++)
		{
			DamageDetectorList[iter].Init(GroupID);
		}
	}

	#region Equips
	public List<GEquipObject> EquipObjectList;
	public void Equip(GEquipObject _item)
	{
		if(!EquipObjectList.Contains(_item))
		{
			EquipObjectList.Add(_item);
			AddBuffData(_item.BuffData);
		}
	}

	public void UnEquip(GEquipObject _item)
	{
		if(EquipObjectList.Contains(_item))
		{
			GEquipObject removeobject = _item;
			EquipObjectList.Remove(_item);
			RemoveBuffData(_item.BuffData);
		}
	}
	#endregion

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

		if(MoveObject != null)
		{
			MoveObject.Init(Stat.Mass,
			                Stat.MaxMovementSpeed,
			                Stat.MoveForce,
			                Stat.MoveResistance,
			                Stat.JumpForce,
			                Stat.MaxJumpCount,
			                Stat.MaxFallSpeed,
			                Stat.GravityResistance
			                );
		}
		for(int iter = 0; iter < AttackCompList.Count; iter++)
		{
			AttackCompList[iter].SetStat(Stat);
		}

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
	public List<GAttackPatternBase> AttackCompList = new List<GAttackPatternBase>();
	protected virtual void InitAttackComp()
	{
		for(int iter = 0; iter < AttackCompList.Count; iter++)
		{
			AttackCompList[iter].Init();
		}
	}
	protected virtual void ProcessAttack(float _timer)
	{
		for(int iter = 0; iter < AttackCompList.Count; iter++)
		{
			AttackCompList[iter].Process(_timer);
		}
	}
	#endregion
}

[System.Serializable]
public struct ObjectStat
{
	public float Health;
	public float HealthRegen;
	public float HealthRegenDelayTime;
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
	public float MaxStamina;
	public float StaminaRegen;
	public float StaminaRegenDelayTime;
	public void AddStat(ObjectStat _stat)
	{
		Health += _stat.Health;
		HealthRegen += _stat.HealthRegen;
		HealthRegenDelayTime += _stat.HealthRegenDelayTime;
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
		MaxStamina += _stat.MaxStamina;
		StaminaRegen += _stat.StaminaRegen;
		StaminaRegenDelayTime += _stat.StaminaRegenDelayTime;
	}
}