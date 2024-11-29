using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Item", menuName = "GameConfiguration/ItemData")]
public class ItemData : ScriptableObject
{
    public int index;
    public ItemType type;

    public Sprite sprite;
    public string discription;
    public int price;

    public int speed;
    public float range;
    public int gold;
}
