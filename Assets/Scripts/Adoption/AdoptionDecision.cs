using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdoptionDecision : MonoBehaviour
{
   
    private List<string> messages = new List<string>
    {
        "{catName} has found a loving home with {npcName}!",
        "{npcName} couldn’t be happier to welcome {catName} into their family!",
        "We’re happy to share that {catName} has been adopted by {npcName}!",
        "{catName} is starting a new adventure with {npcName} — pawsome news!",
        "{npcName} and {catName} are a perfect match — adoption success!",
        "{catName} has been adopted! Their new owner, {npcName}, is thrilled.",
        "{npcName} fell in love with {catName} — they’re heading home together!",
        "{catName} is off to their forever home with {npcName}.",
        "{npcName} couldn’t resist {catName} — a new friendship begins!",
        "{catName} has a new family! {npcName} is ready to give them all the love they deserve."
    };

    string GetRandomMessage(string catName, string npcName)
    {
        int index = Random.Range(0, messages.Count);
        string template = messages[index];
        return template.Replace("{catName}", catName).Replace("{npcName}", npcName);
    }

    private void DisplayMessage(string catName, string npcName)
    {
        GameObject adoptionMsg = AdoptionStats.Instance.adoptionMessagePanel;
        TextMeshProUGUI messageText = adoptionMsg.GetComponentInChildren<TextMeshProUGUI>();
        messageText.text = GetRandomMessage(catName, npcName);

        adoptionMsg.SetActive(true);
    }

    private void FailedAdoptionMessage ()
    {
        GameObject adoptionMsg = AdoptionStats.Instance.adoptionMessagePanel;
        TextMeshProUGUI messageText = adoptionMsg.GetComponentInChildren<TextMeshProUGUI>();
        messageText.text = "The adoption was unsuccessful...";

        adoptionMsg.SetActive(true);

    }
    public void AdoptCat()
    {
        GameObject npc = PlayerController.Instance.companionNPC;
        if (TakeChance())
        {
            GameObject cat = PlayerController.Instance.catSelected;

            CatData catData = cat.GetComponent<DisplayCatInformation>().catData;

            Debug.Log("Cat pod assigned: " + catData.catPodAssigned);

            CatComputerData.Instance.MarkPodAsFree(catData.catPodAssigned);

            npc.GetComponent<NPCBehaviour>().LeaveShelter();

            string catName = catData.catName;
            string npcName = npc.GetComponent<NPC>().dialogueData[0].NPCName;

            DisplayMessage(catName, npcName);
            AdoptionShelterReputation.Instance.SetCurrentPoints(catData.value);
            AdoptionStats.Instance.numCatsAdopted++;
            AdoptionStats.Instance.CatsAdoptedToday++;

            gameObject.SetActive(false);
            Destroy(cat);
        }
        else
        {
            Debug.Log("Adoption was unsucessful....");
            FailedAdoptionMessage();
            gameObject.SetActive(false);
            npc.GetComponent<NPCBehaviour>().LeaveShelter();

        }
    }

    bool TakeChance()
    {
        float chance = PlayerController.Instance.catSelected.GetComponent<AdoptManager>().adoptionChance;
        return Random.Range(0f, 100f) <= chance;
    }

    public void CancelAdoption()
    {
        Destroy(gameObject);
    }

}
