using UnityEngine;
using System.Collections;

public class GAttack_Lich_1 : GAttackComponentBase {

	public Transform Target;

	public void SetTarget(Transform _target)
	{
		Target = _target;
	}

	protected override void CreateAttackObject(int _index)
	{
		Object CurAttackObject = AttackObjectPrefabList[_index];
		GameObject newgo = Instantiate(CurAttackObject) as GameObject;
		
		GAttackObject_Lich_Straight attackobjectbase = newgo.GetComponent<GAttackObject_Lich_Straight>();
		if(attackobjectbase != null)
		{
			Vector3 velocity = (Target.transform.position - PivotTransform.transform.position);
			velocity.z = 0f;
			velocity.Normalize();
			velocity *= 3f;
			GAttackObjectData data = DataManager.Inst.GetGAttackObjectData(attackobjectbase.ID);
			attackobjectbase.Init(PivotTransform.transform.position, velocity, Stat.Attack * DamageMulti * data.DamageMulti,
			                      data.RemainFrame + 1, data.Attack_Effect, data.Hit_Effect, HitGroupIDList);
		}
	}
}
