using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private RectTransform fill;
    [SerializeField] private RectTransform background;
    [SerializeField] float smoothness = 0.1f;

    float currentValue = 1f;
    float targetValue = 1f;

    void Start()
    {
        background = GetComponent<RectTransform>();
    }

    private void Update() {
        currentValue = Mathf.Lerp(currentValue, targetValue, 1 - Mathf.Pow(smoothness, Time.deltaTime));
        fill.offsetMax = new Vector2(-background.rect.width * (1 - currentValue), fill.offsetMax.y);
    }

    public void SetValue(float value) {
        targetValue = value;
    }
}
