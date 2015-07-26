using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class GameManager : NetworkManager {
	public enum GameState { SettingUp, Playing, Finish, SetupFailed, Aborted };
	private GameState mGameState = GameState.SettingUp;

	private Dictionary<string, int> mPlayerAttack = new Dictionary<string, int>();

	private int mFinishRank = 0;

	private GameManager() {
		NetworkDelegate.Add(this);
	}

	override public void Cleanup() {
		base.Cleanup ();

		NetworkDelegate.Remove(this);
	}

	public GameState State {
		get {
			return mGameState;
		}
	}

//	public new static GameManager Instance {
//		get {
//			if (sInstance == null) {
//				sInstance = new GameManager();
//			}
//			return (GameManager)sInstance;
//		}
//	}

	public int FinishRank {
		get {
			return mFinishRank;
		}
	}
}
