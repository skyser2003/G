using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class InvitationEvents : MonoBehaviour {

	public Text message;

	private Invitation invitation;

	private bool processed = false;
	private string inviterName = null;
	
	// Update is called once per frame
	void Update () {
		invitation = (invitation != null) ? invitation : InvitationManager.Instance.Invitation;

		if (invitation == null && !processed) {
			Debug.Log ("No invite -- go to main");
			NavigationUtils.ShowMainMenu ();
			return;
		}

		if (inviterName == null) {
			inviterName = (invitation.Inviter == null || invitation.Inviter.DisplayName == null) ? "Someone" :
				invitation.Inviter.DisplayName;
			message.text = inviterName + " Join";
		}

		if (GameManager.Instance != null) {
			switch (GameManager.Instance.State) {
			case GameManager.GameState.Aborted:
				Debug.Log ("Aborted -- back to main");
				NavigationUtils.ShowMainMenu ();
				break;

			case GameManager.GameState.Finish:
				Debug.Log ("Finish -- back to main");
				NavigationUtils.ShowMainMenu ();
				break;

			case GameManager.GameState.Playing:
				NavigationUtils.ShowPlayingPanel ();
				break;

			case GameManager.GameState.SettingUp:
				message.text = "Setting up Game...";
				break;

			case GameManager.GameState.SetupFailed:
				NavigationUtils.ShowMainMenu ();
				break;
			}
		}
	}

	public void OnAccept() {
		if (processed) {
			return;
		}

		processed = true;
		InvitationManager.Instance.Clear ();
		
		GameManager.AcceptInvitation (invitation.InvitationId);
		Debug.Log ("Accepted!,");
	}

	public void OnDecline() {
		if (processed) {
			return;
		}

		processed = true;
		InvitationManager.Instance.DeclineInvitation ();

		NavigationUtils.ShowMainMenu ();
	}
}
