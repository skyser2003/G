using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextDisplayerManager : MonoBehaviour {

	public Camera WorldCam;
	public Camera UICam;

	public List<TextDisplayerUI> TextDisplayerUIList = new List<TextDisplayerUI>();

	void LateUpdate()
	{
		for(int iter = 0; iter < TextDisplayerUIList.Count; iter++)
		{
			TextDisplayerUI curui = TextDisplayerUIList[iter];
			curui.transform.position = UICam.ScreenToWorldPoint(WorldCam.WorldToScreenPoint(curui.WorldGameObject.position));
		}
	}

	public void Process()
	{

	}

}
