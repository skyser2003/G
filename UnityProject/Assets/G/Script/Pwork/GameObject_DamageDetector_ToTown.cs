using UnityEngine;
using System.Collections;

public class GameObject_DamageDetector_ToTown : GameObject_DamageDetector {

	public override void DoDamage (float _damage)
	{
		//base.DoDamage (_damage);
		Application.LoadLevel(Constant.Scene_Town);
	}
}
