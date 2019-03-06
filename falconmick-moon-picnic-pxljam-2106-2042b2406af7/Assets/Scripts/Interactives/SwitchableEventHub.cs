using System;
using UnityEngine;
using System.Collections;

public class SwitchableEventHub : MonoBehaviour {

    public event Action<bool> onSwitchToggleEvent;
    public bool SwitchIsOn = false;

    public void FlipSwitch()
    {
        if (onSwitchToggleEvent != null)
        {
            onSwitchToggleEvent(SwitchIsOn);
        }
    }
}
