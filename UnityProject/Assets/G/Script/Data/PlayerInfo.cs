using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
	public string ParticipantID = null;

	#region GameObjectPlayer
	public GameObject_Player GameObjectPlayerComp;

	public void UpdatePlayerPos(float posX, float posY) {
		if (GameObjectPlayerComp == null) {
			return;
		}

		GameObjectPlayerComp.MoveObject.InnerForce.x = posX;
		GameObjectPlayerComp.MoveObject.InnerForce.y = posY;
	}

	public byte[] SerializedPlayerInfo() {
		return null;
	}
	#endregion
}