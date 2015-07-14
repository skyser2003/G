﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveObjectBase : MonoBehaviour {

	protected int UnitId = 0;

	public float Mass = 1f;
	public float MaxSpeed = 2f;
	public float Acceleration = 1f;
	public float MoveFriction = 0.95f;
	public bool IsInnerMoving;
	public Vector3 PrevInnerForce;
	public Vector3 InnerForce;
	public Vector3 InnerVelocity;

	public Vector3 OuterForce;
	public Vector3 OuterVelocity;
	public float OuterFriction = 0.7f;
	public Vector3 TotalVelocity;

	public bool IsOnGround = true;
	public bool IsTryingToJump = false;
	public float JumpInitForce = 10f;
	public float MaxFallSpeed = -9.8f;
	public int CurJumpCount;
	public int JumpMaxCount;
	public Vector3 GravityForce = Vector3.down * 9.8f;
	public float GravityResistance = 1f;

	public List<GameObject> PlatformCheckObjectList = new List<GameObject>();

	protected float PlatformCheckYPos = -99999999999f;

	void OnDrawGizmos()
	{
		///draw platform check gizmos
		for(int platformobjectiter = 0; platformobjectiter < PlatformCheckObjectList.Count; platformobjectiter++)
		{
			Transform curobject = PlatformCheckObjectList[platformobjectiter].transform;
			Gizmos.color = Color.red;
			Gizmos.DrawRay(curobject.transform.position, Vector3.up * TotalVelocity.y * Time.deltaTime);
		}
	}

	public void Init(float _mass, float _maxspeed, float _acceleration, float _movefriction, 
	                 float _jumpinitforce, int _jumpmaxcount,
	                 float _maxfallspeed, float _gravityresistance)
	{
		Mass = _mass;
		MaxSpeed = _maxspeed;
		Acceleration = _acceleration;
		MoveFriction = _movefriction;
		JumpInitForce = _jumpinitforce;
		MaxFallSpeed = _maxfallspeed;
		GravityResistance = _gravityresistance;
		JumpMaxCount = _jumpmaxcount;
	}

	public void Move(bool _isleft)
	{
		IsInnerMoving = true;
		if(_isleft)
		{
			InnerForce += Vector3.left * Acceleration;
		}else
		{
			InnerForce += Vector3.right * Acceleration;
		}
	}

	void FixedUpdate()
	{
		//Process (Time.fixedDeltaTime);
	}

	public void Process(float _deltatime)
	{
		ProcessJump(_deltatime);
		ProcessGravity (_deltatime);
		ProcessInnerForce(_deltatime);
		ProcessOuterForce(_deltatime);
		Acummulate(_deltatime);

		ProcessMovement(_deltatime);
		CheckPlatform(_deltatime);

		ResetForces();
	}

	protected void ProcessInnerForce(float _deltatime)
	{
		InnerVelocity += InnerForce / Mass * _deltatime;

		//limit x speed
		InnerVelocity.x = Mathf.Clamp(InnerVelocity.x, -MaxSpeed, MaxSpeed);
		if(!IsInnerMoving)
		{
			InnerVelocity.x *= MoveFriction;
			if(Mathf.Abs(InnerVelocity.x) < 0.05f)
			{
				InnerVelocity.x = 0f;
			}
		}
	}

	protected void ProcessOuterForce(float _deltatime)
	{
		OuterVelocity += OuterForce / Mass * _deltatime; 
		OuterVelocity *= OuterFriction;
	}

	public void SetIsOnGround(bool _flag)
	{
		IsOnGround = _flag;
		if(_flag)
		{
			CurJumpCount = 0;
			TotalVelocity.y = 0f;
			InnerVelocity.y = 0f;
		}else
		{

		}
	}

	public bool Jump()
	{
		if(IsOnGround)
		{
			IsOnGround = false;
		}else
		{
			IsTryingToJump = true;
		}

		if(CurJumpCount < JumpMaxCount)
		{
			CurJumpCount ++;
			InnerVelocity.y = JumpInitForce / Mass;
			return true;
		}

		return false;
	}

	protected void ProcessJump(float _deltatime)
	{
		if(!IsOnGround)
		{

		}
	}

	protected void ProcessGravity(float _deltatime)
	{
		InnerVelocity += GravityForce * _deltatime / GravityResistance;
		if(InnerVelocity.y < MaxFallSpeed)
		{
			InnerVelocity.y = MaxFallSpeed;
		}
	}

	protected void Acummulate(float _deltatime)
	{
		TotalVelocity = Vector3.zero;
		TotalVelocity = InnerVelocity + OuterVelocity;

		if(IsOnGround && TotalVelocity.y < 0f)
		{
			TotalVelocity.y = 0f;
			InnerVelocity.y = 0f;
		}
	}

	protected void ProcessMovement(float _deltatime)
	{
		transform.position += TotalVelocity * _deltatime;
		if(PlatformCheckYPos > transform.position.y)
		{
			Vector3 newpos = transform.position;
			newpos.y = PlatformCheckYPos;
			transform.position = newpos;
			SetIsOnGround(true);
		}
	}

	protected void CheckPlatform(float _deltatime)
	{
		//if(InnerVelocity.y > 0f)
		//{
		PlatformCheckYPos = -99999999999f;
		//}
		//check falling speed
		for(int platformobjectiter = 0; platformobjectiter < PlatformCheckObjectList.Count; platformobjectiter++)
		{
			Transform curobject = PlatformCheckObjectList[platformobjectiter].transform;
			RaycastHit2D[] rayhits = Physics2D.RaycastAll(curobject.transform.position, Vector2.up, TotalVelocity.y * _deltatime,
			                                              LayerMask.GetMask("Platforms"));
			//Debug.Log("Ray check: " + rayhits.Length);
			if(rayhits.Length > 0)
			{
				if(InnerVelocity.y > 0f)
				{
					break;
					//return;
				}else
				{

					float heighestypos = -99999999f;
					for(int platformiter = 0; platformiter < rayhits.Length; platformiter++)
					{
						PlatformBase curplatform = rayhits[platformiter].transform.gameObject.GetComponent<PlatformBase>();
						if(curplatform != null)
						{
							if(curplatform.GetGroundPos() > heighestypos)
							{
								heighestypos = curplatform.GetGroundPos();
							}
						}
					}
					PlatformCheckYPos = heighestypos;
				}
			}else
			{
				SetIsOnGround(false);
			}
		}
	}

	protected void ResetForces()
	{
		PrevInnerForce = InnerForce;
		InnerForce = Vector3.zero;
		OuterForce = Vector3.zero;
		IsTryingToJump = false;
		IsInnerMoving = false;
	}

	void OnCollisionEnter2D(Collision2D _collision)
	{
		//Debug.Log("collsiion enter 2d called");
		//if(_collision.gameObject.layer == LayerMask.NameToLayer("Platforms"))
		//{
		//	Debug.Log("collsiion enter 2d called");
		//	foreach(ContactPoint2D contact in _collision.contacts)
		//	{
		//		if(contact.point.y > transform.position.y && 
		//		   contact.point.y < transform.position.y - TotalVelocity.y * Time.deltaTime)
		//		{
		//			Vector3 newpos = transform.position;
		//			newpos.y = contact.point.y;
		//			transform.position = newpos;
		//
		//			SetIsOnGround(true);
		//			Debug.Log("set on by collsiion");
		//			break;
		//		}
		//	}
		//}
	}
}