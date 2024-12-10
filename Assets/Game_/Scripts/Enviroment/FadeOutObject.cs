using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutObject : MonoBehaviour
{
    Bounds bounds;
    
    [SerializeField] Collider _collider;
    [SerializeField] Renderer _renderer;

    [SerializeField] Material defaultMaterial;
    [SerializeField] Material fadeMaterial;

    private void Start()
    {
        bounds = _collider.bounds;
    }

    private void Update()
    {
        if (CameraFollow.Ins.IsInsideCameraView(bounds))
        {
            _renderer.material = fadeMaterial;
        }
        else
        {
            _renderer.material = defaultMaterial;   
        }
    }
}
