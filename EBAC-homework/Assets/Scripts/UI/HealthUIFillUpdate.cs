using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthUIFillUpdate : MonoBehaviour
{
    public Image healthUI;


    [Header("Animation")]
    public float duration = .1f;
    public Ease ease = Ease.Linear;

    private Tween _currentTween;

    private void OnValidate()
    {
        if(healthUI == null) healthUI = GetComponent<Image>();
    }

    public void UpdateValue(float f)
    {
        healthUI.fillAmount = f;
    }

    public void UpdateValue(float max, float current)
    {
        if(_currentTween != null) _currentTween.Kill();
        _currentTween = healthUI.DOFillAmount(1 - (current / max), duration).SetEase(ease);
    }
}
