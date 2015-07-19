using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour {

	public Transform CharacterTransform;
	public Animator m_Animator;

	void FixedUpdate()
	{
		CheckPlayerAndSayHi();
	}

	public string SayText = "HI";
	protected bool IsPlayerInRange = false;
	public void CheckPlayerAndSayHi()
	{
		float xposdelta = Mathf.Abs(transform.position.x - CharacterTransform.position.x);

		if(xposdelta < 3f)
		{
			if(!IsPlayerInRange)
			{
				IsPlayerInRange = true;
				m_Animator.SetTrigger("StartSpinAttack");
				//show message
				Debug.Log("NPC Says: " + SayText);
				TextDisplayer.Init(3f, SayText);
				TextDisplayer.Play();
			}
		}else
		{
			IsPlayerInRange = false;
		}
	}

	public TextDisplayerUI TextDisplayer;
}
