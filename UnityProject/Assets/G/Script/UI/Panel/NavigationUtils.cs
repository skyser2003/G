using UnityEngine;
using System.Collections;

using UnityEngine.EventSystems;

public class NavigationUtils {

	private static PanelManager panelManager;
	public static PanelManager PanelManager {
		get {
			if (PanelManager == null) {
				panelManager = EventSystem.current.GetComponent<PanelManager> ();
			}
			return PanelManager;
		}
	}

	public static void ShowAuthorizeMenu () {
		PanelManager manager = NavigationUtils.PanelManager;
		
		if (manager != null) {
			Debug.Log ("Show MainMenu");
			manager.OpenAutorizeMenu ();
		} else {
			Debug.LogWarning ("NavigationUtils - ShowMainMenu: PanelManager is null");
		}
	}

	public static void ShowMainMenu() {
		PanelManager manager = NavigationUtils.PanelManager;

		if (manager != null) {
			Debug.Log ("Show MainMenu");
			manager.OpenMainMenu ();
		} else {
			Debug.LogWarning ("NavigationUtils - ShowMainMenu: PanelManager is null");
		}
	}

	public static void ShowInvitationPanel () {
		PanelManager manager = NavigationUtils.PanelManager;
		
		if (manager != null) {
			Debug.Log ("Show MainMenu");
			manager.OpenInvitationPanel ();
		} else {
			Debug.LogWarning ("NavigationUtils - ShowMainMenu: PanelManager is null");
		}
	}

	public static void ShowPlayingPanel() {
		PanelManager manager = NavigationUtils.PanelManager;
		
		if (manager != null) {
			Debug.Log ("Show MainMenu");
			manager.OpenPlayingPanel ();
		} else {
			Debug.LogWarning ("NavigationUtils - ShowMainMenu: PanelManager is null");
		}
	}

}
