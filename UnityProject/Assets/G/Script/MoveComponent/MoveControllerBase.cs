using UnityEngine;
using System.Collections;

public class MoveControllerBase : MonoBehaviour {

	public float Mass = 1f;
	public float MaxSpeed = 2f;
	public float Acceleration = 1f;
	public float MoveFriction = 0.95f;
	protected bool IsInnerMoving;
	protected Vector3 InnerForce;
	protected Vector3 InnerVelocity;
	
	public Vector3 OuterForce;
	public Vector3 OuterVelocity;
	public float OuterFriction = 0.7f;
	public Vector3 TotalVelocity;
	
	public bool IsOnGround = false;
	public float JumpInitForce = 10f;
	public float JumpContinueForce = 1f;
	public float JumpContinueTime = 0.2f;
	public Vector3 GravityForce = Vector3.down * 9.8f;
	public float GravityResistance;
	
	public void Move(bool _isleft)
	{
		IsInnerMoving = true;
		if(_isleft)
		{
			InnerForce = Vector3.left * Acceleration;
		}else
		{
			InnerForce = Vector3.right * Acceleration;
		}
	}
	
	public void Process(float _deltatime)
	{
		ProcessInnerForce(_deltatime);
		
		ResetForces();
	}
	
	protected void ProcessInnerForce(float _deltatime)
	{
		InnerVelocity += InnerForce / Mass * _deltatime;
		
		//limit x speed
		InnerVelocity.x = Mathf.Clamp(InnerVelocity.x, -MaxSpeed, MaxSpeed);
		if(IsInnerMoving)
		{
			InnerVelocity.x *= MoveFriction;
			if(InnerVelocity.x < 0.05f)
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
	
	protected void ProcessGravity(float _deltatime)
	{
		OuterVelocity += GravityForce * _deltatime * GravityResistance;
		
	}
	
	protected void Acummulate(float _deltatime)
	{
		TotalVelocity = Vector3.zero;
		TotalVelocity = InnerVelocity + OuterVelocity;
		
		if(!IsOnGround)
		{
			TotalVelocity.y = 0f;
		}
	}
	
	protected void ResetForces()
	{
		InnerForce = Vector3.zero;
		OuterForce = Vector3.zero;
		
		IsInnerMoving = false;
	}
}

