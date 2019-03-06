using System;
using UnityEngine;
using System.Collections;

public class HideOnGlitch : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private GameManager _gameManager;

    void Start()
    {
        this._spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManager = GameManagerGetter.GetManager();

        _gameManager.GlitchHealth.OnGlitchingStart += GlitchHealthOnOnGlitchingStart;
        _gameManager.GlitchHealth.OnGlitchingEnd += GlitchHealthOnOnGlitchingEnd;
    }

    private void GlitchHealthOnOnGlitchingEnd()
    {
        Debug.Log("Showing bg");
        _spriteRenderer.enabled = true;
    }

    private void GlitchHealthOnOnGlitchingStart()
    {
        StartCoroutine(HideAfter(0.1f));
    }

    private IEnumerator HideAfter(float timeToHideAfter)
    {
        yield return new WaitForSeconds(timeToHideAfter);
        _spriteRenderer.enabled = false;
    }
}
