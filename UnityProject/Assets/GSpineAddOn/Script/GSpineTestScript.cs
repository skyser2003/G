using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine;

public class GSpineTestScript : MonoBehaviour {

	public AtlasAsset Asset1;
	public AtlasAsset Asset2;

	public Atlas Atlas1;
	public Atlas Atlas2;

	//public AtlasAsset GeneratedAtlasAsset;
	public Atlas GeneratedAtlas;
	public Texture2D createdtexture;
	public List<Rect> GenTextureRectList = new List<Rect>();
	public List<Texture2D> GenTextureList = new List<Texture2D>();
	// Use this for initialization
	void Start () {
		Init ();	
		SkelRenderer.OnReset += Apply;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
		{
			SkelRenderer.Reset();
		}
	}

	void Init()
	{
		//TextureLoader loader = new TextureLoader();
		Atlas1 = Asset1.GetAtlas();
		Atlas2 = Asset2.GetAtlas ();
		List<AtlasRegion> CombinedAtlasRegionsList = new List<AtlasRegion>();
		List<AtlasPage> CombinedAtlasPageList = new List<AtlasPage>();
		List<Material> CombineMaterialList = new List<Material>();

		foreach(Material material in Asset1.materials)
		{
			CombineMaterialList.Add (material);
		}

		foreach(Material material in Asset2.materials)
		{
			CombineMaterialList.Add (material);
		}

		foreach(AtlasRegion region in Atlas1.Regions)
		{
			//Debug.Log (region.ToString());
			CombinedAtlasRegionsList.Add(region);
		}
		foreach(AtlasRegion region in Atlas2.Regions)
		{
			//Debug.Log (region.name);
			CombinedAtlasRegionsList.Add(region);
		}


		foreach(AtlasPage page in Atlas1.Pages)
		{
			CombinedAtlasPageList.Add(page);
			//Debug.Log("page object?: " + page.rendererObject.ToString());
			//Debug.Log(page.name);
		}

		foreach(AtlasPage page in Atlas2.Pages)
		{
			CombinedAtlasPageList.Add(page);
			//Debug.Log(page.name);
		}

		foreach(Material ma in CombineMaterialList)
		{
			//Debug.Log("Ma: " + ma.mainTexture.name);
		}

		GenTextureRectList = new List<Rect>();
		GenTextureList = new List<Texture2D>();
		List<AtlasPage> GeneratedAtlasPageList = new List<AtlasPage>();
		AtlasPage GeneratedAtlasPage = new AtlasPage();
		List<AtlasRegion> GenreatedAtlasRegionList = new List<AtlasRegion>();
		foreach(AtlasRegion region in CombinedAtlasRegionsList)
		{
			//get current texture
			Texture2D curatlas = (Texture2D)((Material)region.page.rendererObject).mainTexture;
			//thier pivot is top left, unity is bot left
			int startx = 0;
			int starty = 0;
			int width = 0;
			int height = 0;
			if(!region.rotate)
			{
				startx = region.x;
				starty = region.y;
				width = region.width;
				height = region.height;
			}else
			{
				startx = region.x;
				starty = region.y;
				width = region.height;
				height = region.width;
			}

			Rect genrect = GetPosRectFromSpineToUnity(curatlas.width, curatlas.height, 
			                                          startx, starty,
			                                          width, height);

			Texture2D gentexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
			gentexture.SetPixels(0, 0, width, height, 
			                     curatlas.GetPixels((int)genrect.x, (int)genrect.y, (int)genrect.width, (int)genrect.height));

			gentexture.Apply();

			GenTextureList.Add(gentexture);
			GenreatedAtlasRegionList.Add(GetNewRegion(region));
		}

		//pack texture
		createdtexture = new Texture2D(2048, 2048, TextureFormat.RGBA32, false);
		createdtexture.name = "new atlas";
		Rect[] rects = createdtexture.PackTextures(GenTextureList.ToArray(),1,2048);

		//create page
		GeneratedAtlasPage = GetNewAtlasPage(CombinedAtlasPageList[0], createdtexture);
		GeneratedAtlasPageList.Add(GeneratedAtlasPage);

		for(int iter = 0; iter < rects.Length; iter++)
		{
			Rect currect = rects[iter];
			AtlasRegion curregion = GenreatedAtlasRegionList[iter];

			Rect convertedrect = GetUVRectFromUnityToSpine(currect);
			curregion.u = convertedrect.x;
			curregion.v = convertedrect.y;
			curregion.u2 = convertedrect.width;
			curregion.v2 = convertedrect.height;
			//if(curregion.rotate)
			//{
			//	curregion.u = currect.x;
			//	curregion.v = currect.y;
			//	curregion.u2 = currect.width;
			//	curregion.v2 = currect.height;
			//}else
			//{
			//	curregion.u = currect.x;
			//	curregion.v = currect.y;
			//	curregion.u2 = currect.x + currect.width;
			//	curregion.v2 = currect.y + currect.height;
			//}

			curregion.page = GeneratedAtlasPage;
		}

		GeneratedAtlas = new Atlas(GeneratedAtlasPageList, GenreatedAtlasRegionList);
		//GeneratedAtlasAsset = new AtlasAsset();
		//GeneratedAtlasAsset.atlasFile = GeneratedAtlas
	}

