using UnityEngine;
using System.Collections;

public class GAttack_Lich_2 : GAttackComponentBase {

	protected override void CreateAttackObject(int _index)
	{
		Object CurAttackObject = AttackObjectPrefabList[_index];
		GameObject newgo = Instantiate(CurAttackObject) as GameObject;
		
		GAttackObject_Lich_Curve attackobjectbase = newgo.GetComponent<GAttackObject_Lich_Curve>();
		if(attackobjectbase != null)
		{

			GAttackObjectData data = DataManager.Inst.GetGAttackObjectData(attackobjectbase.ID);
			attackobjectbase.Init(PivotTransform.transform.position, Vector3.zero, Stat.Attack * DamageMulti * data.DamageMulti,
			                      data.RemainFrame * 2 + 1, data.Attack_Effect, data.Hit_Effect, HitGroupIDList);
			attackobjectbase.InitSpeed();
		}
	}
}
