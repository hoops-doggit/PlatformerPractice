using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class GlitchSpeedController : MonoBehaviour
{
    private GameManager _gameManager;
    private Animator _animator;
    private float randomWait;

    void Start()
    {
        this._gameManager = GameManagerGetter.GetManager();
        this._animator = GetComponent<Animator>();

        this._gameManager.GlitchHealth.SetGlitchAnimationDelegate += GlitchAnimationDelegate;
        GlitchAnimationDelegate(0f);

        this.randomWait = Random.Range(0f, 0.3f);

        StartCoroutine(ApplyRanomGlitch());
    }

    private void GlitchAnimationDelegate(float speed)
    {
        this._animator.speed = speed;
    }

    private IEnumerator ApplyRanomGlitch()
    {
        for (;;)
        {
            var skipTo = this._animator.speed + 0.3f;
            this._animator.Play("Glitch Ground", -1, skipTo);

            var skipToWait = ((1.1f - skipTo) + this.randomWait) * 5;
            yield return new WaitForSecondsRealtime(0.1f);

            this._animator.Play("Glitch Ground", -1, 0f);
            yield return new WaitForSeconds(skipToWait);
        }
    }
}
