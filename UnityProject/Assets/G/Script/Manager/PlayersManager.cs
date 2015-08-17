using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayersManager {
	private List<PlayerInfo> players = new List<PlayerInfo>();

	private static PlayersManager _instance = null;
	protected PlayersManager() {

	}
	
	public static PlayersManager Instance {
		get {
			if (_instance == null ) {
				_instance = new PlayersManager();
			}
			return _instance;
		}
	}

	// Count Player
	public int PlayersCount() {
		return players.Count;
	}

	// Player Control
	public bool IsAlreadyPlayerParticipantID(string participantID) {
		bool isAlready = false;
		foreach (PlayerInfo player in players) {
			if (player.ParticipantID == participantID) {
				isAlready = true;
				break;
			}
		}

		return isAlready;
	}
	public bool PlayerAdd(PlayerInfo playerInfo) {
		bool isFindPlayer = false;
		foreach (PlayerInfo player in players) {
			if (player.ParticipantID == playerInfo.ParticipantID) {
				isFindPlayer = true;
				break;
			}
		}

		if (!isFindPlayer) {
			players.Add (playerInfo);
		}

		return !isFindPlayer;
	}

	public void PlayerRemove(string participantID) {
		foreach (PlayerInfo player in players) {
			if (player.ParticipantID == participantID) {
				players.Remove(player);
				break;
			}
		}
	}

	// Player Position
	public bool IsCollisionPlayerPos(Vector3 pos) {
		foreach (PlayerInfo player in players) {
			if (player.transform.position.Equals (pos)) {
				return true;
			}
		}

		return false;
	}
}
