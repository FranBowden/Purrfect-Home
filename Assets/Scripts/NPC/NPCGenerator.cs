using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class NPCGenerator : MonoBehaviour
{
    [SerializeField] private GameObject NPCPrefab;
    [SerializeField] private GameObject NPCParent;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject OpenShelterBtnText;
    [SerializeField] private GameObject TimeOfDayUI;
    [SerializeField] private GameObject WarningMessage;

    [SerializeField] private TimeManager TimeManager;
    public NpcsDialogues[] NPCData;

    private NPC npc;
    private SpriteLibrary spriteLibrary;

    public bool letVisitorsInside = false;
    private bool isWarningShowing = false;

    private void CreateNPC()
    {
        int index = Random.Range(0, NPCData.Length); //get a random number
        GameObject newNPC = Instantiate(NPCPrefab, spawnPoint.position, Quaternion.identity); //create a new prefab of an npc
        newNPC.transform.SetParent(NPCParent.transform);

        spriteLibrary = newNPC.GetComponent<SpriteLibrary>();
        npc = newNPC.GetComponent<NPC>();


        for (int i = 0; i < NPCData[index].dialogues.Length; i++)
        {
            npc.dialogueData[i] = NPCData[index].dialogues[i]; //assign that npc with dialogue data
            spriteLibrary.spriteLibraryAsset = NPCData[index].dialogues[i].NPCSpriteLibraryAsset; //and assign its animation
        }
    }

    private void Update() //test purposes
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CreateNPC();
        }
    }

    [System.Serializable]
    public class NpcsDialogues
    {
        public NPCDialogue[] dialogues;
    }

    public void ShelterOpen()
    {
        if(!letVisitorsInside) 
        {
            TimeOfDayUI.GetComponent<TextMeshProUGUI>().text = "Afternoon";
            letVisitorsInside = true;
            CreateNPC();
            OpenShelterBtnText.GetComponent<TextMeshProUGUI>().text = "End Day"; 
        } else 
        {
            if(AdoptionStats.Instance.numCatsPlacedInShelter == 0)
            {
                ShowWarning();
            } else
            {
                letVisitorsInside = false;
                TimeManager.EndDay();

            }
        }
      
    }

    public void ShowWarning()
    {
        if (!isWarningShowing)
        {
            StartCoroutine(DisplayWarningMessage());
        }
    }

    IEnumerator DisplayWarningMessage()
    {
        isWarningShowing = true;
        WarningMessage.SetActive(true);
        yield return new WaitForSeconds(3);
        WarningMessage.SetActive(false);
        isWarningShowing = false;
    }
}
