using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImgColorSwap : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private Color trueColor;
    [SerializeField] private Color falseColor;
    private bool isTrue;
    private Image img;
    [SerializeField] private UnityEvent onSetTrue;
    [SerializeField] private UnityEvent onSetFalse;

    void Awake() {
        img= GetComponent<Image>();
    }

    public void setFalse() {
        if(!img)img= GetComponent<Image>();
        onSetFalse.Invoke();
        isTrue = false;
        img.color = falseColor;
    }

    public void setTrue() {
        if(!img)img= GetComponent<Image>();
        onSetTrue.Invoke();
        isTrue = true;
        img.color = trueColor;
    }

    public void OnPointerClick(PointerEventData eventData) {
        SwapColor();
    }

    private void SwapColor() {
        if(isTrue) {
           setFalse();
        }else {
           setTrue();
        }
    }
}
