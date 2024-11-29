using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavBar : MonoBehaviour
{
    [SerializeField] ShopPanel[] panels;
    [SerializeField] NavBarButton[] buttons;

    NavBarButton currentButton;

    private void Awake()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].OnInit();
        }
    }

    private void OnEnable()
    {
        OpenPanel(panels[0]);
    }

    public void SelectButton(NavBarButton btn)
    {
        if (btn != currentButton)
        {
            if (currentButton != null)
            {
                currentButton.DeSelect();
            }
            currentButton = btn;
            currentButton.OnSelect();
        }
    }

    public void OpenPanel(ShopPanel panel)
    {
        CharacterManager.Ins.Player.skin.GetDefaultSkin();
        panel.gameObject.SetActive(true);
        panel.OnOpen();

        foreach (ShopPanel go in panels)
        {
            if (go != panel)
            {
                go.gameObject.SetActive(false);
            }
        }

        SelectButton(buttons[0]);
    }
}
