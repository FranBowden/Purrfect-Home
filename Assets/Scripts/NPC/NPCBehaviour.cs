using UnityEngine;
public class NPCBehaviour : MonoBehaviour
{

    public bool followPlayer = false;
    public bool leaveRoom = false;
    public bool enterCattery = false;
    public bool waitingRoom = true;

    public void FollowPlayer()
    {
        PlayerController.Instance.hasCompanionNPC = true;
        PlayerController.Instance.companionNPC = gameObject;
        followPlayer = true;
        leaveRoom = false;
        enterCattery = false;
        waitingRoom = false;
    }

    public void LeaveShelter()
    {
        leaveRoom = true;
        enterCattery = false;
        followPlayer = false;
        waitingRoom = false;
        PlayerController.Instance.hasCompanionNPC = false;
        PlayerController.Instance.companionNPC = null;

    }
    public void EnterCattery()
    {
        enterCattery = true;
        followPlayer= false;
        leaveRoom= false;
        waitingRoom = false;
        PlayerController.Instance.hasCompanionNPC = false;
        PlayerController.Instance.companionNPC = null;

    }

    public void WaitingRoom()
    {
        waitingRoom = true;
        enterCattery = false;   
        followPlayer=false;
        leaveRoom=false;
        PlayerController.Instance.hasCompanionNPC = false;
        PlayerController.Instance.companionNPC = null;

    }
}
