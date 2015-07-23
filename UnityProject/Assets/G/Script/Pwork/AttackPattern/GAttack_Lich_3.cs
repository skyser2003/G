using UnityEngine;
using System.Collections;

public class GAttack_Lich_3 : GAttackComponentBase {

	public Transform TargetPlayer;

	protected override void CreateAttackObject(int _index)
	{
		Object CurAttackObject = AttackObjectPrefabList[_index];
		GameObject newgo = Instantiate(CurAttackObject) as GameObject;
		
		GAttackObject_Lich_Derive attackobjectbase = newgo.GetComponent<GAttackObject_Lich_Derive>();
		if(attackobjectbase != null)
		{
			attackobjectbase.FollowObject = TargetPlayer;

			//GAttackObjectData data = DataManager.Inst.GetGAttackObjectData(attackobjectbase.ID);
			attackobjectbase.Init(PivotTransform.transform.position, Vector3.zero, Stat.Attack * DamageMulti, HitGroupIDList);
		}
	}
}
