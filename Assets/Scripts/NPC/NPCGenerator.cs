using UnityEngine;
using UnityEngine.U2D.Animation;

public class NPCGenerator : MonoBehaviour
{
    
    [SerializeField] private GameObject NPCPrefab;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private NPCDialogue[] NPCData;

    private NPC npc;
    private SpriteLibrary spriteLibrary;
  private void CreateNPC()
    {
        int index = Random.Range(0, NPCData.Length);
        GameObject newNPC = Instantiate(NPCPrefab, spawnPoint.position, Quaternion.identity);

        spriteLibrary = newNPC.GetComponent<SpriteLibrary>();
        npc = newNPC.GetComponent<NPC>();

        npc.dialogueData = NPCData[index];
        spriteLibrary.spriteLibraryAsset = NPCData[index].NPCSpriteLibraryAsset;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CreateNPC();
        }
    }
}
