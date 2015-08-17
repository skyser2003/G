public interface NetworkLobbyListener {
	void SetLobbyStatusMessage(string message);
	void HideLobby();
}

public interface NetworkUpdateListener {
	void UpdateReceived(string participantID, float posX, float posY);
	void StageFinished(string senderID, float finishTime);
	void LeftRoomConfirmed();
	void PlayerJoinRoom(string participantID);
	void PlayerLeftRoom(string participantID);
}