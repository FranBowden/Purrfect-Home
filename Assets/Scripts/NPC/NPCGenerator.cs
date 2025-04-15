using UnityEngine;
using UnityEngine.U2D.Animation;

public class NPCGenerator : MonoBehaviour
{
    [SerializeField] private GameObject NPCPrefab;
    [SerializeField] private GameObject NPCParent;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private NPCDialogue[] NPCData;

    private NPC npc;
    private SpriteLibrary spriteLibrary;

  private void CreateNPC()
    {
        int index = Random.Range(0, NPCData.Length); //get a random number
        GameObject newNPC = Instantiate(NPCPrefab, spawnPoint.position, Quaternion.identity); //create a new prefab of an npc
        newNPC.transform.SetParent(NPCParent.transform);

        spriteLibrary = newNPC.GetComponent<SpriteLibrary>();
        npc = newNPC.GetComponent<NPC>();

        npc.dialogueData = NPCData[index]; //assign that npc with dialogue data
        spriteLibrary.spriteLibraryAsset = NPCData[index].NPCSpriteLibraryAsset; //and assign its animation
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CreateNPC();
        }
    }
}
