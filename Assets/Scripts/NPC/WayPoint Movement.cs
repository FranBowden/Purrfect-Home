using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class WayPointMovement : MonoBehaviour
{
    public enum WaypointType { WaitingRoom, Exit, Player }
  
    public Transform[] waypointParents;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private bool loop = true;

    private Dictionary<WaypointType, Transform[]> waypointMap;
    private NPCBehaviour NPCbehaviour;
    private int currentWaypointIndex;
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
        { WaypointType.WaitingRoom, GetChildWaypoints(waypointParents[0]) },
        { WaypointType.Exit,        GetChildWaypoints(waypointParents[1]) },
        { WaypointType.Player,      GetChildWaypoints(waypointParents[2]) },
        };
    }


    void Update()
    {

        if (PauseController.IsGamePaused || isWaiting)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", lastInputX);
            animator.SetFloat("LastInputY", lastInputY);
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

        if (NPCbehaviour.waitingRoom)
        {
            target = waypointMap[WaypointType.WaitingRoom][currentWaypointIndex];
        }
        else if (NPCbehaviour.followPlayer)
        {
            target = waypointMap[WaypointType.Player][currentWaypointIndex]; ;
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
            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                //wait at the waypoint
                StartCoroutine(WaitAtWaypoint());
            }

        }

        IEnumerator WaitAtWaypoint()
        {
            isWaiting = true;
            animator.SetBool("isWalking", false);

            animator.SetFloat("LastInputX", lastInputX);
            animator.SetFloat("LastInputY", lastInputY);
            yield return new WaitForSeconds(waitTime);


        if (NPCbehaviour.waitingRoom)
        {
            currentWaypointIndex = GetNextWaypointIndex(WaypointType.WaitingRoom, currentWaypointIndex, loop);
        }
        else if (NPCbehaviour.followPlayer)
        {
            currentWaypointIndex = GetNextWaypointIndex(WaypointType.Player, currentWaypointIndex, loop);
        }
        else if (NPCbehaviour.leaveRoom)
        {
            currentWaypointIndex = GetNextWaypointIndex(WaypointType.Exit, currentWaypointIndex, loop);
        }


        isWaiting = false;
        }


    private int GetNextWaypointIndex(WaypointType type, int currentIndex, bool loop)
    {
        if (!waypointMap.ContainsKey(type) || waypointMap[type].Length == 0)
            return 0; 

        int length = waypointMap[type].Length;

        return loop
            ? (currentIndex + 1) % length
            : Mathf.Min(currentIndex + 1, length - 1);
    }

}