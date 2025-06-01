using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] CatComputerData computerData;
    [SerializeField] GameObject ShopMenu;
    [SerializeField] GameObject Mailrescuecats;
    [SerializeField] GameObject EntireComputerUI;
    [SerializeField] GameObject ItemListPrefab;
    [SerializeField] GameObject ItemCheckoutParent;
    [SerializeField] TextMeshProUGUI PlayerMoneyUI;
    [SerializeField] TextMeshProUGUI CheckoutCostsUI;
    [SerializeField] AudioSource purchase;
    [SerializeField] AudioSource click;

    private Dictionary<Item, GameObject> checkoutItems = new Dictionary<Item, GameObject>(); //check out


    private int HistoryFoodPurchases = 0;
    private int HistoryLitterPurchases = 0;
    private int HistoryPodPurchases = 0;

    public float OverallCost = 0;
  
    //Add button to display shop
    public void ShowShop()
    {
        click.Play();
        ShopMenu.SetActive(true);
        Mailrescuecats.SetActive(false);
        PlayerMoneyUI.text = "$" + PlayerController.Instance.Money.ToString();
        CheckoutCostsUI.text = "$" + OverallCost;

    }

    public void ShowListings()
    {
        Mailrescuecats.SetActive(true);
        ShopMenu.SetActive(false);
        click.Play();
    }

    public void ExitComputer()
    {
        click.Play();
        Mailrescuecats.SetActive(true);
        ShopMenu.SetActive(false);
        EntireComputerUI.SetActive(false);

        OverallCost = 0;

        foreach (var pair in checkoutItems)
        {
            Destroy(pair.Value);
        }

        checkoutItems.Clear();
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
        click.Play();
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
            else if (item.ItemName == "Cat Litter")
            {
                PlayerController.Instance.CatLitter += quantity;
                HistoryLitterPurchases += quantity;
            }
            else if (item.ItemName == "Cat Pod")
            {
                int potentialTotal = HistoryPodPurchases + quantity;

                if (HistoryPodPurchases >= 3 || potentialTotal > 3)
                {
                    Debug.Log("Cannot buy more than 3 Cat Pods");
                  //  continue;
                }
                Debug.Log("OverallCost: " + OverallCost);

                if (OverallCost <= PlayerController.Instance.Money) //if player does have enough money then create a new pod and that
                {
                    PlayerController.Instance.CatPod += quantity;
                    HistoryPodPurchases += quantity;
                    computerData.numberOfPods += quantity;
                    computerData.UpdatePodStatusArray(computerData.numberOfPods);
                    computerData.spawnPositions = computerData.GetSpawnPoints(computerData.catPodsPositions, computerData.numberOfPods);
                }
                else
                {
                    Debug.Log("Not enough money for Cat Pods");
                 //   continue;
                }
            }

          
            //bug errors with buying over 3 pods.....
            if (OverallCost <= PlayerController.Instance.Money)
            {
                PlayerController.Instance.Money -= OverallCost;
                Debug.Log("OverallCost: " + OverallCost);
                PlayerMoneyUI.text = "$" + PlayerController.Instance.Money.ToString();
         
                purchase.Play();

            }
            else
            {
                Debug.Log("Dont have enough money!");
 
            }
            OverallCost = 0;
            CheckoutCostsUI.text = "$" + OverallCost;
        }

        foreach (var i in checkoutItems)
        {
            Destroy(i.Value); //clear
        }
        checkoutItems.Clear();
    }

    public void DeleteItem(Item itemData)
    {
        click.Play();
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