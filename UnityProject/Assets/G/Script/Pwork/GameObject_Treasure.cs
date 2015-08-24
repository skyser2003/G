using UnityEngine;
using System.Collections;

public class GameObject_Treasure : GameObjectBase {

	protected bool TreasureGiven = false;

	public override void Process (float _deltatime)
	{
		base.Process (_deltatime);
	}

	public void CheckTreasureDrop()
	{
		if(IsDead)
		{
			if(!TreasureGiven)
			{
				DropTreasure();
			}
		}
	}

	public void DropTreasure()
	{
		TreasureGiven = true;
	}
}
