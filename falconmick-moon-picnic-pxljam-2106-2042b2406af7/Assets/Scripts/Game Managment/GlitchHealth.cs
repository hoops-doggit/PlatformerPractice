using System;
using UnityEngine;
using System.Collections;

public class GlitchHealth : ScriptableObject
{
    public int glitchHealthPoints = 20;
    public event Action<float> SetGlitchAnimationDelegate;
    public event Action OnGlitchingStart;
    public event Action OnGlitchingEnd;
    private bool _isGlitching = false;

    public bool IsGlitching(int currentHp)
    {
        return currentHp <= this.glitchHealthPoints;
    }

    public void RunOnPlayerHealthChangeEvent(int currentHp)
    {
        var isGlitchingNew = IsGlitching(currentHp);
        if (isGlitchingNew != _isGlitching)
        {
            if (isGlitchingNew)
            {
                if (OnGlitchingStart != null)
                {
                    OnGlitchingStart();
                }
            }
            else
            {
                if (OnGlitchingEnd != null)
                {
                    OnGlitchingEnd();
                }
            }
        }

        if (SetGlitchAnimationDelegate != null)
        {
            var inverseSpeed = currentHp / 40f;
            var animationSpeed = 1 - inverseSpeed;

            animationSpeed = Mathf.Clamp(animationSpeed, 0f, 1f);
            SetGlitchAnimationDelegate(animationSpeed);
        }
    }
}
