using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TrailRenderer))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _particleSystemGameObject;
    
    public float maxDistanse;
    public Vector3 bulletSpeed;
    public float scaleSlowMotion;
    
    private Vector3 _startPos;
    private float _trailTime;
    
    private Rigidbody _rigidbody;
    private TrailRenderer _trailRenderer;
    private ParticleSystem _particleSystem;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _particleSystem = _particleSystemGameObject.GetComponent<ParticleSystem>();
        _trailTime = _trailRenderer.time;
        ScreenTouch.SlowMotionStart.AddListener(scale =>
        {
            scaleSlowMotion = scale;
            _rigidbody.velocity = bulletSpeed * scaleSlowMotion;
            _trailRenderer.time = _trailTime / scaleSlowMotion;
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[_particleSystem.particleCount];
            _particleSystem.GetParticles(particles);
            for (int i = 0; i < particles.Length; i++)
            {
                float _rlt = particles[i].remainingLifetime;
                particles[i].remainingLifetime = _rlt / scaleSlowMotion;
            }
            _particleSystem.SetParticles(particles);
            var _particleSystemMain = _particleSystem.main;
            _particleSystemMain.startLifetime = _particleSystemMain.startLifetime.constant / scaleSlowMotion;
            var _partickeSystemEmission = _particleSystem.emission;
            _partickeSystemEmission.rateOverTime = (_partickeSystemEmission.rateOverTime.constant * scaleSlowMotion);
        });
        ScreenTouch.SlowMotionEnd.AddListener(() =>
        {
            _rigidbody.velocity = bulletSpeed;
            _trailRenderer.DOTime(_trailTime, 0.5f);
            var _particleSystemMain = _particleSystem.main;
            _particleSystemMain.startLifetime = _particleSystemMain.startLifetime.constant * scaleSlowMotion;
            var _partickeSystemEmission = _particleSystem.emission;
            _partickeSystemEmission.rateOverTime = (_partickeSystemEmission.rateOverTime.constant / scaleSlowMotion);
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[_particleSystem.particleCount];
            _particleSystem.GetParticles(particles);
            for (int i = 0; i < particles.Length; i++)
            {
                float _rlt = particles[i].remainingLifetime;
                particles[i].remainingLifetime = _rlt * scaleSlowMotion;
            }
            _particleSystem.SetParticles(particles);
            scaleSlowMotion = 1;
        });
    }

    private void Start()
    {
        _startPos = transform.position;
        var _particleSystemMain = _particleSystem.main;
        _particleSystemMain.startLifetime = _particleSystemMain.startLifetime.constant / scaleSlowMotion;
        var _partickeSystemEmission = _particleSystem.emission;
        _partickeSystemEmission.rateOverTime = (_partickeSystemEmission.rateOverTime.constant * scaleSlowMotion);
    }

    private void Update()
    {
        Vector3 distanse = _startPos - transform.position;
        if ((Math.Abs(distanse.x) + Math.Abs(distanse.y) + Math.Abs(distanse.z)) > maxDistanse)
        {
            StartCoroutine(Die());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _));
        {
            CollisionWithPlayer();
        }
        if (collision.gameObject.TryGetComponent<Wall>(out _));
        {
            CollisionWithWall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void CollisionWithPlayer()
    {
        StartCoroutine(Die());
    }

    private void CollisionWithWall()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        _rigidbody.velocity = Vector3.zero;
        transform.DOScale(Vector3.zero, 0.5f/scaleSlowMotion);
        yield return new WaitForSeconds(0.5f/scaleSlowMotion);
        Destroy(gameObject);
    }
}
