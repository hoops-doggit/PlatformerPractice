using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public GameObject player;
	public GameObject cameraObject;
	public GameObject BossMusicObject;
	public GameObject EndGameMusicObject;

	private PlayerManager playerManager;
	private SetLevels setLevels;
	private Camera camera;
    private GameManager _gameManager;
	private AudioSource bossSource;
	private AudioSource endSFX;

	private bool bossBattleHasStarted = false;

    private bool levelsChanging = true;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	
		playerManager = player.GetComponent<PlayerManager> ();
		setLevels = GetComponent<SetLevels> ();
		camera = cameraObject.GetComponent<Camera> ();
        bossSource = BossMusicObject.GetComponent<AudioSource>();
        endSFX = EndGameMusicObject.GetComponent<AudioSource>();
        this._gameManager = GameManagerGetter.GetManager();
        playerManager.OnHealthChangeEvent += PlayerManagerOnOnHealthChangeEvent;
		_gameManager.OnBossBattleEnter += PlayerEnterBossBattle;
	    _gameManager.OnEndOfZeWorldEvent += EndOfGameAudio;
	}

    private void PlayerManagerOnOnHealthChangeEvent(int health)
    {
		if (this._gameManager.GlitchHealth.IsGlitching(health) && !levelsChanging && !bossBattleHasStarted)
        {
            levelsChanging = true;
            setLevels.CreateFade("Melody", -80.0f, 5.0f);
            setLevels.CreateFade("Tape", -2.0f, 0.6f);
            setLevels.CreateFade("Glitch", -10.0f, 0.6f);
            camera.clearFlags = CameraClearFlags.Nothing;

        }
		if (!this._gameManager.GlitchHealth.IsGlitching(health) && levelsChanging && !bossBattleHasStarted)
        {
            levelsChanging = false;
            setLevels.CreateFade("Melody", 0.0f, 0.6f);
            setLevels.CreateFade("Tape", -80.0f, 5.0f);
            setLevels.CreateFade("Glitch", -80.0f, 5.0f);
            camera.clearFlags = CameraClearFlags.SolidColor;
        }
    }
	private void PlayerEnterBossBattle()
	{
		bossBattleHasStarted = true;
		Debug.Log ("Playing boss battle sound");
		setLevels.CreateFade("Melody", -80.0f, 5.0f);
		setLevels.CreateFade("Tape", -80.0f, 5.0f);
		setLevels.CreateFade("Glitch", -80.0f, 5.0f);
		bossSource.PlayDelayed (1.0f);
	}
	private void EndOfGameAudio()
	{
		setLevels.CreateFade("Melody", 0.0f, 1.0f);
		setLevels.CreateFade("Tape", -80.0f, 5.0f);
		setLevels.CreateFade("Glitch", -80.0f, 5.0f);
		bossSource.Stop ();
		endSFX.Play ();
	}
}
