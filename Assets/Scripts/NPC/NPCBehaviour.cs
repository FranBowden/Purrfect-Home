using Unity.VisualScripting;
using UnityEngine;
using static WayPointMovement;

public class NPCBehaviour : MonoBehaviour
{
    public bool followPlayer = false;
    public bool leaveRoom = true;
    public bool enterCattery = false;

    public void FollowPlayer()
    {
        followPlayer = true;
        leaveRoom = false;
        enterCattery = false;
    }

    public void LeaveShelter()
    {
        leaveRoom = true;
        enterCattery = false;
        followPlayer = false;

    }


    public void EnterCattery()
    {
        enterCattery = true;
        followPlayer= false;
        leaveRoom= false;

    }



}
