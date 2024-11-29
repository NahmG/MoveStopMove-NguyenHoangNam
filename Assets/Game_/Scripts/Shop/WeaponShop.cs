using TMPro;
using UnityEngine;

public class WeaponShop : Singleton<WeaponShop>
{
    public Transform viewPoint;

    public TMP_Text discription;
    public TMP_Text weapName;
    public TMP_Text price;

    public GameObject selectPanel;
    public GameObject lockedPanel;

    public GameObject selectedButton;
    public GameObject equippedButton;


    Weapon currentWeapon;
    int index;

    private void OnEnable()
    {
        index = DataManager.Ins.weaponId;
        ShowWeapon();
    }

    private void ShowWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(DataManager.Ins.weapons[index], viewPoint);
        currentWeapon.name = DataManager.Ins.weapons[index].name;

        weapName.text = currentWeapon.name;

        discription.text = currentWeapon.data.discription;

        if (DataManager.Ins.playerData.weaponsUnlock[index] == false)
        {
            selectPanel.SetActive(false);
            lockedPanel.SetActive(true);

            price.text = currentWeapon.data.price.ToString();
        }
        else
        {
            lockedPanel.SetActive(false);
            selectPanel.SetActive(true);

            if(DataManager.Ins.playerData.weaponId == index)
            {
                selectedButton.SetActive(false);
                equippedButton.SetActive(true);
            }
            else
            {
                selectedButton.SetActive(true);
                equippedButton.SetActive(false);
            }
        }
    }

    public void NextWeapon()
    {
        if (index == DataManager.Ins.weapons.Count - 1) return;

        index++;
        ShowWeapon();
    }

    public void PrevWeapon()
    {
        if (index == 0) return;
        
        index--;
        ShowWeapon();
    }

    public void BuyWeapon()
    {
        if(currentWeapon.data.price <= DataManager.Ins.playerData.coin)
        {
            lockedPanel.SetActive(false);
            selectPanel.SetActive(true);
            EquipWeapon();

            DataManager.Ins.playerData.coin -= currentWeapon.data.price;
            DataManager.Ins.playerData.weaponsUnlock[index] = true;

            DataManager.Ins.SaveData();
        }
    }

    public void EquipWeapon()
    {
        Weapon weapon = DataManager.Ins.weapons[index];
        CharacterManager.Ins.Player.skin.AddWeapon(weapon);

        DataManager.Ins.weaponId = index;
        DataManager.Ins.SaveData();

        selectedButton.SetActive(false);
        equippedButton.SetActive(true);
    }
}
