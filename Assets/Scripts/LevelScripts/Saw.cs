using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Saw : MonoBehaviour
{
    [SerializeField] private float offsetTime;
    [SerializeField] private TrailRenderer _trailRenderer;

    private float _offsetTime;
    private Animator _animator;
    private float _trailTime;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        _trailTime = _trailRenderer.time;
        ScreenTouch.SlowMotionStart.AddListener(scale =>
        {
            _animator.SetFloat("Scale", scale);
            _trailRenderer.time = _trailTime / scale;
        });
        ScreenTouch.SlowMotionEnd.AddListener(() =>
        {
            _animator.SetFloat("Scale", 1);
            _trailRenderer.time = _trailTime;
        });
    }

    private void Start()
    {
        _offsetTime = Time.time + offsetTime;
    }

    private void Update()
    {
        if (Time.time > _offsetTime)
        {
            _animator.enabled = true;
        }
    }
}
