using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovement : MonoBehaviour
{
    public enum WaypointType { Cattery, Exit, Player }

    public Transform[] waypointParents;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private bool loop = true;

    public Dictionary<WaypointType, Transform[]> waypointMap;
    private NPCBehaviour NPCbehaviour;

    public int currentWaypointIndex;
    private bool isWaiting = false;
    private Animator animator;

    private float lastInputX;
    private float lastInputY;
    // public Transform playerTransform;
    public Transform playerTransform;
    void Start()
    {
        NPCbehaviour = GetComponent<NPCBehaviour>();

        animator = GetComponent<Animator>();

        waypointMap = new Dictionary<WaypointType, Transform[]>
        {
            { WaypointType.Cattery, GetChildWaypoints(waypointParents[0]) },
            { WaypointType.Exit,      GetChildWaypoints(waypointParents[1]) },
             { WaypointType.Player,     new Transform[] { playerTransform } }
        };
    }


    void Update()
    {
        if (PauseController.IsGamePaused || isWaiting)
        {
            animator.SetBool("isWalking", false);

            animator.SetFloat("LastInputX", 0f);
            animator.SetFloat("LastInputY", 1f);
            // animator.SetFloat("LastInputX", lastInputX);
            //animator.SetFloat("LastInputY", lastInputY);
            return;
        }

        MoveToWayPoint();
    }

    private Transform[] GetChildWaypoints(Transform parent)
    {
        Transform[] children = new Transform[parent.childCount];

        for (int i = 0; i < parent.childCount; i++)
        {
            children[i] = parent.GetChild(i);
        }

        return children;
    }


    void MoveToWayPoint()
    {
        Transform target = null;

        if (NPCbehaviour.enterCattery)
        {
            target = waypointMap[WaypointType.Cattery][currentWaypointIndex];
        }
        else if (NPCbehaviour.followPlayer)
        {
            if (waypointMap.ContainsKey(WaypointType.Player) && waypointMap[WaypointType.Player].Length > 0 && waypointMap[WaypointType.Player][0] != null)
            {
                target = playerTransform;
            }
            else
            {
                Debug.LogError("WaypointType.Player key NOT found in waypointMap!");
            }
        }

        else if (NPCbehaviour.leaveRoom)
        {
            target = waypointMap[WaypointType.Exit][currentWaypointIndex];
        }
        if (target == null)
        {
            Debug.LogWarning($"{gameObject.name} has no valid target set - check waypoints");
        }


        Vector2 direction = (target.position - transform.position).normalized;

        if (direction.magnitude > 0f)
        {
            lastInputX = direction.x;
            lastInputY = direction.y;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        animator.SetFloat("CurrentInputX", direction.x);
        animator.SetFloat("CurrentInputY", direction.y);
        animator.SetBool("isWalking", direction.magnitude > 0f);
        if (Vector2.Distance(transform.position, target.position) < 0.1f) //calculates distance between npc and the waypoint
        {
            //wait at the waypoint
            StartCoroutine(WaitAtWaypoint());
        }

    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        animator.SetBool("isWalking", false);

        // animator.SetFloat("LastInputX", lastInputX);
        //animator.SetFloat("LastInputY", lastInputY);
        animator.SetFloat("LastInputX", 0f);
        animator.SetFloat("LastInputY", -1f);
        yield return new WaitForSeconds(waitTime);

        if (NPCbehaviour.enterCattery)
        {
            currentWaypointIndex = GetNextWaypointIndex(WaypointType.Cattery, currentWaypointIndex, loop);
        }
        else if (NPCbehaviour.followPlayer)
        {
            currentWaypointIndex = GetNextWaypointIndex(WaypointType.Player, currentWaypointIndex, loop);
        }
        else if (NPCbehaviour.leaveRoom)
        {
            if (currentWaypointIndex == 1)
            {
                NPCbehaviour.leaveRoom = false;
                animator.SetBool("isWalking", false);

                StartCoroutine(LeaveAndDestroy());
            }
            else if (currentWaypointIndex == 0)
            {
                NPCbehaviour.leaveRoom = false;
                animator.SetBool("isWalking", false);

                Debug.Log("NPC is already at waypoint 0, leaving now.");
            }
            else
            {
                currentWaypointIndex++;
            }
        }


        isWaiting = false;
    }


    IEnumerator LeaveAndDestroy()
    {
        yield return new WaitForSeconds(1f);  // Adjust delay as needed
        Destroy(gameObject);  // Destroy the NPC when they leave
    }



    private int GetNextWaypointIndex(WaypointType type, int currentIndex, bool loop)
    {
        if (!waypointMap.ContainsKey(type) || waypointMap[type].Length == 0)
            return 0;

        int length = waypointMap[type].Length;

        if (type == WaypointType.Cattery) //if the waypoint is cattery
        {
            if (currentIndex < 2)
            {
                return currentIndex + 1;
            }
            else if (currentIndex == 2)
            {
                waitTime = 5f; //have a longer wait time


                NPC vistor = GetComponent<NPC>();
                if (vistor.dialogueData.Length > vistor.dialogueDataIndex++)
                {
                    vistor.dialogueDataIndex = 1;
                }

                return 3;
            }
            else
            {

                int next = currentIndex + 1;
                if (next > length - 1) //if next is more than length of the array
                    next = 3; //next should go back to 3
                return next;
            }
        }
        else
        {

            return loop
                ? (currentIndex + 1) % length
                : Mathf.Min(currentIndex + 1, length - 1);
        }
    }
}




