using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GameMenuEvents : MonoBehaviour {
	
	private System.Action<bool> mAuthCallback;
	private bool mAuthOnStart = true;
	private bool mSigningIn = false;
	
	// Use this for initialization
	void Start () {
		mAuthCallback = (bool success) => {
			Debug.Log ("In Auth callback, success: " + success);
			
			mSigningIn = false;
			if (success) {
				NavigationUtils.ShowMainMenu ();
			} else {
				Debug.Log ("Auth Failed");
			}
		};
		
		var config = new PlayGamesClientConfiguration.Builder ()
			.WithInvitationDelegate (InvitationManager.Instance.OnInvitationReceived)
				.Build ();
		PlayGamesPlatform.InitializeInstance (config);
		PlayGamesPlatform.DebugLogEnabled = true;
		
		if (mAuthOnStart) {
			Authorize (true);
		}
	}
	
	public void AuthBtnClk() {
		Authorize (false);
	}
	
	// Update is called once per frame
	void Authorize (bool silent) {
		if (!mSigningIn) {
			Debug.Log ("Sign-In");
			PlayGamesPlatform.Instance.Authenticate (mAuthCallback, silent);
		} else {
			Debug.Log ("Already Started Signing in");
		}
	}
}
