using UnityEngine;
using System.Collections;

using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

using System.Collections.Generic;

using System;

public class GameManager : MonoBehaviour, NetworkUpdateListener {
	// My Player
	private GameObject myPlayer = null;

	// Network
	public NetworkManager networkManager = null;

	// Player Manager
	private PlayersManager playersManager = null;
	private string myParticipantID;

	// Game State
	public enum GameState { None, SettingUp, Playing, Finish, SetupFailed, Aborted };
	private GameState mGameState = GameState.None;

	private int mFinishRank = 0;
	
	public string mapName = null;

	// Deal Meter
	private Dictionary<string, float> damageDealMeter = null;

	// Player Position - Using Unity Inspector
	public List<Vector3> playerInitPos = new List<Vector3>();

	// Playing Time
	private float playedTime;

	// Next broadcast Time
	private float nextBroadcastTime = 0;
	// broadcast gap
	private float broadcastGap = .16f;

	private static GameManager _instance = null;

	protected GameManager() {
		// NetworkManager initialized
		networkManager = NetworkManager.Instance;

		// PlayersManager initialized
		playersManager = PlayersManager.Instance;

		playedTime = 0;

		mGameState = GameState.SettingUp;
	}
	
	public static GameManager Instance {
		get {
			if (_instance == null ) {
				_instance = new GameManager();
			}
			return _instance;
		}
	}

	public void SignInAndStartGame() {
		Debug.Log ("SignInAndStartGame Btn Clk");
		networkManager.SignInAndStartGame ((bool success) => {
			Debug.Log ("SignIn: " + networkManager.IsAuthenticated());
			
			networkManager.updateListener = this;
			
			// My Participant
			myParticipantID = networkManager.GetSelf ().ParticipantId;
			
			List<Participant> allPlayers = networkManager.GetAllPlayers ();
			damageDealMeter = new Dictionary<string, float> (allPlayers.Count);
		});
	}

	public GameState State {
		get {
			return mGameState;
		}
	}

	public int FinishRank {
		get {
			return mFinishRank;
		}
	}

	private void SetupMap () {

	}

	// Update Logic
	void Update() {
	}

	// Network Update Logic
	void DoNetworkUpdate() {
		playedTime += Time.deltaTime;

		if (Time.time > nextBroadcastTime) {
			networkManager.SendMyUpdate (myPlayer.transform.position.x,
			                             myPlayer.transform.position.y);

			nextBroadcastTime = Time.time + broadcastGap;
		}
	}

	// Network Update Interface
	public void UpdateReceived(string participantID, float posX, float posY) {
	}

	public void StageFinished(string senderID, float finishTime) {
	}

	public void LeftRoomConfirmed() {
	}

	public void PlayerJoinRoom(string participantID) {
		if (playersManager.IsAlreadyPlayerParticipantID (participantID)) {
			Debug.Log ("Player is Already");
			return;
		}

		// Player Initialized
		PlayerInfo playerInfo = new PlayerInfo ();
		playerInfo.ParticipantID = participantID;

		GameObject playerTemplate = GameObject.Find ("PlayerCharacter1");
		GameObject_Player templateInfo = playerTemplate.GetComponent<GameObject_Player> ();
		MoveObjectBase templateMove = playerTemplate.GetComponent<MoveObjectBase> ();
		GAnimation_Animator templateAnimator = playerTemplate.GetComponent<GAnimation_Animator> ();
		GAttackPatternBase templateAttackPattern = playerTemplate.GetComponent<GAttackPatternBase> ();

		playerInfo.GameObjectPlayerComp = templateInfo;

		// Initialized Player Position
		Vector3 initPos = Vector3.zero;
		int playersCount = playersManager.PlayersCount();
		if (playersCount == playerInitPos.Count) {
			System.Random rand = new System.Random();
			int initPosIndex = rand.Next(0, playerInitPos.Count);
			initPos = playerInitPos[initPosIndex];
		} else {
			initPos = playerInitPos[playersCount];
		}
		// initPos is already other player, initPos y axis + 40
		if (playersManager.IsCollisionPlayerPos(initPos)) {
			initPos.y += 40;
		}
		playerInfo.GameObjectPlayerComp.transform.position = initPos;

		playersManager.PlayerAdd (playerInfo);
	}

	public void PlayerLeftRoom(string participantID) {
	}
}
