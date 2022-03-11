using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector3 _cameraPos;
    private void Awake()
    {
        _cameraPos = transform.position - player.transform.position;
    }

    private void Update()
    {
        transform.position = _cameraPos + player.transform.position;
    }
}
