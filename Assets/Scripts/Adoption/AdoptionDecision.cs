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

    public IEnumerator DisplayMessage(string catName, string npcName, GameObject adoptionMsg)
    {
        TextMeshProUGUI messageText = adoptionMsg.GetComponentInChildren<TextMeshProUGUI>();
        messageText.text = GetRandomMessage(catName, npcName);

        adoptionMsg.SetActive(true);

        yield return new WaitForSecondsRealtime(3);

        adoptionMsg.SetActive(false);
    }

    public void AdoptCat()
    {

        GameObject cat = PlayerController.Instance.catSelected;
        GameObject npc = PlayerController.Instance.companionNPC;
        //NPC leaves the shelter
        npc.GetComponent<NPCBehaviour>().LeaveShelter();

        AdoptionStats.Instance.numCatsAdopted++; //increase count of cats adopted

       
        GameObject adoptionMessageInstance = AdoptionStats.Instance.adoptionMessagePrefab;
     

        adoptionMessageInstance.SetActive(true);
        TextMeshProUGUI message = adoptionMessageInstance.GetComponentInChildren<TextMeshProUGUI>();

        string catName = cat.GetComponent<DisplayCatInformation>().catData.catName; 
        string npcName = npc.GetComponent<NPC>().dialogueData[0].NPCName;
        message.text = GetRandomMessage(catName, npcName);


        Destroy(gameObject); //destroy gameobject
        Destroy(cat); //removes the cat (probs best to replace this with an animation that then triggers destroy e.g. fade cat out)

    }

    public void CancelAdoption()
    {
        Destroy(gameObject);
    }

}
