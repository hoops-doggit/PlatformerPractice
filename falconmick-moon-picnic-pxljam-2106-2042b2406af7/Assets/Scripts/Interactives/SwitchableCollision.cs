using UnityEngine;
using System.Collections;


public class SwitchableCollision : MonoBehaviour
{
    private SwitchableEventHub _switchableEventHub;
    private Collider2D _collider2D;
    public bool LookInParentForHub = false;

    void Awake()
    {
        _switchableEventHub = GetComponent<SwitchableEventHub>();
        bool foundHub = false;

        // if we are not the hub, go one up and look for the hub
        if (_switchableEventHub == null && LookInParentForHub)
        {
            _switchableEventHub = GetComponentInParent<SwitchableEventHub>();
        }

        if (_switchableEventHub == null)
        {
            Debug.LogError("Cannot find event hub for " + this.gameObject.name);
        }

        _collider2D = GetComponent<Collider2D>();
        _switchableEventHub.onSwitchToggleEvent += AnimateSwitch;

        AnimateSwitch(_switchableEventHub.SwitchIsOn);
    }

    public void AnimateSwitch(bool SwitchIsOn)
    {
        _collider2D.enabled = !SwitchIsOn;
    }
}
