using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GEffectManager : MonoBehaviour {

	private static GEffectManager instance;
	public static GEffectManager Instance
	{
		get{
			if(instance == null)
			{
				instance = FindObjectOfType(typeof(GEffectManager)) as GEffectManager;
			}
			
			return instance;
		}
	}

	public List<Object> EffectPrefabList = new List<Object>();

	public GameObject GetEffectObject(string _effectname)
	{
		for(int iter = 0; iter < EffectPrefabList.Count; iter++)
		{
			if(EffectPrefabList[iter].name == _effectname)
			{
				GameObject newgo = Instantiate(EffectPrefabList[iter]) as GameObject;
				return newgo;
			}
		}

		return null;
	}

	public enum EffectType
	{
		Attack_Claw,
		Attack_Light,
		Attack_Fireball,


		Hit_Normal_1,
		Hit_Light,
		Hit_Fire,
	}
}
