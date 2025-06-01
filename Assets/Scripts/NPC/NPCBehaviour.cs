using UnityEngine;
public class NPCBehaviour : MonoBehaviour
{

    public bool followPlayer = false;
    public bool leaveRoom = false;
    public bool enterCattery = false;
    public bool leaveCattery = false;
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
        leaveCattery = false;

        leaveRoom = true;
        enterCattery = false;
        followPlayer = false;
        waitingRoom = false;
        PlayerController.Instance.hasCompanionNPC = false;
        PlayerController.Instance.companionNPC = null;

    }
    public void EnterCattery()
    {
        //toggle collider

        leaveCattery = false;

        enterCattery = true;
        followPlayer= false;
        leaveRoom= false;
        waitingRoom = false;
        PlayerController.Instance.hasCompanionNPC = false;
        PlayerController.Instance.companionNPC = null;

    }
    public void WaitingRoom()
    {
        leaveCattery = false;

        waitingRoom = true;
        enterCattery = false;   
        followPlayer=false;
        leaveRoom=false;
        PlayerController.Instance.hasCompanionNPC = false;
        PlayerController.Instance.companionNPC = null;

    }

    public void LeaveCattery()
    {
        leaveCattery = true;
        enterCattery = false;
        followPlayer = false;
        leaveRoom = false;
        waitingRoom = false;
        PlayerController.Instance.hasCompanionNPC = false;
        PlayerController.Instance.companionNPC = null;
    }
}
