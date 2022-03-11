using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    [SerializeField] private AudioSource idleMusic;
    [SerializeField] private AudioSource gameMusic;
    
    private Boolean _gameStart = false;
    private Boolean _musicIsPlaying = false;
    private void Awake()
    {
        ScreenTouch.PointerUp.AddListener(() =>
        {
            if (!_musicIsPlaying)
            {
                _gameStart = true;
                _musicIsPlaying = true;
            }
        });
    }

    private void Update()
    {
        if (_gameStart)
        {
            idleMusic.loop = false;
            if (!idleMusic.isPlaying)
            {
                gameMusic.Play();
                _gameStart = false;
            }
        }
    }
}
