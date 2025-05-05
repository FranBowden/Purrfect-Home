using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovement : MonoBehaviour
{
    public enum WaypointType { Cattery, Exit, Player, Waiting }

    public Transform[] waypointParents;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float followDistance = 1f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private bool loop = true;

    public Dictionary<WaypointType, Transform[]> waypointMap;
    private NPCBehaviour NPCbehaviour;

    public int currentWaypointIndex;
    private bool isWaiting = false;
    private Animator animator;

    private float lastInputX;
    private float lastInputY;

    void Start()
    {
        NPCbehaviour = GetComponent<NPCBehaviour>();

        animator = GetComponent<Animator>();

        waypointMap = new Dictionary<WaypointType, Transform[]>
        {
            { WaypointType.Cattery, GetChildWaypoints(waypointParents[0]) },
            { WaypointType.Exit,      GetChildWaypoints(waypointParents[1]) },
                { WaypointType.Player, new Transform[] { waypointParents[2] } },      
            { WaypointType.Waiting,    GetChildWaypoints(waypointParents[3]) }
        };
    }


    void Update()
    {
        if (PauseController.IsGamePaused || isWaiting)
        {
            animator.SetBool("isWalking", false);
            if (NPCbehaviour.enterCattery)
            {
                animator.SetFloat("LastInputX", 0f);
                animator.SetFloat("LastInputY", -1f);
            }
            else
            {
                animator.SetFloat("LastInputX", lastInputX);
                animator.SetFloat("LastInputY", lastInputY);
            }
           
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
            if (waypointMap.ContainsKey(WaypointType.Player) && waypointMap[WaypointType.Player].Length > 0)
                target = waypointMap[WaypointType.Player][0];
            else
            {
                Debug.LogError("WaypointType.Player key NOT found in waypointMap!");
            }
        }

        else if (NPCbehaviour.leaveRoom)
        {
            currentWaypointIndex = 1;
            target = waypointMap[WaypointType.Exit][currentWaypointIndex];
          

        }
        else if (NPCbehaviour.waitingRoom)
        {
            target = waypointMap[WaypointType.Waiting][currentWaypointIndex];
        }


        if (target == null)
        {
            //Debug.LogWarning($"{gameObject.name} has no valid target set - check waypoints");
            return;
        }




        Vector2 direction = (target.position - transform.position).normalized;
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (direction.magnitude > 0f)
        {
            lastInputX = direction.x;
            lastInputY = direction.y;
        }


        if (NPCbehaviour.followPlayer)
        {
            if (distanceToTarget > followDistance) //lets the npc have some distance from the player so they dont overlap.
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }

            if(distanceToTarget < followDistance) //if the npc is where the palyer is
            {
                StartCoroutine(WaitAtWaypoint()); //wait
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        }


        animator.SetFloat("CurrentInputX", direction.x);
        animator.SetFloat("CurrentInputY", direction.y);
        animator.SetBool("isWalking", direction.magnitude > 0f);

       

        if (Vector2.Distance(transform.position, target.position) < 0.1f) //calculates distance between npc and the waypoint
        {

            if (NPCbehaviour.leaveRoom)
            {
                Destroy(gameObject); //if visitor is leaving - destroy it once it reaches the exit 
            }
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

        if (NPCbehaviour.enterCattery)
        {
            animator.SetFloat("LastInputX", 0f);
            animator.SetFloat("LastInputY", -1f);
        } else
        {
             animator.SetFloat("LastInputX", lastInputX);
            animator.SetFloat("LastInputY", lastInputY);
        }
        yield return new WaitForSeconds(waitTime);

        if (NPCbehaviour.enterCattery)
        {
            currentWaypointIndex = GetNextWaypointIndex(WaypointType.Cattery, currentWaypointIndex, loop);
        }
        else if (NPCbehaviour.followPlayer)
        {
            currentWaypointIndex = 0;
        }
        else if (NPCbehaviour.leaveRoom)
        {
            currentWaypointIndex = GetNextWaypointIndex(WaypointType.Exit, currentWaypointIndex, loop);

            Debug.Log("currentWaypointIndex:" + currentWaypointIndex);
        }
        else if (NPCbehaviour.waitingRoom)
        {
            currentWaypointIndex = GetNextWaypointIndex(WaypointType.Waiting, currentWaypointIndex, loop);
        }


        isWaiting = false;
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




