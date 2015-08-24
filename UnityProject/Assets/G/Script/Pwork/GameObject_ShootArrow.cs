using UnityEngine;
using System.Collections;

public class GameObject_ShootArrow : GameObjectBase {

	public GAttack_Arrow ShootArrowAttack;

	public float StartDelayTime;
	public float StartDelayTimer;
	public float RestTime;
	public float RestTimer;
	public float ShootTime;
	public float ShootTimer;

	public enum Direction{
		left,
		right,
		top,
		bot,
	}

	public Direction ShootDirection;
	public float Speed;

	public override void Process (float _deltatime)
	{
		base.Process (_deltatime);
		ProcessArrowFire(_deltatime);
	}


	protected void ProcessArrowFire(float _deltatime)
	{
		StartDelayTimer += _deltatime;
		if (StartDelayTimer > StartDelayTime)
		{
			RestTimer += _deltatime;
			if(RestTimer > RestTime)
			{
				ShootTimer += _deltatime;
				if(ShootTimer > ShootTime)
				{
					Vector3 velocity = Vector3.zero;
					if(ShootDirection == Direction.left)
					{
						velocity = Vector3.left;
					}else if(ShootDirection == Direction.right)
					{
						velocity = Vector3.right;
					}else if(ShootDirection == Direction.top)
					{
						velocity = Vector3.up;
					}else if(ShootDirection == Direction.bot)
					{
						velocity = Vector3.down;
					}

					velocity = velocity * Speed;
					ShootArrowAttack.SetDirection(velocity);
					ShootArrowAttack.Play();
					ShootTimer = 0f;
					RestTimer = 0f;
				}
			}
		}
	}
}
