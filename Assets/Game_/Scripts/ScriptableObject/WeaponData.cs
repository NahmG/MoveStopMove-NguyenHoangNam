using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "WeaponData", menuName = "GameConfiguration/WeaponData")]
public class WeaponData : ScriptableObject
{
    public int index;
    public WeaponType type;

    public string discription;
    public int price;

    public int speed;
    public float range;

    //public bool unlock;
}
