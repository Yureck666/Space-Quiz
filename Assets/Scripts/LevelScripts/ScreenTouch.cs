using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.iOS;

public class ScreenTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public static UnityEvent PointerUp = new UnityEvent();
    public static UnityEvent<Vector3> PointerDrag = new UnityEvent<Vector3>();
    public static UnityEvent<float> SlowMotionStart = new UnityEvent<float>();
    public static UnityEvent SlowMotionEnd = new UnityEvent();

    [SerializeField] private float slowMotionScale;

    private Vector2 _screenPoint;
    private Vector3 _screenPoint3;

    public void OnPointerDown(PointerEventData eventData)
    {
            _screenPoint = eventData.position;
            SlowMotionStart.Invoke(slowMotionScale);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
            _screenPoint3 = _screenPoint - eventData.position;
            _screenPoint3 = new Vector3(_screenPoint3.x, _screenPoint3.z, _screenPoint3.y);
            PointerUp.Invoke();
            SlowMotionEnd.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
            _screenPoint3 = _screenPoint - eventData.position;
            _screenPoint3 = new Vector3(_screenPoint3.x, _screenPoint3.z, _screenPoint3.y);
            PointerDrag.Invoke(_screenPoint3);
    }
}
