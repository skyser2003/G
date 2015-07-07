using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectBalanceDataRow
{
	public string ID { get; set; }
	public string Name { get; set; }
	public float Attack { get; set; }
	public float Health { get; set; }
	public float Defense { get; set; }
	public float Mass { get; set; }
	public float MaxMovementSpeed { get; set; }
	public float MoveForce { get; set; }
	public float MoveResistance { get; set; }
	public float JumpForce { get; set; }
	public int MaxJumpCount { get; set; }
	public float GravityResistance { get; set; }
	public List<string> AttackPatternList { get; set; }
}

public class ObjectBalanceDataDatabase
{
	public List< ObjectBalanceDataRow > ObjectBalanceDataRow { get; set; }
}

public class AttackPatternDataRow
{
	public string ID { get; set; }
	public string Name { get; set; }
	public float TotalTime { get; set; }
	public float CoolTime { get; set; }
	public List<float> AttackObjectTimeList { get; set; }
	public List<string> AttackObjectDataList { get; set; }
}

public class AttackPatternDataDatabase
{
	public List< AttackPatternDataRow > AttackPatternDataRow { get; set; }
}

public class AttackObjectDataRow
{
	public string ID { get; set; }
	public string Name { get; set; }
	public string Attack_Effect { get; set; }
	public string Hit_Effect { get; set; }
	public float DamageMulti { get; set; }
	public string DamageType { get; set; }
	public List<float> MoveLocalSpeed { get; set; }
	public Vector3 MoveLocalSpeed_Vector3 { get { return new Vector3(MoveLocalSpeed[0], MoveLocalSpeed[1], MoveLocalSpeed[2]); } }
	public int RemainFrame { get; set; }
}

public class AttackObjectDataDatabase
{
	public List< AttackObjectDataRow > AttackObjectDataRow { get; set; }
}