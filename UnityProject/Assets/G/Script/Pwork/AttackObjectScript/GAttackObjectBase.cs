using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GAttackObjectBase : MonoBehaviour {

	public enum DamageType
	{
		Single,
	}

	public string ID;

	public GameObject AttackObject;
	public float Damage;
	public int RemainFrame;
	public Vector3 MoveLocalSpeed;
	public List<int> HitGroupList = new List<int>();
	public string AttackEffect;
	public string HitEffect;
	public virtual void Init(Vector3 _worldpos,
	                            Vector3 _movespeed,
	                            float _damage,
	                            int _remainframe,
	                            string _attackeffect,
	                            string _hiteffect,
	                            List<int> _hitgrouplist)
	{
		GAttackObjectCreateManager.Instance.AddAttackObject(this);
		transform.position = _worldpos;
		MoveLocalSpeed = _movespeed;
		AttackEffect = _attackeffect;
		HitEffect = _hiteffect;
		Damage = _damage;
		RemainFrame = _remainframe;
		AttackObject = GEffectManager.Instance.GetEffectObject(AttackEffect);
		HitGroupList = _hitgrouplist;
		//AttackObject.transform.parent = transform;
		AttackObject.transform.position = transform.position;
		Destroy(AttackObject.gameObject, 0.2f);
	}

	void FixedUpdate()
	{
		//Process(Time.deltaTime);
	}

	public virtual void Process(float _deltatime)
	{
		RemainFrame--;
		//Debug.Log("???:" + RemainFrame);
		transform.position += MoveLocalSpeed * _deltatime;

		if(RemainFrame <= 0)
		{
			DestroyAttackObject();
		}
	}

	protected virtual void DestroyAttackObject()
	{
		GAttackObjectCreateManager.Instance.RemoveAttackObject(this);
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D _col)
	{
		GameObject_DamageDetector gob = _col.GetComponent<GameObject_DamageDetector>();
		if(gob != null)
		{
			OnObjectHit(gob);
		}
	}

	void OnTriggerStay2D(Collider2D _col)
	{
		GameObject_DamageDetector gob = _col.GetComponent<GameObject_DamageDetector>();
		if(gob != null)
		{
			OnObjectHit(gob);
		}
	}

	void OnTriggerExit2D(Collider2D _col)
	{
		GameObject_DamageDetector gob = _col.GetComponent<GameObject_DamageDetector>();
		if(gob != null)
		{
			OnObjectHit(gob);
		}
	}

	protected virtual void OnObjectHit(GameObject_DamageDetector _col)
	{
		//Debug.Log("Check collision: " + _col.gameObject.name);
		if(IsValidTarget(_col))
		{
			//Debug.Log("???: "+ _col.GroupID);
			_col.DoDamage(Damage);
		}
	}

	protected virtual void OnObjectStay(GameObject_DamageDetector _col)
	{

	}

	protected virtual void OnObjectExit(GameObject_DamageDetector _col)
	{

	}

	protected virtual bool IsValidTarget(GameObject_DamageDetector _object)
	{
		bool match = false;
		for(int iter = 0; iter < HitGroupList.Count; iter++)
		{
			if(HitGroupList[iter] == _object.GroupID)
			{
				match = true;
				break;
			}
		}

		return match;
		//return true;
	}
}

public class GAttackObjectData
{
	public string ID;
	public string Name;
	public string Attack_Effect;
	public string Hit_Effect;
	public float DamageMulti;
	public string DamageType;
	public Vector3 MoveLocalSpeed;
	public Vector3 CreateDeltaPos;
	public int RemainFrame;
}