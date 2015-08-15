using UnityEngine;
using System.Collections;

public class GameObjectShootArrow : GameObjectBase {

	public GAttackPatternBase ShootArrowAttack;

	public float StartDelayTime;
	public float StartDelayTimer;
	public float RestTime;
	public float RestTimer;
	public float ShootTime;
	public float ShootTimer;


	public override void Process (float _deltatime)
	{
		base.Process (_deltatime);
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
					ShootArrowAttack.Play();
					ShootTimer = 0f;
					RestTimer = 0f;
				}
			}
		}
	}
}
