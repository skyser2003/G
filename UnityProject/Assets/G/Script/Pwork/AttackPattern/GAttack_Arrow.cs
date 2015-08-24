using UnityEngine;
using System.Collections;

public class GAttack_Arrow : GAttackPatternBase {

	public Vector3 ShootDirection;

	public void SetDirection(Vector3 _direction)
	{
		ShootDirection = _direction;
	}

	protected override void CreateAttackObject(int _index)
	{
		Object CurAttackObject = AttackObjectPrefabList[_index];
		GameObject newgo = Instantiate(CurAttackObject) as GameObject;
		
		GAttackObject_Arrow attackobjectbase = newgo.GetComponent<GAttackObject_Arrow>();
		if(attackobjectbase != null)
		{
			Vector3 velocity = ShootDirection;
			//velocity.z = 0f;
			//velocity.Normalize();
			//velocity *= 3f;
			GAttackObjectData data = DataManager.Inst.GetGAttackObjectData(attackobjectbase.ID);
			attackobjectbase.Init(PivotTransform.transform.position, velocity, Stat.Attack * DamageMulti * data.DamageMulti, HitGroupIDList);
		}
	}
}
