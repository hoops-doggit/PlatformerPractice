using System;
using UnityEngine;
using System.Collections;

public class AddThisSwitchController : MonoBehaviour
{

    private SwitchConctoller _switchConctoller;

    void Awake()
    {
        var foundMyGo = false;
        _switchConctoller = GetComponent<SwitchConctoller>();
        for (int i = 0; i < _switchConctoller.TargetSwitch.Length; i++)
        {
            if (_switchConctoller.TargetSwitch[i].gameObject == this.gameObject)
            {
                foundMyGo = true;
            }
        }

        if (!foundMyGo)
        {
            Array.Resize(ref _switchConctoller.TargetSwitch, _switchConctoller.TargetSwitch.Length + 1);
            var eventHub = GetComponent<SwitchableEventHub>();
            _switchConctoller.TargetSwitch[_switchConctoller.TargetSwitch.Length - 1] = eventHub;
        }
    }
}
