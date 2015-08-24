using UnityEngine;
using System.Collections;

public class PlatformCreator : MonoBehaviour {

	public float Width;
	public float Height;

	public float TopColliderDepth = 0.1f;
	public float SideColliderDepth = 0.1f;

	public GameObject TopColliderObject;
	public GameObject LeftColliderObject;
	public GameObject RightColliderObject;
	public GameObject BotColliderObject;
	// Use this for initialization
	void Start () {
		Init();
	}

	public virtual void Init()
	{
		TopColliderObject = new GameObject("TopCollider");
		TopColliderObject.AddComponent<BoxCollider2D>().size = new Vector2(Width + 0.1f, TopColliderDepth);
		TopColliderObject.transform.parent = transform;
		TopColliderObject.transform.localPosition = new Vector3(0f, Height / 2f - TopColliderDepth / 2f, 0f);
		TopColliderObject.gameObject.layer = LayerMask.NameToLayer(Constant.Layer_Ground);
		TopColliderObject.AddComponent<PlatformBase>().Init(TopColliderDepth);

		LeftColliderObject = new GameObject("LeftCollider");
		LeftColliderObject.AddComponent<BoxCollider2D>().size = new Vector2(SideColliderDepth, Height);
		LeftColliderObject.transform.parent = transform;
		LeftColliderObject.transform.localPosition = new Vector3(-(Width / 2f - SideColliderDepth / 2f), 0f, 0f);
		LeftColliderObject.gameObject.layer = LayerMask.NameToLayer(Constant.Layer_Side);

		RightColliderObject = new GameObject("RightCollider");
		RightColliderObject.AddComponent<BoxCollider2D>().size = new Vector2(SideColliderDepth, Height);
		RightColliderObject.transform.parent = transform;
		RightColliderObject.transform.localPosition = new Vector3((Width / 2f - SideColliderDepth / 2f), 0f, 0f);
		RightColliderObject.gameObject.layer = LayerMask.NameToLayer(Constant.Layer_Side);

		if(Height > 1f)
		{
			BotColliderObject = new GameObject("BotCollider");
			BotColliderObject.AddComponent<BoxCollider2D>().size = new Vector2(Width, TopColliderDepth);
			BotColliderObject.transform.parent = transform;
			BotColliderObject.transform.localPosition = new Vector3(0f, -(Height / 2f - TopColliderDepth / 2f), 0f);
			BotColliderObject.gameObject.layer = LayerMask.NameToLayer(Constant.Layer_JumpCheck);
			BotColliderObject.AddComponent<PlatformBase>().Init(TopColliderDepth);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
