using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private float fateScale;
    [SerializeField] private GameObject[] settingsButtons;
    
    private CanvasGroup _canvasGroupMenu;
    
    private Boolean _gameStarted;
    private void Awake()
    {
        _canvasGroupMenu = GetComponent<CanvasGroup>();

        PlayerMove.GameStarted.AddListener(gs =>
        {
            StartCoroutine(ShowHideMenu(gs));
        });

        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("4653151", false);
        }
    }

    private IEnumerator ShowHideMenu(Boolean gs)
    {
        if (!gs)
        {
            foreach (var sb in settingsButtons)
            {
                sb.SetActive(true);
            }
            _canvasGroupMenu.DOFade(fateScale, 0.2f);
            if (Advertisement.isInitialized)
            {
                Advertisement.Show("Interstitial_Android");
            }
        }
        else
        {
            _canvasGroupMenu.DOFade(0, 0.2f);
            yield return new WaitForSeconds(0.2f);
            foreach (var sb in settingsButtons)
            {
                sb.SetActive(false);
            }
        }
    }
}
