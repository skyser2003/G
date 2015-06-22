using UnityEngine;
using System.Collections;

public class AnimatorTest : MonoBehaviour {

	public enum AnimState
	{
		Idle,
		Take_Damage,
		Die,
		Stunned,
		Defend,
		Walk,
		Run,
		Jump,
		Attack01,
		Attack02,

	}

	public Animator Anim;
	public AnimState State;
	public float StateSpeed = 1f;

	public float speedtest;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Anim.speed = StateSpeed;
			Anim.Play(State.ToString());
		}

		Anim.SetFloat("HorizontalSpeed", speedtest);
	}
}
