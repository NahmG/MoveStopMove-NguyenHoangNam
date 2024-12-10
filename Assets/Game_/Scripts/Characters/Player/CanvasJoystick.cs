using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasJoystick : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    private void Awake()
    {
        canvas.worldCamera = Camera.main;
    }
}
