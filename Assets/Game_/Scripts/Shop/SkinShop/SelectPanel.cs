using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    [SerializeField] GameObject equipButton;
    [SerializeField] GameObject unEquipButton;

    public void TurnOnEquipButton()
    {
        equipButton.SetActive(true);
        unEquipButton.SetActive(false);
    }

    public void TurnOnUnEquipButton()
    {
        equipButton.SetActive(false);
        unEquipButton.SetActive(true);
    }
}
