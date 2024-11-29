using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTargetIndicator : MonoBehaviour
{
    [SerializeField] CanvasGroup canv;

    private void OnEnable()
    {
        canv.alpha = 1.0f;
    }

    private void OnDisable()
    {
        canv.alpha = 0;
    }
}
