using UnityEngine;
using System.Collections;

public class GAnimation_Animator : GAnimationBase {

	public Animator m_Animator;

	public override void SetLeft (bool _left)
	{
		base.SetLeft (_left);

		Vector3 eulerangle = new Vector3(0f,90f,0f); 
		if(_left)
		{
			eulerangle.y = 270f;
		}

		m_Animator.transform.localEulerAngles = eulerangle;
	}

	public override void SetBool (string _param, bool _val)
	{
		base.SetBool (_param, _val);
		m_Animator.SetBool(_param, _val);
	}


	public override void SetFloat (string _param, float _val)
	{
		base.SetFloat (_param, _val);
		m_Animator.SetFloat(_param, _val);
	}

	public override void SetInt (string _param, int _val)
	{
		base.SetInt (_param, _val);
		m_Animator.SetInteger(_param, _val);
	}

	public override void SetTrigger (string _param)
	{
		base.SetTrigger (_param);
		m_Animator.SetTrigger(_param);
	}
}
