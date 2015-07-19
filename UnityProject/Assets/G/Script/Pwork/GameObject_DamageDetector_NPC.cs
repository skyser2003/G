using UnityEngine;
using System.Collections;

public class GameObject_DamageDetector_NPC : GameObject_DamageDetector {

	public override void DoDamage (float _damage)
	{
		//base.DoDamage (_damage);
		Application.LoadLevel(Constant.Scene_Boss_Lich);
	}
}
