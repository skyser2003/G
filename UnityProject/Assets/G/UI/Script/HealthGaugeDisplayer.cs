using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class HealthGaugeDisplayer : MonoBehaviour {

	public Image HealthImage;
	public Image HealthBackImage;
	protected float HealthRatioVal = 10f;
	public UnityEngine.UI.Text HealthText;

	public void UpdateUI(Vector3 _wolrdpos, float _curval, float _maxval)
	{
		transform.position = _wolrdpos;
		float fill = _curval / HealthRatioVal - Mathf.Floor(_curval / HealthRatioVal);
		if(fill == 0f && _curval > 0f)
		{
			fill = 1f;
		}
		HealthImage.fillAmount = fill;
		if(_curval > 1)
		{
			HealthText.text = "X " + ((int)(_curval / HealthRatioVal)).ToString();
		}else
		{
			HealthText.text = "";
		}
	}

}
