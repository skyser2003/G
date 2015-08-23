using UnityEngine;
using System.Collections;

public class CameraSystem : MonoBehaviour {

	public Transform TargetTransform;
	public float FollowSpeed;
	public Vector3 DeltaPos;

	void LateUpdate()
	{
		UpdateCameraPos();
	}

	public void UpdateCameraPos()
	{
		if(TargetTransform != null)
		{
			Vector3 targetpos = TargetTransform.position;
			targetpos.z = DeltaPos.z;	
			Vector3 pos = Vector3.Lerp(transform.position,  TargetTransform.position, FollowSpeed);
			pos.z = DeltaPos.z;	
			transform.position = pos;
		}
	}
}
