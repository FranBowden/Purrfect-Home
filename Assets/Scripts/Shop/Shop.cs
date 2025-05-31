using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject ShopMenu;
    [SerializeField] GameObject Mailrescuecats;
    [SerializeField] GameObject ItemListPrefab;
    [SerializeField] GameObject ItemCheckoutParent;
    [SerializeField] TextMeshProUGUI PlayerMoneyUI;
    [SerializeField] TextMeshProUGUI CheckoutCostsUI;
    private Dictionary<Item, GameObject> checkoutItems = new Dictionary<Item, GameObject>(); //check out

    private int HistoryFoodPurchases = 0;
    private int HistoryLitterPurchases = 0;
    private int HistoryPodPurchases = 0;

    private float OverallCost = 0;

    //Add button to display shop
    public void ShowShop()
    {
        ShopMenu.SetActive(true);
        Mailrescuecats.SetActive(false);
        PlayerMoneyUI.text = "$" + PlayerController.Instance.Money.ToString();
        CheckoutCostsUI.text = "$" + OverallCost;

    }

    public void ShowListings()
    {
        Mailrescuecats.SetActive(true);
        ShopMenu.SetActive(false);
    }
    //Create a function for "ADD" that will then add a prefab to itemsCheckout
    public void AddItem(GameObject button)
    {
        Item itemData = button.GetComponent<ItemData>().itemData;

        if (checkoutItems.ContainsKey(itemData))
        {
            //item already exists and need to increased quantity
            itemData.Quantity += 1;

            GameObject existingItem = checkoutItems[itemData];
            TextMeshProUGUI amount = existingItem.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            amount.text = "X" + itemData.Quantity.ToString();
            OverallCost += itemData.Price;
        }
        else
        { //item not in checkout yet create it
            GameObject item = Instantiate(ItemListPrefab, ItemCheckoutParent.transform);

            Image image = item.transform.Find("Image").GetComponent<Image>();
            TextMeshProUGUI name = item.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI amount = item.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            OverallCost += itemData.Price;


            itemData.Quantity = 1;
            image.sprite = itemData.ItemImage;
            name.text = itemData.ItemName;
            amount.text = "X" + itemData.Quantity.ToString();
            Button deleteButton = item.transform.Find("Delete").GetComponent<Button>();
            deleteButton.onClick.AddListener(() => DeleteItem(itemData));

            checkoutItems.Add(itemData, item);
        }
        CheckoutCostsUI.text = "$" + OverallCost;
    }

    private void BuyItems()
    {
        foreach (var i in checkoutItems)
        {
            Item item = i.Key;//data
            GameObject ItemGameobject = i.Value; //ui


            int quantity = item.Quantity;

            Debug.Log($"Buying {quantity} of {item.ItemName}");


            if (item.ItemName == "Cat Food")
            {
                PlayerController.Instance.CatFood += quantity;
                HistoryFoodPurchases += quantity;
            }
            else if (item.ItemName == "Cat Pod")
            {
                int potentialTotal = HistoryPodPurchases + quantity;

                if (HistoryPodPurchases >= 3 || potentialTotal > 3)
                {
                    Debug.Log("Cannot buy more than 3 Cat Pods.");
                    return;
                }
                else
                {
                    PlayerController.Instance.CatPod += quantity;
                    HistoryPodPurchases += quantity;
                }
            }
            else if (item.ItemName == "Cat Litter")
            {
                PlayerController.Instance.CatLitter += quantity;
                HistoryLitterPurchases += quantity;
            }
            //bug errors with buying over 3 pods.....
            if (OverallCost <= PlayerController.Instance.Money)
            {
                PlayerController.Instance.Money -= OverallCost;
                OverallCost = 0;
                PlayerMoneyUI.text = "$" + PlayerController.Instance.Money.ToString();
         
                CheckoutCostsUI.text = "$" + OverallCost;
               


            }
            else
            {
                Debug.Log("Dont have enough money!");
            }
        }

        foreach (var i in checkoutItems)
        {
            Destroy(i.Value); //clear
        }
        checkoutItems.Clear();
    }

    public void DeleteItem(Item itemData)
    {
        if (checkoutItems.TryGetValue(itemData, out GameObject itemGO))
        {
            itemData.Quantity -= 1;
            OverallCost -= itemData.Price;
            CheckoutCostsUI.text = "$" + OverallCost;

            if (itemData.Quantity > 0)
            {
                TextMeshProUGUI amountText = itemGO.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
                amountText.text = "X" + itemData.Quantity.ToString();
            }
            else
            {
                checkoutItems.Remove(itemData);
                Destroy(itemGO);
            }
        }
    }

}