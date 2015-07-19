using UnityEngine;
using System.Collections;

public class GameObjectPart : GameObjectBase {

	public GameObjectBase BaseObject;

	public override void Process (float _deltatime)
	{

	}

	public override void Hit (float _damage)
	{
		base.Hit (_damage);

		if(!IsDead)
		{
			BaseObject.Hit(_damage);
		}
	}
}
