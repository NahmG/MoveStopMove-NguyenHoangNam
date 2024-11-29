using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField] Camera _camera;

    [Header("MainMenu pos")]
    public Vector3 mainMenuOffset;
    public Vector3 mainMenuRot;

    [Header("Gameplay pos")]
    public Vector3 gameplayOffset;
    public Vector3 gameplayRot;

    [Header("Shop pos")]
    public Vector3 shopOffset;
    public Vector3 shopRot;

    Vector3 offset;
    Vector3 rot;
    float smoothSpeed;


    Character target;

    private void Update()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            smoothSpeed = .5f;
        }
        else
        {
            smoothSpeed = .1f;
        }

        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, smoothSpeed);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, rot, smoothSpeed);
        }
    }

    public bool IsInsideCameraView(Bounds bounds)
    {
        Plane[] cameraFrustum = GeometryUtility.CalculateFrustumPlanes(_camera);
        if(GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            return true;
        }
        else return false;  
    }

    public void MainMenuPos()
    {
        offset = mainMenuOffset;
        rot = mainMenuRot;
    }

    public void GamePlayPos()
    {
        offset = gameplayOffset;
        rot = gameplayRot;
    }

    public void SkinShopPos()
    {
        offset = shopOffset;
        rot = shopRot;
    }

    public void SetTarget(Character target)
    {
        this.target = target;
    }

    public void ChangeOffsetIngame(float scale)
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            offset = gameplayOffset * scale;
        }
    }
}



