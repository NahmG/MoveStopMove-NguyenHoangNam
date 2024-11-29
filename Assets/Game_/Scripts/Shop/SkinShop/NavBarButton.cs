using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NavBarButton : MonoBehaviour
{
    [SerializeField] Button button;

    [SerializeField] GameObject activeBG;
    [SerializeField] GameObject InActiveBG;

    public void OnInit(UnityAction onClickEvent)
    {
        button.onClick.AddListener(onClickEvent);
    }

    public void OnSelect()
    {
        activeBG.SetActive(true);
        InActiveBG.SetActive(false);
    }

    public void DeSelect()
    {
        activeBG.SetActive(false);
        InActiveBG.SetActive(true);
    }
}
