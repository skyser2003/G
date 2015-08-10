using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PlatformCreator))]
[CanEditMultipleObjects]
public class PlatformCreatorPoser : Editor{

	public override void OnInspectorGUI ()
	{
		serializedObject.Update();
		//create button to pos it
		if(GUILayout.Button ("Pos"))
		{
			for(int iter = 0; iter < serializedObject.targetObjects.Length; iter++)
			{
				PlatformCreator creator = (PlatformCreator)serializedObject.targetObjects[iter];
				Vector3 newpos = creator.transform.localPosition;
				if(creator.Width % 2 == 0)
				{
					newpos.x = Mathf.Round(newpos.x);
				}else
				{
					newpos.x = Mathf.Round(newpos.x * 2f) / 2f;
				}
				if(creator.Height % 2 == 0)
				{
					newpos.y = Mathf.Round(newpos.y);
				}else
				{
					newpos.y = Mathf.Round(newpos.y * 2f) / 2f;
				}
				creator.transform.localPosition = newpos;
			}
			//serializedObject.targetObjects
		}

		base.OnInspectorGUI ();
	}
}
