using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObject_Player : GameObjectBase {

	public List<Transform> EquipItemObjectPivot = new List<Transform>();

	protected bool IsHoldingStringAttack = false;
	public float StrongAttackStartTime = 0.05f;
	protected float AttackPressedTimer;
	protected bool IsLeft = false;

	
	
	public override void Process (float _deltatime)
	{
		base.Process (_deltatime);
		UpdateEquipedItemPos(_deltatime);
	}

	protected override void ProcessInput (float _deltatime)
	{
		base.ProcessInput (_deltatime);
		IsHoldingStringAttack = false;
		//Debug.Log("?: " + InputReceivedList.Count);
		for(int inputiter = 0; inputiter < InputReceivedList.Count; inputiter++)
		{
			GInputType curinput = InputReceivedList[inputiter];
			//Debug.Log("wat: " + curinput);
			if(curinput == GInputType.MOVE_LEFT_PRESSED)
			{
				IsLeft = true;
				DirectionPos = Vector3.left;
				MoveObject.Move(true);
			}

			if(curinput == GInputType.MOVE_RIGHT_PRESSED)
			{
				IsLeft = false;
				DirectionPos = Vector3.right;
				MoveObject.Move(false);
			}

			if(curinput == GInputType.JUMP_DOWN)
			{
				if(	MoveObject.Jump() )
				{
					//AnimationComp.SetTrigger("StartJump");
				}
			}

			if(curinput == GInputType.KEY_1_DOWN)
			{
				//check if item can be equiped....
				CheckEquipItem();

				//if(AttackComp.PlayAttack(0))
				//{
				//	AnimationComp.SetTrigger("StartWeakAttack");
				//}
				if(AttackCompList[0].CanAttack())
				{
					IsHoldingStringAttack = true;
				}

				AttackPressedTimer = 0f;
			}
			if(curinput == GInputType.KEY_1_PRESSED)
			{
				if(AttackCompList[0].CanAttack())
				{
					IsHoldingStringAttack = true;
					AttackPressedTimer += Time.deltaTime;
					if(AttackPressedTimer > StrongAttackStartTime)
					{

					}
				}
			}

			if(curinput == GInputType.KEY_1_RELEASE)
			{
				if(AttackCompList[0].CanAttack())
				{
					if(AttackCompList[0].PlayAttack())
					{
						AnimationComp.SetTrigger("ReleaseAttack");
					}
					AttackPressedTimer = 0f;
				}
			}
		}

		ClearInput();
	}

	protected override void ProcessAnimation (float _timer)
	{
		base.ProcessAnimation (_timer);
		AnimationComp.SetBool("IsOnAir", !MoveObject.IsOnGround);
		AnimationComp.SetFloat ("HorizontalSpeed", Mathf.Abs(MoveObject.InnerVelocity.x));
		AnimationComp.SetLeft(IsLeft);
		AnimationComp.SetBool("HoldAttack", IsHoldingStringAttack);
	}

	protected void UpdateEquipedItemPos(float _deltatime)
	{
		for(int itemiter = 0; itemiter < EquipObjectList.Count; itemiter++)
		{
			GEquipObject curequip = EquipObjectList[itemiter];

			int targetindex = Mathf.Min(itemiter, EquipObjectList.Count);
			Transform targetpos = EquipItemObjectPivot[targetindex];

			curequip.transform.position = Vector3.Lerp(curequip.transform.position, targetpos.position, _deltatime * 10f);
		}
	}

	protected void CheckEquipItem()
	{
		RaycastHit2D hitinfo = Physics2D.CircleCast(transform.position, 1f, Vector2.right, 0f, LayerMask.GetMask("DropItem"));
		if(hitinfo != null && hitinfo.collider != null)
		{
			Debug.Log("what : " +hitinfo.collider.name);
			GEquipObject equipobject =  hitinfo.collider.gameObject.GetComponent<GEquipObject>();
			if(equipobject != null)
			{
				Equip(equipobject);
			}
		}else
		{
			Debug.Log("no item found");
		}
	}
}
