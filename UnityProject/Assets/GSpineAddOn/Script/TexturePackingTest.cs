using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TexturePackingTest : MonoBehaviour {

	public SpriteRenderer Displayer;
	public Texture2D CreatedTexture;
	public List<Texture2D> CombineList;

	protected int RectIndex = 0;
	public List<Rect> RectList = new List<Rect>();
	public int TargetWidth = 2048;
	public int TargetHeight = 2048;
	// Use this for initialization
	void Start () {
		CombineTexture();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			ChangeRect();
		}
	}

	public void ChangeRect()
	{
		RectIndex = (RectIndex + 1) % RectList.Count;

		Debug.Log("Cur r: " + RectList[RectIndex]);
		Sprite sp = Sprite.Create(CreatedTexture, RectList[RectIndex], Vector2.zero);

		Displayer.sprite = sp;
	}
	public List<Texture2D> GenTextureList;
	public void CombineTexture()
	{
		CreatedTexture = new Texture2D(TargetWidth, TargetHeight, TextureFormat.ARGB32, false);
		//CreatedTexture.Resize(TargetHeight, TargetWidth);

		List<Sprite> testsprites = new List<Sprite>();
		GenTextureList = new List<Texture2D>();
		foreach(Texture2D texture in CombineList)
		{
			int width = texture.width;
			int height = texture.height;
			int widthcount = 10;
			int heightcount = 10;
			for(int w = 0; w < widthcount - 1; w++)
			{
				for(int h = 0; h < heightcount - 1; h++)
				{
					int widthlength = width / widthcount;
					int heightlength = height / heightcount;
					int curstartx = widthlength  * w;
					int curstary = heightlength * h;

					Texture2D gentexture = new Texture2D(widthlength, heightlength, TextureFormat.ARGB32, false);
					gentexture.SetPixels(texture.GetPixels(curstartx, curstary, widthlength, heightlength));
					gentexture.Apply();
					//Sprite cursprite = Sprite.Create(texture, new Rect(curstartx, curstary, widthlength - 1, heightlength - 1), Vector2.zero);
					GenTextureList.Add(gentexture);
				}
			}
		}

		Rect[] rects;

		rects = CreatedTexture.PackTextures(GenTextureList.ToArray(),1, 4096);
		TargetWidth = CreatedTexture.width;
		TargetHeight = CreatedTexture.height;

		foreach(Rect r in rects)
		{
			//Debug.Log("Rect: " + r);
			RectList.Add(new Rect(r.x * TargetWidth, r.y * TargetHeight, r.width * TargetWidth, r.height * TargetHeight));
		}

		RectIndex = 0;

		Sprite sp = Sprite.Create(CreatedTexture, RectList[RectIndex], Vector2.zero);

		Displayer.sprite = sp;
	}
}
