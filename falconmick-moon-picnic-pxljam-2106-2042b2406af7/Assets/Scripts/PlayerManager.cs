using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerManager : MonoBehaviour
{
	public PlayerStatsScriptableObject Stats;
    public event Action<int> OnHealthChangeEvent; 

	private SpriteRenderer _sprite;
    private GameManager _gameManager;
    private SwitchConctoller _glitchSwitch;

    public int HitPoints;

	public bool IsImmune;

	private void Awake()
	{
		this._sprite = base.GetComponent<SpriteRenderer>();
        this._gameManager = GameManagerGetter.GetManager();
	    this._glitchSwitch = GetComponent<SwitchConctoller>();
	}

	private void Start()
	{
		this.SetPlayerStats();
	}

    void Update()
    {
        if(this.HitPoints <= 0)
        {
            this._gameManager.RestartLevelComponent.Execute();
        }
    }

	private void SetPlayerStats(PlayerStatsScriptableObject newStats = null)
	{
		newStats = (newStats ?? this.Stats);
		this.HitPoints = newStats.HitPoints;
	}

	public bool ModifyHitPoints(int reduceBy, bool forceDammageThroughImmune = false, bool giveImune = true)
	{
	    var previousHitPoints = this.HitPoints;
		if (!this.IsImmune)
		{
			this.HitPoints -= reduceBy;
			//Debug.Log(string.Format("Reduced HP by {0} down to {1}HP", reduceBy, this.HitPoints));

		    if (giveImune)
            {
                this.GiveImmuneTime(this.Stats.ImmuneTime);
            }

		    if (this.OnHealthChangeEvent != null)
		    {
		        this.OnHealthChangeEvent(this.HitPoints);

		    }

            // todo move to event listener
		    var wasGlitchingBefore = _gameManager.GlitchHealth.IsGlitching(previousHitPoints);
		    var isGlitchingNow = _gameManager.GlitchHealth.IsGlitching(this.HitPoints);
		    if (wasGlitchingBefore != isGlitchingNow)
		    {
                Debug.Log("Toggling Glitch");
		        _glitchSwitch.Interact();

		    }

            _gameManager.GlitchHealth.RunOnPlayerHealthChangeEvent(this.HitPoints);
		}

	    this.HitPoints = this.HitPoints > this.Stats.HitPoints ? this.Stats.HitPoints : this.HitPoints;

        return this.HitPoints <= 0;
	}

	public void GiveImmuneTime(float seconds)
	{
		this.IsImmune = true;
		base.StartCoroutine(this.RemoveImmune(seconds));
		base.StartCoroutine(this.BlinkFrame(seconds, 6));
	}

	private IEnumerator RemoveImmune(float seconds)
	{
		//Debug.Log ("Immune Start");
		yield return new WaitForSeconds (seconds);
		this.IsImmune = false;
		//Debug.Log ("Immune End");
	}

	private IEnumerator BlinkFrame(float totalTime, int numberOfBlinks)
	{
		var blinkTime = totalTime / 2;
		blinkTime = blinkTime / numberOfBlinks;
		bool spriteIsVisable = true;

		for (int i = 0; i < (numberOfBlinks * 2); i++) {
			_sprite.color = spriteIsVisable ? new Color (1f, 1f, 1f, 0f) : new Color (1f, 1f, 1f, 1f);
			spriteIsVisable = !spriteIsVisable;
			yield return new WaitForSeconds (blinkTime);
		}
	}
		
	//Sorry Michael, I added this last night- needed an accessor to HP
	public int GetHitpoints(){
		return this.HitPoints;
	}
}
