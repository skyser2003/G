using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextDisplayerUI : MonoBehaviour {

	public Transform DisplayObject;
	public Transform WorldGameObject;

	public Text TextLabel;
	public Image BackgroundSprite;
	public float ShowTime ;
	public float ShowTimer = 0f;

	protected Coroutine AnimationRoutine;

	void Awake()
	{
		DisplayObject.gameObject.SetActive(false);
	}

	public void Init(float _showtimer, string _text)
	{
		TextLabel.text = _text;
		ShowTime = _showtimer;
	}

	public void Play()
	{
		ShowTimer = 0f;
		AnimationRoutine = StartCoroutine(IEAnimation());
	}

	public void Process(float _time)
	{
		ShowTimer += _time;

		if(ShowTimer > ShowTime)
		{
			
		}
	}

	protected IEnumerator IEAnimation()
	{
		DisplayObject.gameObject.SetActive(true);
		DisplayObject.transform.localScale = Vector3.zero;

		//showtime
		float appeartimer = 0f;
		float appeartime = 0.2f;
		while(appeartimer < appeartime)
		{
			appeartimer += Time.deltaTime;

			float ratio = appeartimer / appeartime;
			DisplayObject.transform.localScale = Vector3.Lerp(DisplayObject.transform.localScale, Vector3.one, ratio);
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(ShowTime);

		float disappeartimer = 0f;
		float disappeartime = 0.2f;
		while(disappeartimer < disappeartime)
		{
			disappeartimer += Time.deltaTime;
			
			float ratio = disappeartimer / disappeartime;
			DisplayObject.transform.localScale = Vector3.Lerp(DisplayObject.transform.localScale, Vector3.zero, ratio);
			yield return new WaitForEndOfFrame();
		}

		AnimationRoutine = null;
		yield break;
	}
}
