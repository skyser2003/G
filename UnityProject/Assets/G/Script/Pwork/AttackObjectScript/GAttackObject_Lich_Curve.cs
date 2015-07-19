using UnityEngine;
using System.Collections;

public class GAttackObject_Lich_Curve : GAttackObjectBase {

	public float MaxYForce;
	public float MinYForce;

	public float MaxXForce;
	public float MinXForce;

	public float Gravity = -20f;
	protected Vector3 CurSpeed;

	public override void Process (float _deltatime)
	{
		CurSpeed += Vector3.up * Gravity * _deltatime;
		if(CurSpeed.y < -20f)
		{
			CurSpeed.y = -20f;
		}

		transform.position += CurSpeed * _deltatime;
		base.Process (_deltatime);
	}

	public void InitSpeed()
	{
		CurSpeed = new Vector3(Random.Range(MinXForce, MaxXForce), Random.Range(MinYForce, MaxYForce), 0f);

	}

	protected override void OnObjectHit(GameObject_DamageDetector _col)
	{
		base.OnObjectHit(_col);
		//Debug.Log("??? what th efuck: " + _col.gameObject.name);
		if(IsValidTarget(_col))
		{
			DestroyAttackObject();
		}
	}
}