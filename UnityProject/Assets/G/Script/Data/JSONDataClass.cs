using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//----------------------------------------------
//    Google2u: Google Doc Unity integration
//         Copyright © 2015 Litteratus
//
//        This file has been auto-generated
//              Do not manually edit
//----------------------------------------------

using System.Collections.Generic;
using UnityEngine;


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
	public float MaxFallSpeed { get; set; }
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
	public float DamageMulti { get; set; }
	public string DamageType { get; set; }
	public List<float> MoveLocalSpeed { get; set; }
	public Vector3 MoveLocalSpeed_Vector3 { get { return new Vector3(MoveLocalSpeed[0], MoveLocalSpeed[1], MoveLocalSpeed[2]); } }
	public List<float> CreateDeltaPos { get; set; }
	public Vector3 CreateDeltaPos_Vector3 { get { return new Vector3(CreateDeltaPos[0], CreateDeltaPos[1], CreateDeltaPos[2]); } }
	public int RemainFrame { get; set; }
}

public class AttackObjectDataDatabase
{
	public List< AttackObjectDataRow > AttackObjectDataRow { get; set; }
}
