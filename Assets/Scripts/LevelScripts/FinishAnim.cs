using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FinishAnim : MonoBehaviour
{
    private Animator _animator;
    void Awake()
    {
        _animator = GetComponent<Animator>();
        ScreenTouch.SlowMotionStart.AddListener(scale =>
        {
            _animator.SetFloat("Scale", scale);
        });
        ScreenTouch.SlowMotionEnd.AddListener(() =>
        {
            _animator.SetFloat("Scale", 1);
        });
    }

}
