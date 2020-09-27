using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Walking : MonoBehaviour
{
    [SerializeField] private float _speed;

    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool isWalking = false;

        if (Input.GetKey(KeyCode.A))
        {
            _renderer.flipX = false;
            _rigidbody.velocity = new Vector2(-_speed, 0f);
            isWalking = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _renderer.flipX = true;
            _rigidbody.velocity = new Vector2(_speed, 0f);
            isWalking = true;
        }

        if (!isWalking)
            _rigidbody.velocity = Vector2.zero;

        _animator.SetBool("isWalking", isWalking);
    }
}
