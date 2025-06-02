using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool _isCollected = false;
    [SerializeField]
    public bool IsCollected
    {
        get { return _isCollected; }
        set { _isCollected = value; }
    }

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator component not found on Coin object! Please add an Animator to the Coin Prefab.", this);
        }
    }

    public void PlayBounceAnimation()
    {
        if (_animator != null)
        {
            // Reset trigger, щоб він точно спрацював знову, якщо він був якимось чином заблокований
            _animator.ResetTrigger("PlayBounce"); // Додайте цей рядок
            _animator.SetTrigger("PlayBounce");
            Debug.Log($"Playing bounce animation for {gameObject.name}"); // Для налагодження
        }
    }

    public void OnBounceAnimationComplete()
    {
        Debug.Log($"Animation complete, destroying {gameObject.name}"); // Для налагодження
        Destroy(gameObject);
    }
}