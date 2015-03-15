using UnityEngine;
using System.Collections;
using Spine;

public class GSpineSkeletonCreatingTest : MonoBehaviour {

	public SkeletonRenderer MySkeletonRender;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Generate(Atlas _genatlas)
	{
		Debug.Log("Generate Called slot count: " + MySkeletonRender.skeleton.Slots.Count);
		AtlasAttachmentLoader loader = new AtlasAttachmentLoader(_genatlas);		
		float scaleMultiplier = MySkeletonRender.skeletonDataAsset.scale;

		Debug.Log(MySkeletonRender.initialSkinName);

		Skin genskin = new Skin("Generated Skin");
		foreach(AtlasRegion region in _genatlas.Regions)
		{
			Debug.Log("regions: " + region.name);
		}

		foreach(Slot slot in MySkeletonRender.skeleton.Slots)
		{
			if(slot.Attachment == null)
			{
				continue;
			}
			
			Debug.Log("Change Slot: " + slot.Attachment.Name);
			AtlasRegion findregion = _genatlas.Regions[0];
			bool found = false;
			foreach(AtlasRegion region in _genatlas.Regions)
			{
				if(region.name == slot.Attachment.Name)
				{
					found = true;
					findregion = region;
					Debug.Log("Region match Found: " + region.name);
					break;
				}
				else if(region.name == slot.Skeleton.Skin.Name + "/" + slot.Attachment.Name)
				{
					found = true;
					findregion = region;
					Debug.Log("Region match Found: " + region.name);
					break;
				}
			}

			if(found)
			{
				//Debug.Log("?:"  + slot.Bone.Data.);
				Attachment originattachment = slot.Attachment;
				if(originattachment is RegionAttachment)
				{
					Debug.Log("RegionAttachment attach");
					RegionAttachment curattachment = (RegionAttachment)originattachment;
					RegionAttachment attachment = loader.NewRegionAttachment(null, 
					                                                         findregion.name, 
					                                                         findregion.name);

					attachment.Width = curattachment.Width;
					attachment.Height = curattachment.Height;
					attachment.X = curattachment.X;
					attachment.y = curattachment.y;
					attachment.Rotation = curattachment.Rotation;
					attachment.ScaleX = curattachment.ScaleX;
					attachment.ScaleY = curattachment.ScaleY;

					attachment.SetColor(new Color(1, 1, 1, 1));
					attachment.UpdateOffset();
					
					slot.Attachment = attachment;
				}else if(originattachment is MeshAttachment)
				{
					Debug.Log("MeshAttachment attach");
				}else if(originattachment is SkinnedMeshAttachment)
				{
					Debug.Log("SkinnedMeshAttachment attach");
				}

				//RegionAttachment attachment = loader.NewRegionAttachment(null, 
				//                                                         findregion.name, 
				//                                                         findregion.name);
				//attachment.Width = attachment.regionOriginalWidth * scaleMultiplier;
				//attachment.Height = attachment.RegionOriginalHeight * scaleMultiplier;
				//attachment.X = slot.Bone.Data.X;
				//attachment.Y = slot.Bone.Data.Y; 
				//attachment.Rotation = slot.Bone.Data.Rotation;
				//attachment.ScaleX = slot.Bone.Data.ScaleX;
				//attachment.ScaleY = slot.Bone.Data.ScaleY;
				//attachment.RegionOffsetX = 0;

				//Debug.Log("Origin X Y REGIONX REGIONY " + slot.attachment.X + " " + attachment.Y
				//          + " " + slot.Bone.Data.X + " " + slot.Bone.Data.Y);
				//
				//Debug.Log("X Y REGIONX REGIONY " + attachment.X + " " + attachment.Y
				//          + " " + slot.Bone.Data.X + " " + slot.Bone.Data.Y);
				//Debug.Log("Bone name: " + slot.Bone.Data.Name + slot.Bone.Data.X);


				//attachment.SetColor(new Color(1, 1, 1, 1));
				//attachment.UpdateOffset();
				//
				//slot.Attachment = attachment;
			}else
			{
				//genskin.AddAttachment(MySkeletonRender.skeleton.FindSlotIndex(slot.Data.Name), slot.Data.Name, slot.Attachment);
				slot.Attachment = null;
				//Debug.Log ("wtf!?: " + slot.Attachment
			}
		}

		//MySkeletonRender.skeleton.SetSkin(genskin);
		//MySkeletonRender.skeleton.SetSlotsToSetupPose();
	}
}
