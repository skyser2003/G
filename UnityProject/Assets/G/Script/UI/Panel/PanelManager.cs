using UnityEngine;
using System.Collections;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {

	public GameObject authorizePanel;

	public GameObject mainMenu;
	public GameObject gameMenu;

	public GameObject playingPanel;
	public GameObject invitationPanel;

	private GameObject currentPanel;
	private GameObject prevSelected;

	// Use this for initialization
	void Start () {
		OpenPanel (authorizePanel);
	}

	public void OpenPlayingPanel() {
		OpenPanel (playingPanel);
	}

	public void OpenAutorizeMenu() {
		OpenPanel (authorizePanel);
	}

	public void OpenMainMenu() {
		OpenPanel (mainMenu);
	}

	public void OpenInvitationPanel() {
		OpenPanel (invitationPanel);
	}

	void OpenPanel(GameObject panel) {
		if (currentPanel == panel) {
			return;
		}

		if (currentPanel != null) {
			ClosePanel (currentPanel);
		}

		panel.gameObject.SetActive (true);
		prevSelected = EventSystem.current.currentSelectedGameObject;
		currentPanel = panel;

		Selectable[] items = panel.GetComponentsInChildren<Selectable> (true);

		foreach (Selectable s in items) {
			if (s.IsActive () && s.IsInteractable ()) {
				EventSystem.current.SetSelectedGameObject (s.gameObject);
				break;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		StandaloneInputModule module = EventSystem.current.currentInputModule as StandaloneInputModule;

		if (Input.GetKey (KeyCode.JoystickButton0)) {
			if (module != null && module.inputMode == StandaloneInputModule.InputMode.Mouse) {
				Button btn = EventSystem.current.currentSelectedGameObject.GetComponent<Button> ();
				if (btn) {
					ExecuteEvents.Execute (EventSystem.current.currentSelectedGameObject,
					                      null, ExecuteEvents.submitHandler);
				}
			}
		}
	}

	void ClosePanel(GameObject panel) {
		panel.SetActive (false);
		EventSystem.current.SetSelectedGameObject (prevSelected);
	}
}
