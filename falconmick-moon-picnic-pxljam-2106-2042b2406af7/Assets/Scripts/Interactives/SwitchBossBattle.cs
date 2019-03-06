using UnityEngine;
using System.Collections;

public class SwitchBossBattle : MonoBehaviour {
	private GameManager _gameManager;
	private SwitchableEventHub _switchableEventHub;

	void Awake() {
		_gameManager = GameManagerGetter.GetManager ();
		_switchableEventHub = GetComponent<SwitchableEventHub>();

		if (_switchableEventHub == null)
		{
			Debug.LogError("Cannot find event hub for " + this.gameObject.name);
		}

		_switchableEventHub.onSwitchToggleEvent += AnimateSwitch;

	}

	public void AnimateSwitch(bool SwitchIsOn)
	{
		_gameManager.StartBossBattle ();
	}
}
