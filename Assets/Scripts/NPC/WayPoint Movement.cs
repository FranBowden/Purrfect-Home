using System.Collections;
using UnityEngine;

public class WayPointMovement : MonoBehaviour
{
    public Transform waypointParent;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private bool loop = true;

    private Transform[] waypoints;
    private int currentWaypointIndex;
    private bool isWaiting = false;
    private Animator animator;

    private float lastInputX;
    private float lastInputY;


    void Start()
    {
        animator = GetComponent<Animator>();

        waypoints = new Transform[waypointParent.childCount];

        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }
    }

 
    void Update()
    {
        if (isWaiting)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", lastInputX);
            animator.SetFloat("LastInputY", lastInputY);
            return;
        }

        MoveToWayPoint();

    }

    void MoveToWayPoint()
    {
        Transform target = waypoints[currentWaypointIndex];
        Vector2 direction = (target.position - transform.position).normalized;

        if(direction.magnitude > 0f)
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

        currentWaypointIndex = loop ? (currentWaypointIndex++) % waypoints.Length : Mathf.Min(currentWaypointIndex++, waypoints.Length - 1);

        isWaiting = false;
    }
}
