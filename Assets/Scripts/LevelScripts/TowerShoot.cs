using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootDelay;
    [SerializeField] private float maxDistanse;

    private float _nextShoot;
    private RaycastHit _hit;
    private Boolean _slowMotion = false;
    private float _slowMotionScale = 1;
    
    private void Awake()
    {
        ScreenTouch.SlowMotionStart.AddListener(scale =>
        {
            if (Time.time < _nextShoot)
            {
                _nextShoot = Time.time + ((_nextShoot - Time.time) / scale);
            }
            _slowMotionScale = scale;
        });
        ScreenTouch.SlowMotionEnd.AddListener(() =>
        { 
            if (Time.time < _nextShoot)
            {
                _nextShoot = Time.time + ((_nextShoot - Time.time) * _slowMotionScale);
            }
            _slowMotionScale = 1;
        });
    }

    private void Start()
    {
        _nextShoot = Time.time;
    }

    private void Update()
    {
        if (Time.time > _nextShoot)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 thisPos = transform.position + new Vector3(0, 1.2f, 0);
            if (Physics.Raycast(thisPos, playerPos - thisPos, out _hit, maxDistanse, ~6, QueryTriggerInteraction.Ignore))
            {
                if (_hit.collider.gameObject == player)
                {
                    _nextShoot = Time.time + (shootDelay / _slowMotionScale);
                    Shoot();
                }
            }
        }
    }

    private void Shoot()
    {
        GameObject curBullet = Instantiate(bullet);
        curBullet.GetComponent<Bullet>().maxDistanse = maxDistanse;
        curBullet.GetComponent<Bullet>().scaleSlowMotion = _slowMotionScale;
        curBullet.transform.position = transform.position + new Vector3(0, 1.2f, 0);
        Vector3 direction = player.transform.position - transform.position;
        direction = direction.normalized * bulletSpeed;
        curBullet.GetComponent<Bullet>().bulletSpeed = new Vector3(direction.x, 0, direction.z);
        curBullet.GetComponent<Rigidbody>().velocity = new Vector3(direction.x, 0, direction.z) * _slowMotionScale;
    }
}
