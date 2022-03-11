using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TowerCannon : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float maxDistanse;
    
    private LineRenderer _trajectory;
    private Boolean _seePlayer = false;

    private void Awake()
    {
        _trajectory = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 thisPos = transform.position + new Vector3(0, 1.2f, 0);
        RaycastHit hit;
        if (Physics.Raycast(thisPos, playerPos - thisPos, out hit, maxDistanse, ~6, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.gameObject == player)
            {
                Rotate();
            }
            else
            {
                _seePlayer = false;
            }
        }
        else
        {
            _seePlayer = false;
        }
        ShowHideTrajectory();
    }

    private void Rotate()
    {
        if (!_seePlayer)
        {
            StartCoroutine(SlowRotate());
        }
        else
        {
            transform.LookAt(player.transform.position);
        }
    }
    
    private IEnumerator SlowRotate()
    {
        transform.DOLookAt(player.transform.position, 0.2f);
        yield return new WaitForSeconds(0.2f);
        _seePlayer = true;
    }

    private void ShowHideTrajectory()
    {
        if (_seePlayer)
        {
            _trajectory.positionCount = 2;
            _trajectory.SetPosition(0, transform.position + new Vector3(0, 1.2f, 0));
            _trajectory.SetPosition(1, player.transform.position + new Vector3(0, 0.8f, 0));
        }
        else
        {
            _trajectory.positionCount = 0;
        }
    }
}