using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "NewItem", menuName = "ShopItem")]

public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite ItemImage;
    public int Quantity;
    public float Price;

}
