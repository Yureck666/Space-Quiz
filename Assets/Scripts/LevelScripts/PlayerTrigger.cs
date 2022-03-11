using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{
    public static UnityEvent PlayerDead = new UnityEvent();
    public static UnityEvent PlayerFinish = new UnityEvent();
    
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private Color finishcolor;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Killer>(out _))
        {
            StartCoroutine(DestroyPlayer(true));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Killer>(out _))
        {
            StartCoroutine(DestroyPlayer(true));
        }

        if (other.gameObject.TryGetComponent<Finish>(out _))
        {
            StartCoroutine(Teleport());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        ElectroTower field;
        if (other.gameObject.TryGetComponent<ElectroTower>(out field))
        {
            if (field.DeadField)
            {
                StartCoroutine(DestroyPlayer(true));
            }
        }
    }

    private IEnumerator DestroyPlayer(Boolean death)
    {
        GetComponent<CapsuleCollider>().enabled = false;
        _rigidbody.velocity = Vector3.zero;
        particleSystem.Play();
        yield return new WaitForSeconds(0.2f);
        mesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.8f);
        if (death)
        {
            PlayerDead.Invoke();
        }
        else
        {
            PlayerFinish.Invoke();
        }
    }

    private IEnumerator Teleport()
    {
        particleSystem.gravityModifier = -1;
        particleSystem.startColor = finishcolor;
        _rigidbody.velocity = _rigidbody.velocity / 5;
        mesh.material.DOColor(finishcolor, "_EmissionColor", 1);
        yield return new WaitForSeconds(1);
        StartCoroutine(DestroyPlayer(false));
    }

}
