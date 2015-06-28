using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour {

	protected int UnitId = 0;

	public float Mass = 1f;
	public float MaxSpeed = 2f;
	public float Acceleration = 1f;
	public float MoveFriction = 0.95f;
	public bool IsInnerMoving;
	public Vector3 InnerForce;
	public Vector3 InnerVelocity;

	public Vector3 OuterForce;
	public Vector3 OuterVelocity;
	public float OuterFriction = 0.7f;
	public Vector3 TotalVelocity;

	public bool IsOnGround = true;
	public bool IsTryingToJump = false;
	public float JumpInitForce = 10f;
	public float JumpContinueForce = 1f;
	public float JumpContinueTime = 0.2f;
	protected float JumpContinueTimer = 0f;
	public float MaxFallSpeed = -9.8f;
	public Vector3 GravityForce = Vector3.down * 9.8f;
	public float GravityResistance = 1f;

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

	void FixedUpdate()
	{
		Process (Time.fixedDeltaTime);
	}

	public void Process(float _deltatime)
	{
		ProcessInnerForce(_deltatime);
		ProcessOuterForce(_deltatime);
		ProcessJump(_deltatime);
		ProcessGravity (_deltatime);
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
			JumpContinueTimer = 0f;
		}else
		{

		}
	}

	public void Jump()
	{
		if(IsOnGround)
		{
			IsOnGround = false;
			InnerVelocity.y += JumpInitForce / Mass;
		}else
		{
			IsTryingToJump = true;
		}
	}

	protected void ProcessJump(float _deltatime)
	{
		if(!IsOnGround)
		{
			JumpContinueTimer += _deltatime;
			if(JumpContinueTimer < JumpContinueTime && IsTryingToJump)
			{
				InnerVelocity.y += JumpContinueForce / Mass * _deltatime;
			}
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
	}

	protected void CheckPlatform(float _deltatime)
	{
		//check falling speed

		RaycastHit2D[] rayhits = Physics2D.RaycastAll(transform.position, Vector2.down, TotalVelocity.y * _deltatime,
		                                              LayerMask.GetMask("Platforms"));
		//Debug.Log("Ray check: " + rayhits.Length);
		if(rayhits.Length > 0)
		{
			if(InnerVelocity.y > 0f)
			{
				//if rising don't check
			}else
			{			
				Vector3 newpos = transform.position;
				newpos.y = rayhits[0].point.y;
				transform.position = newpos;
				TotalVelocity.y = 0f;
				InnerVelocity.y = 0f;
				SetIsOnGround(true);
			}
		}else
		{
			SetIsOnGround(false);
		}
	}

	protected void ResetForces()
	{
		InnerForce = Vector3.zero;
		OuterForce = Vector3.zero;
		IsTryingToJump = false;
		IsInnerMoving = false;
	}
}
