using UnityEngine;
using System.Collections;

public class GameObject_DamageDetector : MonoBehaviour {

	public int GroupID;
	public GameObjectBase DamageTo;

	public void Init(int _groupid)
	{

	}

	public virtual void DoDamage(float _damage)
	{
		if(DamageTo != null)
		{
			DamageTo.Hit(_damage);
		}
	}
}
