using UnityEngine;
using System.Collections;
using Prime31;

public class RemoveLayerCollisionSwitch : MonoBehaviour
{
    private CharacterController2D _characterController2D;
    private SwitchableEventHub _switchableEventHub;

    void Awake()
    {
        _characterController2D = GetComponent<CharacterController2D>();
        _switchableEventHub = GetComponent<SwitchableEventHub>();

        _switchableEventHub.onSwitchToggleEvent += AnimateSwitch;

        AnimateSwitch(_switchableEventHub.SwitchIsOn);
    }



    public void AnimateSwitch(bool SwitchIsOn)
    {
        var defaultLayer = LayerMask.NameToLayer(LayerDefinitions.Default);
        var defaultLayerBit = 1 << defaultLayer;
        var glitchLayer = LayerMask.NameToLayer(LayerDefinitions.Glitch);
        var glitchLayerBit = 1 << glitchLayer;

        if (SwitchIsOn)
        {
            _characterController2D.platformMask = defaultLayerBit;
        }
        else
        {
            _characterController2D.platformMask = defaultLayerBit | glitchLayerBit;
        }
    }
}
