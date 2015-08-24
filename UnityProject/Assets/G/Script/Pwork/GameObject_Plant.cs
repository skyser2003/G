using UnityEngine;
using System.Collections;

public class GameObject_Plant : GameObjectBase {

	public GAttack_Arrow ShootArrowAttack;

	public float StartDelayTime;
	public float StartDelayTimer;
	public float RestTime;
	public float RestTimer;
	public float ShootTime;
	public float ShootTimer;

	public bool IsLeft = false;

	public float CheckRadius = 5f;
	public Transform Target;
	public override void Process (float _deltatime)
	{
		base.Process (_deltatime);

		ProcessArrowFire(_deltatime);
		CheckPlayer();
	}
	protected override void ProcessAnimation (float _timer)
	{
		base.ProcessAnimation (_timer);

		AnimationComp.SetBool("IsDead", IsDead);

	}

	protected void ProcessArrowFire(float _deltatime)
	{
		if(IsDead)
		{
			return;
		}
		RestTimer += _deltatime;
		if (Target != null)
		{
			//RestTimer += _deltatime;
			if(RestTimer > RestTime)
			{
				ShootTimer += _deltatime;
				if(ShootTimer > ShootTime)
				{
					Vector3 direction = Vector3.right;
					if(IsLeft)
					{
						direction = Vector3.left;
					}
					direction *= 5f;
					ShootArrowAttack.SetDirection(direction);
					AnimationComp.SetTrigger("StartAttack");
					ShootArrowAttack.Play();
					ShootTimer = 0f;
					RestTimer = 0f;
				}
			}else
			{
				if(transform.position.x < Target.transform.position.x)
				{
					IsLeft = false;
				}else
				{
					IsLeft = true;
				}
				AnimationComp.SetLeft(IsLeft);
			}
		}else
		{
			ShootTimer = 0f;
		}
	}

	protected void CheckPlayer()
	{
		if(Target != null && Vector2.Distance(Target.transform.position, transform.position) > CheckRadius)
		{
			Target = null;
		}

		if(Target == null && ShootTimer <= 0f)
		{
			Collider2D[] collist = Physics2D.OverlapCircleAll(transform.position, CheckRadius, LayerMask.GetMask(Constant.Layer_Player));
			if(collist.Length > 0)
			{
				Target = collist[0].transform;
			}
		}
	}
}
