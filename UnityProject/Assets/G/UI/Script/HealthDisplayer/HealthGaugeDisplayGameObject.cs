using UnityEngine;
using System.Collections;

public class HealthGaugeDisplayGameObject : MonoBehaviour {

	public GameObjectBase ObjectBase;

	void LateUpdate()
	{
		HealthGaugeUIManager.Instance.ShowHealthDisplayer(transform.position, ObjectBase.CurHealth, ObjectBase.Stat.Health,
		                                                  ObjectBase.CurStamina, ObjectBase.Stat.MaxStamina);
	}
}
