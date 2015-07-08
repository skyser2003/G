using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectBalanceDataRow
{
	public string ID { get; set; }
	public string Name { get; set; }
	public double Attack { get; set; }
	public double Health { get; set; }
	public double Defense { get; set; }
	public double Mass { get; set; }
	public double MaxMovementSpeed { get; set; }
	public double MoveForce { get; set; }
	public double MoveResistance { get; set; }
	public double JumpForce { get; set; }
	public int MaxJumpCount { get; set; }
	public double GravityResistance { get; set; }
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
	public double TotalTime { get; set; }
	public double CoolTime { get; set; }
	public List<double> AttackObjectTimeList { get; set; }
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
	public double DamageMulti { get; set; }
	public string DamageType { get; set; }
	public List<double> MoveLocalSpeed { get; set; }
	public Vector3 MoveLocalSpeed_Vector3 { get { return new Vector3((float)MoveLocalSpeed[0], (float)MoveLocalSpeed[1], (float)MoveLocalSpeed[2]); } }
	public int RemainFrame { get; set; }
}

public class AttackObjectDataDatabase
{
	public List< AttackObjectDataRow > AttackObjectDataRow { get; set; }
}