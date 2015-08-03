using UnityEngine;
using System.Collections;

using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

using System.Collections.Generic;

using System;

public class GameManager : RealTimeMultiplayerListener {
	string MapName = "Tutorial-1";
	public enum GameState { SettingUp, Playing, Finish, SetupFailed, Aborted };
	private GameState mGameState = GameState.SettingUp;

	private PlayerInfo[] players;
	private Dictionary<string, int> mPlayerAttack = new Dictionary<string, int>();

	private int mFinishRank = 0;

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

	private void TearDownMap() {
		BehaviorUtils.MakeVisible (GameObject.Find (MapName), false);
		foreach(PlayerInfo player in players) {
			GameObject playerCharacter = GameObject.Find(player.name);
//			playerCharacter.GetComponent<playerController>().Reset();
			BehaviorUtils.MakeVisible(playerCharacter, false);
		}
	}

	public void OnRoomConnected(bool success) {
		if (success) {
			mGameState = GameState.Playing;
			mMyParticipantId = GetSelf ().ParticipantId;
			SetupMap ();
		} else {
			mGameState = GameState.SetupFailed;
		}
	}
	
	// Network
	static uint QuickGameOpponents = 1;
	static uint GameVariant = 0;
	static uint MinOpponents = 1;
	static uint MaxOpponents = 8;
	
	internal static GameManager sInstance = null;

	public static GameManager Instance {
		get {
			return sInstance;
		}
	}
	
	private string mMyParticipantId = "";
	
	private int mResultRank = 0;
	
	private float mRoomSetupProgress = 0.0f;
	
	const float FakeProgressSpeed = 1.0f;
	const float MaxFakeProgress = 30.0f;
	float mRoomSetupStartTime = 0.0f;
	
	private GameManager() {
		mRoomSetupStartTime = Time.time;
	}
	
	public static void CreateQuickGame() {
		sInstance = new GameManager ();
		PlayGamesPlatform.Instance.RealTime.CreateQuickGame (QuickGameOpponents, QuickGameOpponents,
		                                                     GameVariant, sInstance);
	}
	
	public static void CreateWithInvitationScreen() {
		sInstance = new GameManager ();
		PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen (MinOpponents, MaxOpponents,
		                                                                GameVariant, sInstance);
	}
	
	public static void AcceptFromInbox() {
		sInstance = new GameManager ();
		PlayGamesPlatform.Instance.RealTime.AcceptFromInbox (sInstance);
	}
	
	public static void AcceptInvitation(string invitationId) {
		sInstance = new GameManager ();
		PlayGamesPlatform.Instance.RealTime.AcceptInvitation (invitationId, sInstance);
	}
	
	public float RoomSetupProgress {
		get {
			float fakeProgress = (Time.time - mRoomSetupStartTime) * FakeProgressSpeed;
			if (fakeProgress > MaxFakeProgress) {
				fakeProgress = MaxFakeProgress;
			}
			float progress = mRoomSetupProgress + fakeProgress;
			
			return progress < 99.0f ? progress : 99.0f;
		}
	}

	public void OnLeftRoom() {
		if (mGameState != GameState.Finish ) {
			mGameState = GameState.Aborted;
		}
	}
	
	public void OnPeersConnected(string[] peers) {
	}
	
	public void OnParticipantLeft(Participant participant) {
	}
	
	public void OnPeersDisconnected(string[] peers) {
		foreach (string perr in peers) {
		}
	}
	
	public void OnRoomSetupProgress(float percent) {
		mRoomSetupProgress = percent;
	}
	
	public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {
	}
	
	virtual public void Cleanup() {
		PlayGamesPlatform.Instance.RealTime.LeaveRoom ();
		TearDownMap ();
		mGameState = GameState.Aborted;
		sInstance = null;
	}
	
	internal Participant GetSelf() {
		return PlayGamesPlatform.Instance.RealTime.GetSelf ();
	}
	
	internal List<Participant> GetPlayers() {
		return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants ();
	}
	
	internal Participant GetParticipant(string participantId) {
		return PlayGamesPlatform.Instance.RealTime.GetParticipant (participantId);
	}
}
