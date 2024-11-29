using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTransition : UICanvas
{
    [SerializeField] Transform weaponImg;

    private void Update()
    {
        weaponImg.Rotate(0, 0, -360 * Time.deltaTime);       
    }
}
