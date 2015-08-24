using UnityEngine;
using System.Collections;

public class CoinItem : MonoBehaviour {
		
	void FixedUpdate()
	{
		Process(Time.deltaTime);
	}

	public void Process(float _time)
	{
		ProcessRotate(_time);
	}

	void ProcessRotate(float _deltatime)
	{
		transform.Rotate(Vector3.up, 720f * _deltatime);
	}

	void OnTriggerEnter2D(Collider2D _col)
	{
		if(_col.gameObject.layer == LayerMask.NameToLayer(Constant.Layer_Player))
		{
			if(UserDataManager.Instance != null)
			{
				UserDataManager.Instance.Coin += 1;
			}
		}
	}

}
