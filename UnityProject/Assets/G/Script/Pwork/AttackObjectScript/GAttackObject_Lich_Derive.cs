using UnityEngine;
using System.Collections;

public class GAttackObject_Lich_Derive : GAttackObjectBase {

	public Transform FollowObject;

	protected Vector3 CurSpeed;
	public float Speed;
	public float CurveRatio;

	public override void Process (float _deltatime)
	{
		Vector3 targetdirection = FollowObject.transform.position - transform.position;
		targetdirection.z = 0f;
		Vector3 curdirection = CurSpeed.normalized;
		curdirection.z = 0f;

		Vector3 curved = Vector3.Slerp(curdirection, targetdirection, CurveRatio);
		curved.z = 0f;
		curved.Normalize();
		CurSpeed = curved * Speed;

		transform.position += CurSpeed * _deltatime;

		base.Process (_deltatime);
	}

	protected override void OnObjectHit(GameObject_DamageDetector _col)
	{
		base.OnObjectHit(_col);
		if(IsValidTarget(_col))
		{
			DestroyAttackObject();
		}
	}
}