	protected AtlasRegion GetNewRegion(AtlasRegion _region)
	{
		AtlasRegion created = new AtlasRegion();

		created.page = _region.page;
		created.name = _region.name;
		created.x = _region.x;
		created.y = _region.y;
		created.width = _region.width;
		created.height = _region.height;
		
		created.u = _region.u;
		created.v = _region.v;
		created.u2 = _region.u2;
		created.v2 = _region.v2;
		
		created.offsetX = _region.offsetX;
		created.offsetY = _region.offsetY;
		created.originalHeight = _region.originalHeight;
		created.originalWidth = _region.originalWidth;
		created.index = _region.index;
		
		created.rotate = _region.rotate;
		created.splits = _region.splits;
		created.pads = _region.pads;
		
		return created;
	}

	protected AtlasPage GetNewAtlasPage(AtlasPage _origin, Texture2D _texture)
	{
		AtlasPage genatlaspage = new AtlasPage();

		genatlaspage.name = _texture.name + ".png";
		genatlaspage.format = Format.RGBA8888;
		genatlaspage.width = _texture.width;
		genatlaspage.height = _texture.height;
		genatlaspage.uWrap = _origin.uWrap;
		genatlaspage.vWrap = _origin.vWrap;
		genatlaspage.minFilter = _origin.minFilter;
		genatlaspage.magFilter = _origin.magFilter;

		Material newmat =  new Material(((Material)_origin.rendererObject));
		newmat.mainTexture = _texture;
		genatlaspage.rendererObject = (object)newmat;

		return genatlaspage;
	}


	public Rect GetUVRectFromUnityToSpine(Rect _rectinfo)
	{
		Rect genrect = new Rect();

		genrect.x = _rectinfo.x;
		genrect.width = _rectinfo.x + _rectinfo.width;
		genrect.y = (_rectinfo.y + _rectinfo.height);
		genrect.height =(_rectinfo.y);

		//Debug.Log("Input rect: " + _rectinfo + " Outpur rect: " + genrect);

		return genrect;
	}

	public Rect GetPosRectFromSpineToUnity(int _texturewidth, int _textureheight,int _startx, int _starty, int _width, int _height)
	{
		Rect genrect = new Rect();
		genrect.x = _startx;
		genrect.y = _textureheight - _starty - _height;
		genrect.width = _width;
		genrect.height = _height;
		return genrect;
	}

	public Rect GetPosRectFromUnityToSpin(int _texturewidth, int _textureheight,int _startx, int _starty, int _width, int _height)
	{
		Rect genrect = new Rect();

		return genrect;
	}

	public SkeletonDataAsset SkeletonAsset;
	[System.Serializable]
	public class SlotRegionPair {
		[SpineSlot]
		public string slot;
		
		//[SpineAtlasRegion]
		//public string region;
	}

	public SkeletonRenderer SkelRenderer;
	public SlotRegionPair[] attachments;
	protected int ApplyRegionIndexer = -1;

	public GSpineSkeletonCreatingTest SkeletonTest;

	void Apply(SkeletonRenderer skeletonRenderer) {
		Debug.Log ("cursom apply called");
		AtlasAttachmentLoader loader = new AtlasAttachmentLoader(GeneratedAtlas);
		
		float scaleMultiplier = skeletonRenderer.skeletonDataAsset.scale;
		
		//IEnumerator enumerator = attachments.GetEnumerator();
		//while (enumerator.MoveNext()) {
		//	var entry = (SlotRegionPair)enumerator.Current;
		//	var regionAttachment = loader.NewRegionAttachment(entry.region, entry.region);
		//	regionAttachment.Width = regionAttachment.RegionOriginalWidth * scaleMultiplier;
		//	regionAttachment.Height = regionAttachment.RegionOriginalHeight * scaleMultiplier;
		//	
		//	regionAttachment.SetColor(new Color(1, 1, 1, 1));
		//	regionAttachment.UpdateOffset();
		//
		//	var slot = skeletonRenderer.skeleton.FindSlot(entry.slot);
		//	slot.Attachment = regionAttachment;
		//}

		//Debug.Log("
		ApplyRegionIndexer = (ApplyRegionIndexer + 1) % GeneratedAtlas.Regions.Count;
		foreach(SlotRegionPair pair in attachments)
		{
			RegionAttachment attachment = loader.NewRegionAttachment(null, 
			                                                         GeneratedAtlas.Regions[ApplyRegionIndexer].name, 
			                                                         GeneratedAtlas.Regions[ApplyRegionIndexer].name);
			attachment.Width = attachment.regionOriginalWidth * scaleMultiplier;
			attachment.Height = attachment.RegionOriginalHeight * scaleMultiplier;

			attachment.SetColor(new Color(1, 1, 1, 1));
			attachment.UpdateOffset();

			Slot slot = skeletonRenderer.skeleton.FindSlot(pair.slot);
			slot.Attachment = attachment;

			//Debug.Log("Region: " + GeneratedAtlas.Regions[ApplyRegionIndexer].name + " Attachment: " + attachment.Name);
		}

		SkeletonTest.Generate(GeneratedAtlas);
	}
	
}
