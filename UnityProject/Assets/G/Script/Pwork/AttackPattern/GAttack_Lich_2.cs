using UnityEngine;
using System.Collections;

public class GAttack_Lich_2 : GAttackPatternBase {

	protected override void CreateAttackObject(int _index)
	{
		Object CurAttackObject = AttackObjectPrefabList[_index];
		GameObject newgo = Instantiate(CurAttackObject) as GameObject;
		
		GAttackObject_Lich_Curve attackobjectbase = newgo.GetComponent<GAttackObject_Lich_Curve>();
		if(attackobjectbase != null)
		{

			attackobjectbase.Init(PivotTransform.transform.position, Vector3.zero, Stat.Attack * DamageMulti, HitGroupIDList);
			attackobjectbase.InitSpeed();
		}
	}
}
