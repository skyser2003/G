using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthGaugeUIManager : MonoBehaviour {


	private static HealthGaugeUIManager instance;
	public static HealthGaugeUIManager Instance
	{
		get{
			if(instance == null)
			{
				instance = FindObjectOfType(typeof(HealthGaugeUIManager)) as HealthGaugeUIManager;
			}
			
			return instance;
		}
	}


	public Camera GameCamera;

	public Camera UICamera;

	public Object HealthGaugeDisplayerObject;
	public List<HealthGaugeDisplayer> HealthDisplayerList = new List<HealthGaugeDisplayer>();

	/// <summary>
	/// showhealth displayer will be called at late update. so init on update
	/// </summary>
	protected int DisplayHealthCallCount = 0;
	void Update()
	{
		DisplayHealthCallCount = 0;
		for(int iter = 0; iter < HealthDisplayerList.Count; iter++)
		{
			HealthDisplayerList[iter].gameObject.SetActive(false);
		}
	}

	public void ShowHealthDisplayer(Vector3 _worldpos, float _curhealth, float _maxhealth)
	{
		HealthGaugeDisplayer displayer;
		if(HealthDisplayerList.Count > DisplayHealthCallCount)
		{
			//fetch
			displayer = HealthDisplayerList[DisplayHealthCallCount];
		}else
		{
			GameObject newgo = Instantiate(HealthGaugeDisplayerObject) as GameObject;
			newgo.transform.parent = transform;
			newgo.transform.localScale = Vector3.one;
			displayer = newgo.GetComponent<HealthGaugeDisplayer>();
			HealthDisplayerList.Add(displayer);
		}

		displayer.gameObject.SetActive(true);
		Vector3 newworldpos = UICamera.ScreenToWorldPoint(GameCamera.WorldToScreenPoint(_worldpos));
		displayer.UpdateUI(newworldpos, _curhealth,	 _maxhealth);

		DisplayHealthCallCount++;
	}
}
