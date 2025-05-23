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
    public NpcsDialogues[] NPCData;

    private NPC npc;
    private SpriteLibrary spriteLibrary;

    private bool isShelterOpen = false;

    public int waitTimeSeconds = 10;

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
        if(!isShelterOpen) //if shelter is closed -> its now open
        {
            isShelterOpen = true;
            CreateNPC();
           // StartCoroutine(SpawnNpc()); //spawns a new npc every 10 seconds
            OpenShelterBtnText.GetComponent<TextMeshProUGUI>().text = "Close Shelter"; 
        } else //shelter was open -> now closed
        {
            isShelterOpen = false;
           // OpenShelterBtnText.GetComponent<TextMeshProUGUI>().text = "Open Shelter";
        }
      
    }
    /*
    IEnumerator SpawnNpc()
    {
        CreateNPC();
        yield return new WaitForSeconds(waitTimeSeconds);
       }
    */
}
