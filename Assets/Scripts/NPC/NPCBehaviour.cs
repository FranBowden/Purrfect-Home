using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public bool followPlayer = false;
    public bool leaveRoom = false;
    public bool waitingRoom = true;

    private GameObject player;
    private WayPointMovement npcMovement;
  
    private void Start()
    {
        npcMovement = GetComponent<WayPointMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public void GoToWaitingRoom()
    {
    }

    public void FollowPlayer()
    {
       
    }

    public void LeaveRoom()
    {
    }
}
