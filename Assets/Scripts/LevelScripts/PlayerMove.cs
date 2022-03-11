using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    public static UnityEvent<Boolean> GameStarted = new UnityEvent<Boolean>();

    [SerializeField] private float Speed = 1;
    [SerializeField] private GameObject arrow;
    
    private Boolean _slow = false;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector3 _direction = Vector3.zero;
    private Boolean _gameStarted = false;
    private void Awake()
    {
        arrow.SetActive(false);
        ScreenTouch.PointerUp.AddListener(ChangeDirection);
        ScreenTouch.PointerDrag.AddListener(ChooseDirection);
        ScreenTouch.SlowMotionStart.AddListener(SlowMotion);
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameStarted.Invoke(false);
    }

    private void ChangeDirection()
    {
        if (_direction != Vector3.zero)
        {
            arrow.SetActive(false);
            _animator.SetFloat("Speed", 1);
            _animator.SetBool("Run", true);
            _rigidbody.velocity = _direction * Speed;
            transform.DOLookAt(transform.position + _direction, 0.1f);
            _slow = false;
        }
    }

    private void ChooseDirection(Vector3 direction)
    {
        if ((Math.Abs(direction.x) + Math.Abs(direction.y) + Math.Abs(direction.z)) > 100)
        {
            _direction = direction.normalized;
            arrow.SetActive(true);
            arrow.transform.LookAt(arrow.transform.position + _direction);
            if (!_gameStarted)
            {
                _gameStarted = true;
                GameStarted.Invoke(true);
            }
        }
    }

    private void SlowMotion(float SlowMotionScale)
    {
        if (!_slow)
        {
            _animator.SetFloat("Speed", SlowMotionScale);
            _rigidbody.velocity = _rigidbody.velocity * SlowMotionScale;
            _slow = true;
        }
    }
}
