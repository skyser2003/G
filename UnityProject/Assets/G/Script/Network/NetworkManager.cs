using UnityEngine;
using System.Collections;

using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

using System.Collections.Generic;

public class NetworkManager : RealTimeMultiplayerListener {

	internal HashSet<RealTimeMultiplayerListener> NetworkDelegate = new HashSet<RealTimeMultiplayerListener>();

	const string MapName = "Tutorial-1";

	static uint QuickGameOpponents = 1;
	static uint GameVariant = 0;
	static uint MinOpponents = 1;
	static uint MaxOpponents = 8;

	internal static NetworkManager sInstance = null;

	private string mMyParticipantId = "";

	private int mResultRank = 0;

	private float mRoomSetupProgress = 0.0f;

	const float FakeProgressSpeed = 1.0f;
	const float MaxFakeProgress = 30.0f;
	float mRoomSetupStartTime = 0.0f;

	protected NetworkManager() {
		mRoomSetupStartTime = Time.time;
	}

	public static void CreateQuickGame() {
		sInstance = new NetworkManager ();
		PlayGamesPlatform.Instance.RealTime.CreateQuickGame (QuickGameOpponents, QuickGameOpponents,
		                                                    GameVariant, sInstance);
	}

	public static void CreateWithInvitationScreen() {
		sInstance = new NetworkManager ();
		PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen (MinOpponents, MaxOpponents,
		                                                               GameVariant, sInstance);
	}

	public static void AcceptFromInbox() {
		sInstance = new NetworkManager ();
		PlayGamesPlatform.Instance.RealTime.AcceptFromInbox (sInstance);
	}

	public static void AcceptInvitation(string invitationId) {
		sInstance = new NetworkManager ();
		PlayGamesPlatform.Instance.RealTime.AcceptInvitation (invitationId, sInstance);
	}

	public static NetworkManager Instance {
		get {
			return sInstance;
		}
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

// MUST OVERRIDE
	public void OnRoomConnected(bool success) {
		foreach (RealTimeMultiplayerListener listener in NetworkDelegate) {
			listener.OnRoomConnected (success);
		}
	}

	public void OnLeftRoom() {
		foreach (RealTimeMultiplayerListener listener in NetworkDelegate) {
			listener.OnLeftRoom ();
		}
	}

	public void OnPeersConnected(string[] peers) {
	}

	public void OnParticipantLeft(Participant participant) {
	}

	public void OnPeersDisconnected(string[] peers) {
		foreach (RealTimeMultiplayerListener listener in NetworkDelegate) {
			listener.OnPeersDisconnected (peers);
		}
	}

	public void OnRoomSetupProgress(float percent) {
		mRoomSetupProgress = percent;
	}

	public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {
		foreach (RealTimeMultiplayerListener listener in NetworkDelegate) {
			listener.OnRealTimeMessageReceived (isReliable, senderId, data);
		}
	}

	virtual public void Cleanup() {
		PlayGamesPlatform.Instance.RealTime.LeaveRoom ();
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
