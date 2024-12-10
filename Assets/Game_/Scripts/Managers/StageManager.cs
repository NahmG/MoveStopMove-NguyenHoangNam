using IngameDebugConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] Stage[] stages;
    [HideInInspector] public Stage currentStage;

    [SerializeField] Canvas targetIndicator;

    public void Start()
    {
        //OnLoadStage(0);

        UIManager.Ins.OpenUI<CanvasIntro>();
        UIManager.Ins.CloseUI<CanvasIntro>(2f);

        Invoke(nameof(OnInit), 1.8f);
    }

    public void OnInit()
    {
        Debug.Log("Init");
        CharacterManager.Ins.OnInit();
        MainMenu();
    }

    public void DelayInit(float delay)
    {
        UIManager.Ins.OpenUI<CanvasTransition>();
        UIManager.Ins.CloseUI<CanvasTransition>(delay);

        Invoke(nameof(OnInit), delay);
    }

    public void MainMenu()
    {
        UIManager.Ins.OpenUI<CanvasMainMenu>();
        CameraFollow.Ins.MainMenuPos();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        CharacterManager.Ins.Player.skin.GetDefaultSkin();
    }

    public void StartPlay()
    {
        CameraFollow.Ins.GamePlayPos();
        UIManager.Ins.OpenUI<CanvasGameplay>();

        Invoke(nameof(OnPlay), .75f);
    }

    public void OnPlay()
    {
        UIManager.Ins.OpenUI<CanvasGameplay>();
        targetIndicator.gameObject.SetActive(true);
        GameManager.Ins.ChangeState(GameState.GamePlay);
        CharacterManager.Ins.OnPlay();
    }


    public void WeaponShop()
    {
        UIManager.Ins.OpenUI<CanvasWeapon>();
        GameManager.Ins.ChangeState(GameState.Weapon);
    }

    public void SkinShop()
    {
        UIManager.Ins.OpenUI<CanvasSkinShop>();
        CameraFollow.Ins.SkinShopPos();

        GameManager.Ins.ChangeState(GameState.Shop);
    }

    public void Setting()
    {
        UIManager.Ins.OpenUI<CanvaSetting>();
        targetIndicator.gameObject.SetActive(false);

        GameManager.Ins.ChangeState(GameState.Setting);
    }

    public void Continue()
    {
        UIManager.Ins.OpenUI<CanvasGameplay>();
        targetIndicator.gameObject.SetActive(true);

        GameManager.Ins.ChangeState(GameState.GamePlay);
    }

    public void Victory()
    {
        UIManager.Ins.OpenUI<CanvasVictory>();

        GameManager.Ins.ChangeState(GameState.Victory);

        targetIndicator.gameObject.SetActive(false);
    }

    public void Fail()
    {
        UIManager.Ins.CloseAll();
        if (!CharacterManager.Ins.isRevive)
        {
            UIManager.Ins.OpenUI<CanvasRevive>();
        }
        else
        {
            UIManager.Ins.OpenUI<CanvasFail>();
        }

        GameManager.Ins.ChangeState(GameState.Fail);
        targetIndicator.gameObject.SetActive(false);
    }

    public void Revive()
    {
        CharacterManager.Ins.RevivePlayer();
        OnPlay();
    }  

    public void OnReset()
    {
        CharacterManager.Ins.OnDespawn();
    }

    public void OnLoadStage(int stage)
    {
        if (currentStage != null)
        {
            Destroy(currentStage.gameObject);
        }

        currentStage = Instantiate(stages[stage]);
    }

}