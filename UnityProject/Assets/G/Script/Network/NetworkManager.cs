using UnityEngine;
using System.Collections;

using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

using System.Collections.Generic;

using System;

public class NetworkManager : RealTimeMultiplayerListener {

	public NetworkUpdateListener updateListener;
	public NetworkLobbyListener lobbyListener;

	private static NetworkManager _instance = null;

	static uint QuickGameOpponents = 1;
	static uint GameVariant = 0;
	static uint MinOpponents = 1;
	static uint MaxOpponents = 8;

	private byte protocolVersion = 1;
	// protocolVersion + Type(Update, Finish) + Position X + Position Y
	private int updateMessageLength = (sizeof(byte) + sizeof(byte) + sizeof(float) + sizeof(float));
	private List<byte> updateMessage;
	// protocolVersion + Type(Update, Finish) + Total Time
	private int finishMessageLength = (sizeof(byte) + sizeof(byte) + sizeof(float));

	private string mMyParticipantId = "";

	private int mResultRank = 0;

	private float mRoomSetupProgress = 0.0f;

	const float FakeProgressSpeed = 1.0f;
	const float MaxFakeProgress = 30.0f;
	float mRoomSetupStartTime = 0.0f;

	// SingleTon
	protected NetworkManager() {
		mRoomSetupStartTime = Time.time;
		updateMessage = new List<byte> (updateMessageLength);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate ();
	}

	public static NetworkManager Instance {
		get {
			if (_instance == null ) {
				_instance = new NetworkManager();
			}
			return _instance;
		}
	}

	// Participant
	internal Participant GetSelf() {
		return PlayGamesPlatform.Instance.RealTime.GetSelf ();
	}
	
	internal List<Participant> GetAllPlayers() {
		return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants ();
	}

	internal Participant GetParticipant(string participantId) {
		return PlayGamesPlatform.Instance.RealTime.GetParticipant (participantId);
	}

	// Matching Game
	public void CreateQuickGame() {
		PlayGamesPlatform.Instance.RealTime.CreateQuickGame (QuickGameOpponents, QuickGameOpponents,
		                                                    GameVariant, this);
	}

	public void CreateWithInvitationScreen() {
		PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen (MinOpponents, MaxOpponents,
		                                                               GameVariant, this);
	}

	public void AcceptFromInbox() {
		PlayGamesPlatform.Instance.RealTime.AcceptFromInbox (this);
	}

	public void AcceptInvitation(string invitationId) {
		PlayGamesPlatform.Instance.RealTime.AcceptInvitation (invitationId, this);
	}

	// Game Setup Progess
	public void OnRoomSetupProgress(float percent) {
		ShowNetworkStatus (percent + "% done with room setup");
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

	// Room
	public void OnRoomConnected(bool success) {
		if (success) {
			lobbyListener.HideLobby ();
			lobbyListener = null;
			Application.LoadLevel ("MainGame");
		} else {
		}
	}

	public void OnLeftRoom() {
		ShowNetworkStatus("We have left the room.");
		if (updateListener != null) {
			updateListener.LeftRoomConfirmed ();
		}
	}

	public void LeaveGame() {
		PlayGamesPlatform.Instance.RealTime.LeaveRoom ();
	}

	// Peer
	public void OnPeersConnected(string[] peers) {
		foreach (string participantID in peers) {
			ShowNetworkStatus ("Player " + participantID + " has joined");

			if (updateListener != null) {
				updateListener.PlayerJoinRoom(participantID);
			}
		}
	}

	public void OnPeersDisconnected(string[] peers) {
		foreach (string participantID in peers) {
			ShowNetworkStatus ("Player " + participantID + " has left");

			if (updateListener != null) {
				updateListener.PlayerLeftRoom (participantID);
			}
		}
	}

	// Participant
	public void OnParticipantLeft(Participant participant) {
		ShowNetworkStatus ("Player " + participant + " has left");
		if (updateListener != null) {
			updateListener.PlayerLeftRoom (participant.ToString());
		}
	}

	// Network Message
	private void ShowNetworkStatus(string message) {
		Debug.Log (message);
		if (lobbyListener != null) {
			lobbyListener.SetLobbyStatusMessage (message);
		}
	}

	//Network
	public void SendFinishMessage(float totalTime) {
		List<byte> bytes = new List<byte>(finishMessageLength); 
		bytes.Add (protocolVersion);
		bytes.Add ((byte)'F');
		bytes.AddRange(System.BitConverter.GetBytes(totalTime));  
		byte[] messageToSend = bytes.ToArray ();
		PlayGamesPlatform.Instance.RealTime.SendMessageToAll (true, messageToSend);
	}

	public void SendMyUpdate(float posX, float posY) {
		updateMessage.Clear ();

		updateMessage.Add (protocolVersion);

		// U -> Update, F -> Finish
		updateMessage.Add ((byte)'U');

		updateMessage.AddRange (System.BitConverter.GetBytes (posX));
		updateMessage.AddRange (System.BitConverter.GetBytes (posY));

		byte[] messageToSend = updateMessage.ToArray ();

		PlayGamesPlatform.Instance.RealTime.SendMessageToAll (false, messageToSend);
	}

	// Authenticate
	public void SignInAndStartGame(Action<bool> callback) {
		if (!PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.localUser.Authenticate ((bool success) => {
				callback(success);
				if (success) {
					Debug.Log ("Login Success: " + PlayGamesPlatform.Instance.localUser.userName);
					CreateQuickGame ();
				} else {
					Debug.Log ("Not signed in");
				}
			});
		} else {
			callback(true);
			Debug.Log ("already signed in");
			CreateQuickGame ();
		}
	}

	public void SignOut() {
		PlayGamesPlatform.Instance.SignOut ();
	}

	public bool IsAuthenticated() {
		return PlayGamesPlatform.Instance.localUser.authenticated;
	}

	public void TrySilentSignIn() {
		if (!IsAuthenticated()) {
			PlayGamesPlatform.Instance.Authenticate ((bool success) => {
				if (success) {
					Debug.Log ("Silently signed in: " + PlayGamesPlatform.Instance.localUser.userName);
				} else {
					Debug.Log ("not signed in");
				}
			}, true);
		} else {
			Debug.Log ("already signed in");
		}
	}

	// Received Message
	public void OnRealTimeMessageReceived(bool isReliable, string senderID, byte[] data) {
		byte messageVersion = (byte)data [0];
		char messageType = (char)data [1];

		if (messageType == 'U' && data.Length == updateMessageLength) {
			float posX = System.BitConverter.ToSingle(data, 2);
			float posY = System.BitConverter.ToSingle(data, 6);

			Debug.Log ("Player: " + senderID + " ,Pos(" + posX + ", " + posY + ")");

			if (updateListener != null) {
				updateListener.UpdateReceived(senderID, posX, posY);
			}
		} else if (messageType == 'F' && data.Length == finishMessageLength) {
			float finalTime = System.BitConverter.ToSingle(data, 2);
			Debug.Log ("Player: " + senderID + " has Finished, Time : " + finalTime);

			if (updateListener != null) {
				updateListener.StageFinished(senderID, finalTime);
			}
		}
	}

// MUST OVERRIDE

	virtual public void Cleanup() {
		PlayGamesPlatform.Instance.RealTime.LeaveRoom ();
		_instance = null;
	}
}
