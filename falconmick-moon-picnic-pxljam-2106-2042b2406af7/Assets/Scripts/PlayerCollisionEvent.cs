using UnityEngine;
using System.Collections;
using Prime31;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(CharacterController2D))]
public class PlayerCollisionEvent : MonoBehaviour
{
    public float FallVelocityToHitEnemy = -10f;

	private PlayerManager _playerManager;
	private CharacterController2D _characterController2D;
	private PlayerController controller;
    private QuitApplication _quitApplication;
    private GameManager _gameManager;

    void Awake()
	{
		_playerManager = GetComponent<PlayerManager>();
		_characterController2D = GetComponent<CharacterController2D> ();
	    _quitApplication = Object.FindObjectOfType<QuitApplication>();
	    _gameManager = GameManagerGetter.GetManager();


        _characterController2D.onTriggerStayEvent += PlayerStayTrigger;
	    _characterController2D.onTriggerEnterEvent += PlayerEnterTrigger;
	}

    private void PlayerEnterTrigger(Collider2D collider)
    {
        if (collider.CompareTag(TagDefinition.EnterBoss))
        {
            Debug.Log("Hit");
            var switchController = collider.GetComponent<SwitchConctoller>();
            switchController.Interact();
        }
        else if (collider.CompareTag(TagDefinition.WinHead))
        {
            Debug.Log("Hit");
            HitEnemy(collider);
            StartCoroutine(WinGame());
            _gameManager.EndOfZeWorldEvent();
        }
    }

    private IEnumerator WinGame()
    {
        yield return new WaitForSeconds(3f);
        _quitApplication.Quit();
    }

    void Start()
	{
		controller = GetComponent<PlayerController> ();
	}

	private void PlayerStayTrigger(Collider2D collider) 
	{
		//Debug.Log ("Trigger Stay");
	    if (collider.CompareTag(TagDefinition.Spike))
	    {
	        // tmp code because triggers are hard ;(
	        if (collider.gameObject.transform.parent.gameObject.CompareTag(TagDefinition.Enemy))
	        {
	            _playerManager.ModifyHitPoints(10);
	            controller.PlayHitSound();
	            //Debug.Log("Hit Enemy");
	        }
	        else
	        {
	            _playerManager.ModifyHitPoints(10);
	            controller.PlayHitSound();
	            //Debug.Log("Hit Spikes");
	        }

	        //_playerManager.ModifyHitPoints (10);
	    }
	    else if(collider.CompareTag(TagDefinition.Head) && _characterController2D.velocity.y <= FallVelocityToHitEnemy)
	    {
	        HitEnemy(collider);
	    }


        if (collider.CompareTag(TagDefinition.Enemy))
        {
            _playerManager.ModifyHitPoints(20);
        }
    }

    private void HitEnemy(Collider2D collider)
    {
        var enemy = collider.gameObject.GetComponent<EnemyHit>();
        controller.PlayerJump(0.5f);
        enemy.HitEnemy();
        //Debug.LogWarning("Hit the head at y velocity: " + _characterController2D.velocity.y);
        _playerManager.ModifyHitPoints(-10, giveImune: false);
    }
}
