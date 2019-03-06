using System;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public RestartLevel RestartLevelComponent;
    public GlitchHealth GlitchHealth = null;
	public event Action OnBossBattleEnter;
    public event Action OnEndOfZeWorldEvent; 

    // Update is called once per frame
    void Awake ()
    {
        RestartLevelComponent = GetComponent<RestartLevel>();
        if (GlitchHealth == null)
        {
            GlitchHealth = new GlitchHealth();
        }
	}

	public void StartBossBattle()
	{
		if (OnBossBattleEnter != null) 
		{
			OnBossBattleEnter ();
		}
	}

    public void EndOfZeWorldEvent()
    {
        if (OnEndOfZeWorldEvent != null)
        {
            OnEndOfZeWorldEvent();
        }
    }
}
