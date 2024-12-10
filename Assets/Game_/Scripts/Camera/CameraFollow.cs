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

    [SerializeField]
    float smoothSpeedGplay;
    [SerializeField]
    float smoothSpeedShop;

    Character target;

    public Transform Tf;

    private void Awake()
    {
        Tf = transform;
        GameManager.Ins._OnStateChanged += OnChangeStateCamera;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Tf.position = Vector3.Lerp(Tf.position, target.Tf.position + offset, smoothSpeed * Time.deltaTime);
            Tf.eulerAngles = Vector3.Lerp(Tf.eulerAngles, rot, smoothSpeed);
        }
    }

    public void OnChangeStateCamera(GameState state)
    {
        switch (state)
        {
            case GameState.GamePlay:
                smoothSpeed = smoothSpeedGplay;
                break;
            default:
                smoothSpeed = smoothSpeedShop;
                break;
        }
    }

    public bool IsInsideCameraView(Bounds bounds)
    {
        Plane[] cameraFrustum = GeometryUtility.CalculateFrustumPlanes(_camera);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
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



