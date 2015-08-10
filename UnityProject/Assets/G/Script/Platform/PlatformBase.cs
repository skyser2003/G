using UnityEngine;
using System.Collections;

public class PlatformBase : MonoBehaviour {

	public float PlatformHeight;
	public float GetGroundPos()
	{
		return transform.position.y + PlatformHeight / 2f;
	}

	public void Init(float _platformheight)
	{
		PlatformHeight = _platformheight;
	}
}
