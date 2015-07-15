using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GAttackObjectCreateManager : MonoBehaviour {

	private static GAttackObjectCreateManager instance;
	public static GAttackObjectCreateManager Instance
	{
		get{
			if(instance == null)
			{
				instance = FindObjectOfType(typeof(GAttackObjectCreateManager)) as GAttackObjectCreateManager;
			}
			
			return instance;
		}
	}

	public List<Object> AttackPrefabList = new List<Object>();
	public void CreateAttackObject(string _id, float _damage, Vector3 _pivotpos, Vector3 _direction, List<int> _hitgrouplist)
	{
		GAttackObjectData data = DataManager.Inst.GetGAttackObjectData(_id);

		GameObject newgo = Instantiate(AttackPrefabList[0]) as GameObject;
		GAttackObjectBase attackobject = newgo.GetComponent<GAttackObjectBase>();

		attackobject.Init(_pivotpos + _direction, data.MoveLocalSpeed, _damage * data.DamageMulti,
		                  data.RemainFrame, data.Attack_Effect, data.Hit_Effect, _hitgrouplist);

	}

	public List<GAttackObjectBase> GAttackObjectList = new List<GAttackObjectBase>();

}	
