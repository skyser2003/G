﻿using UnityEngine;
using System.Collections;

public class GAttackObject_Lich_Straight : GAttackObjectBase {


	protected override void OnObjectHit(GameObject_DamageDetector _col)
	{
		base.OnObjectHit(_col);
		if(IsValidTarget(_col))
		{
			DestroyAttackObject();
		}
	}
}
