using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject[] levelList;
    [SerializeField] private CanvasGroup _screen;

    private GameObject _currentLevel;
    private void Awake()
    {
        _currentLevel = new GameObject();
        _screen.alpha = 1;
        
        PlayerTrigger.PlayerDead.AddListener(() =>
        {
            StartCoroutine(RestartLevel());
        });
        PlayerTrigger.PlayerFinish.AddListener(() =>
        {
            StartCoroutine(NextLevel());
        });
        
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);
        }
    }

    private void Start()
    {
        LoadLevel();
    }

    private IEnumerator RestartLevel()
    {
        _screen.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        LoadLevel();
    }
    
    private IEnumerator NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        if (PlayerPrefs.GetInt("Level") >= levelList.Length)
        {
            PlayerPrefs.SetInt("Level", 0);
        }
        _screen.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        LoadLevel();
    }
    
    private void LoadLevel()
    {
        Destroy(_currentLevel);
        _currentLevel = Instantiate(levelList[PlayerPrefs.GetInt("Level")]);
        _screen.DOFade(0, 0.5f);
    }
}
