using UnityEngine;
using System.Collections;

public class GameObject_DamageDetector : MonoBehaviour {

	public GObjectGroup GroupID;
	public GameObjectBase DamageTo;

	public void Init(GObjectGroup _groupid)
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
