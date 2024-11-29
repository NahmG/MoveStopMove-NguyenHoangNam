using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    [Header("Private props")]
    [SerializeField] Animator anim;

    [SerializeField] float delayTime;

    bool muted;
    bool vibrating;

    [SerializeField] GameObject imageMutedOn;
    [SerializeField] GameObject imageMutedOff;

    [SerializeField] GameObject imageVibrateOn;
    [SerializeField] GameObject imageVibrateOff;

    public override void Open()
    {
        base.Open();
        UIManager.Ins.OpenUI<CanvasCoin>();
    }

    public void Play()
    {
        anim.ResetTrigger("MenuOff");
        anim.SetTrigger("MenuOff");

        UIManager.Ins.GetUI<CanvasCoin>().OnClose();
        UIManager.Ins.CloseUI<CanvasCoin>(delayTime);
        UIManager.Ins.CloseUI<CanvasMainMenu>(delayTime);
        StageManager.Ins.StartPlay();
    }

    public void WeaponShop()
    {
        anim.ResetTrigger("MenuOff");
        anim.SetTrigger("MenuOff");

        UIManager.Ins.CloseUI<CanvasMainMenu>(delayTime);
        StageManager.Ins.WeaponShop();
    }

    public void SkinShop()
    {
        anim.ResetTrigger("MenuOff");
        anim.SetTrigger("MenuOff");

        UIManager.Ins.CloseUI<CanvasMainMenu>(delayTime);
        StageManager.Ins.SkinShop();
    }

    public void OnNameTextChange(string s)
    {
        DataManager.Ins.playerData.name = s;    
        DataManager.Ins.SaveData();
    }

    public void Mute()
    {
        if (muted)
        {
            muted = false;
        }
        else
        {
            muted = true;
        }

        imageMutedOn.SetActive(muted);
        imageMutedOff.SetActive(!muted);
    }

    public void Vibrate()
    {
        if(vibrating)
        {
            vibrating = false;
        }
        else
        {
            vibrating = true;
        }

        imageVibrateOn.SetActive(vibrating);
        imageVibrateOff.SetActive(!vibrating);
    }
}
