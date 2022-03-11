using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ElectroTower : MonoBehaviour
{
    [SerializeField] private float chargeDelay;
    [SerializeField] private float startDelay;
    
    public Boolean DeadField = false;

    private float _nextCharge = 0;
    private Animator _animator;
    private float _scaleSlowMotion = 1;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        ScreenTouch.SlowMotionStart.AddListener(scale =>
        {
            _scaleSlowMotion = scale;
            _animator.SetFloat("Scale", scale);
        });
        ScreenTouch.SlowMotionEnd.AddListener(() =>
        {
            _scaleSlowMotion = 1;
            _animator.SetFloat("Scale", 1);
        });
    }

    private void Start()
    {
        _nextCharge = Time.time + startDelay;
    }

    private void Update()
    {
        if (Time.time > _nextCharge)
        {
            StartCoroutine(Charge());
            _nextCharge = Time.time + (chargeDelay * _scaleSlowMotion);
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("ElectroCharge"))
        {
            DeadField = true;
        }
        else
        {
            DeadField = false;
        }
    }

    private IEnumerator Charge()
    {
        _animator.SetTrigger("Charge");
        yield return new WaitForSeconds(0.1f);
        _animator.ResetTrigger("Charge");
    }
}
