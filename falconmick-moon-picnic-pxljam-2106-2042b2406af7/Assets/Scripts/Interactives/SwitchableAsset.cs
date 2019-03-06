using System;
using UnityEngine;
using System.Collections;

public class SwitchableAsset : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private SwitchableEventHub _switchableEventHub;
    public Sprite SwitchOnSprite;
    public Sprite SwitchOffSprite;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _switchableEventHub = GetComponent<SwitchableEventHub>();

        _switchableEventHub.onSwitchToggleEvent += AnimateSwitch;

        if (SwitchOnSprite == null || SwitchOffSprite == null)
        {
            Debug.LogError("You have not set the Switch Sprites for " + this.gameObject.name);
        }

        AnimateSwitch(_switchableEventHub.SwitchIsOn);
    }



    public void AnimateSwitch(bool SwitchIsOn)
    {
        _spriteRenderer.sprite = SwitchIsOn ? SwitchOnSprite : SwitchOffSprite;
        //Debug.Log("(Asset switch) Set " + this.gameObject.name + " to state: " + SwitchIsOn.ToString());
    }
}
