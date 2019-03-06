using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class HealthUI : MonoBehaviour
{
    private PlayerManager _playerManager;
    private SpriteRenderer _spriteRenderer;

    public Sprite FullHealth;
    public Sprite ThreeQuaterHealth;
    public Sprite HalfHealth;
    public Sprite QuaterHealth;
    public Sprite NoHealth;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerManager = GetComponentInParent<PlayerManager>();
        _playerManager.OnHealthChangeEvent += PlayerManagerOnOnHealthChangeEvent;
        PlayerManagerOnOnHealthChangeEvent(40);
    }

    private void PlayerManagerOnOnHealthChangeEvent(int playerHealth)
    {
        if (playerHealth <= 0)
        {
            _spriteRenderer.sprite = NoHealth;
        }
        else if (playerHealth < 10)
        {
            _spriteRenderer.sprite = QuaterHealth;
        }
        else if (playerHealth <= 20)
        {
            _spriteRenderer.sprite = HalfHealth;
        }
        else if (playerHealth <= 30)
        {
            _spriteRenderer.sprite = ThreeQuaterHealth;
        }
        else
        {
            _spriteRenderer.sprite = FullHealth;
        }
    }
}
