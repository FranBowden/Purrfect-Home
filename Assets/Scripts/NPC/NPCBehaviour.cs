using UnityEditor.Rendering;
using UnityEngine;
public class NPCBehaviour : MonoBehaviour
{
    public bool followPlayer = false;
    public bool leaveRoom = false;
    public bool enterCattery = false;
    public bool waitingRoom = true;

    public void FollowPlayer()
    {
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
    }
    public void EnterCattery()
    {
        enterCattery = true;
        followPlayer= false;
        leaveRoom= false;
        waitingRoom = false;
    }

    public void WaitingRoom()
    {
        waitingRoom = true;
        enterCattery = false;   
        followPlayer=false;
        leaveRoom=false;
    }
}
