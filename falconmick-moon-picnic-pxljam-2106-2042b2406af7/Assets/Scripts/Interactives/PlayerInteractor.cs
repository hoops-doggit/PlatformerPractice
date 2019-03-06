using System;
using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerInteractor : MonoBehaviour
{
    private CharacterController2D _characterController2D;
    private Interactable[] _nearbyInteractables = new Interactable[5];
	private PlayerController controller;

    // Use this for initialization
    void Start ()
    {
        _characterController2D = GetComponent<CharacterController2D>();
		controller = GetComponent<PlayerController> ();

        _characterController2D.onTriggerEnterEvent += (col) => CallIfIsInteractable(col, AddInteractable);
        _characterController2D.onTriggerExitEvent += (col) => CallIfIsInteractable(col, RemoveInteractable);
    }

    private void CallIfIsInteractable(Collider2D collider2D, Action<Collider2D> action)
    {
        if (collider2D.CompareTag(TagDefinition.Interactable))
        {
            action(collider2D);
        }
    }

    private void AddInteractable(Collider2D collider2D)
    {
        for (int i = 0; i < _nearbyInteractables.Length; i++)
        {
            if (_nearbyInteractables[i] == null)
            {
                _nearbyInteractables[i] = collider2D.gameObject.GetComponent<Interactable>();
                break;
            }
        }
    }

    private void RemoveInteractable(Collider2D collider2D)
    {
        var interactable = collider2D.gameObject.GetComponent<Interactable>();
        var interavtableRemoved = false;

        for (int i = 0; i < _nearbyInteractables.Length; i++)
        {
            if (_nearbyInteractables[i].UniqueName == interactable.UniqueName)
            {
                _nearbyInteractables[i] = null;
                interavtableRemoved = true;
                break;
            }
        }

        if (!interavtableRemoved)
        {
            Debug.LogError("We couldn't destroy the collider for: " + collider2D.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetButtonUp(InputDefinition.Interact))
        {
            for (int i = 0; i < _nearbyInteractables.Length; i++)
            {
                if (_nearbyInteractables[i] != null)
                {
                    _nearbyInteractables[i].Interact(this.gameObject);
					controller.PlayButtonSound ();
                }
            }
        }
    }
}
