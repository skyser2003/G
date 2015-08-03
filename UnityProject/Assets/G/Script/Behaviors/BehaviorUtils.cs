using UnityEngine;

public static class BehaviorUtils {
	public static void MakeVisible(GameObject obj, bool visible) {
		Renderer renderer = obj.GetComponent<Renderer>();
		if (renderer != null && renderer.enabled != visible) {
			renderer.enabled = visible;
		}
		int childCount = obj.transform.childCount;
		for (int i = 0; i < childCount; i++) {
			GameObject child = obj.transform.GetChild(i).gameObject;
			MakeVisible(child, visible);
		}
	}
}
