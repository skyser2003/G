using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class HealthGaugeDisplayer : MonoBehaviour {

	public Image HealthImage;
	public Image HealthBackImage;

	public Image StaminaImage;
	protected float HealthRatioVal = 10f;
	public UnityEngine.UI.Text HealthText;

	public void UpdateUI(Vector3 _wolrdpos, float _curhealth, float _maxhealth, float _curstamina, float _maxstamina)
	{
		transform.position = _wolrdpos;
		HealthImage.fillAmount = _curhealth / _maxhealth;
		StaminaImage.fillAmount = _curstamina / _maxstamina;
		//float fill = _curval / HealthRatioVal - Mathf.Floor(_curval / HealthRatioVal);
		//if(fill == 0f && _curval > 0f)
		//{
		//	fill = 1f;
		//}
		//HealthImage.fillAmount = fill;
		//if(_curval > 1)
		//{
		//	HealthText.text = "X " + ((int)(_curval / HealthRatioVal)).ToString();
		//}else
		//{
		//	HealthText.text = "";
		//}
	}

}
