using UnityEngine;
using System.Collections;

public class PlatformCreator_Move : PlatformCreator {

	protected Vector3 StartWorldPos;
	protected Vector3 EndWorldPos;
	public Vector3 StartLocalPos;
	public Vector3 EndLocalPos;
	protected Vector3 StartTarget;
	protected Vector3 EndTarget;

	public float RemainTme;
	public float RemainTimer;
	public float MoveTime;
	public float MoveTimer;

	void OnDrawGizmos()
	{
		Color color = Color.blue;
		color.a = 0.1f;
		Gizmos.color = color;

		Vector3 platformsize = new Vector3 (Width, Height);
		//draw start rect
		Gizmos.DrawCube (transform.position + StartLocalPos, platformsize);
		//draw end rect
		Gizmos.DrawCube (transform.position + EndLocalPos, platformsize);
		Gizmos.DrawLine (transform.position + StartLocalPos, transform.position + EndLocalPos);
	}

	void FixedUpdate()
	{
		Process (Time.deltaTime);
	}

	public override void Init ()
	{
		base.Init ();
		StartWorldPos = transform.position + StartLocalPos;
		EndWorldPos = transform.position + EndLocalPos;

		StartTarget = StartWorldPos;
		EndTarget = EndWorldPos;

		RemainTimer = 0f;
		MoveTimer = 0f;
	}

	public void Process(float _time)
	{
		RemainTimer += _time;
		if(RemainTimer > RemainTme)
		{
			MoveTimer += _time;
			float ratio = MoveTimer / MoveTime;
			Vector3 targetpos = Vector3.Lerp(StartTarget, EndTarget, ratio); 
			transform.position = targetpos;

			if(MoveTimer > MoveTime)
			{
				RemainTimer = 0f;
				MoveTimer = 0f;
				if(StartTarget == StartWorldPos)
				{
					StartTarget = EndWorldPos;
					EndTarget = StartWorldPos;
				}else
				{
					StartTarget = StartWorldPos;
					EndTarget = EndWorldPos;
				}
			}
		}
	}
}
