using UnityEngine;
using System.Collections;

[System.Serializable]
public class GAIBase
{
	public int StateIndex;

	public float EnterTime;
	public float EnterTimer;

	public float PlayingTime;
	public float PlayingTimer;

	public float EndTime;
	public float EndTimer;

	public bool IsPlaying = false;

	public virtual void Reset()
	{
		PlayingTimer = 0f;
		EnterTimer = 0f;
		EndTimer = 0f;
	}

	public virtual void ProcessEnter(float _deltatime)
	{
		EnterTimer += _deltatime;
	}

	public virtual void Process(float _deltatime)
	{
		PlayingTimer += _deltatime;
	}

	public virtual void ProcessExit(float _deltatime)
	{
		EndTimer += _deltatime;
		if(EndTimer > EndTime)
		{

		}
	}
}