using System;
using UnityEngine;
using System.Collections;

public class SwitchConctoller : Interactable
{

    private string _uniqueName;
    public SwitchableEventHub[] TargetSwitch = new SwitchableEventHub[0];
    public bool AllowToggle = false;
    public bool SwitchIsOn = false;

    private bool _initialState;

    void Awake()
    {
        _uniqueName = Guid.NewGuid().ToString();
        for (int i = 0; i < TargetSwitch.Length; i++)
        {
            TargetSwitch[i].FlipSwitch();
        }

        _initialState = SwitchIsOn;
    }

    protected override string GetUniqueName()
    {
        return _uniqueName;
    }

    /// <summary>
    /// Flips the switch either On or Off.
    /// 
    /// If the switch is already on, unless allowOff is true
    /// 
    /// returns current state of switch
    /// </summary>
    public bool FlipSwitch(bool allowOff = false)
    {
        SwitchIsOn = !SwitchIsOn;

        if (!allowOff)
        {
            SwitchIsOn = !_initialState;
        }
        
        for (int i = 0; i < TargetSwitch.Length; i++)
        {
            TargetSwitch[i].SwitchIsOn = SwitchIsOn;
        }


        return SwitchIsOn;
    }

    public override object Interact(object arg = null)
    {
        if (CanInteract())
        {
            FlipSwitch(AllowToggle);
            for (int i = 0; i < TargetSwitch.Length; i++)
            {
                TargetSwitch[i].FlipSwitch();
            }
        }
        return null;
    }
}
