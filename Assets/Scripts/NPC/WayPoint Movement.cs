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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waypoints = new Transform[waypointParent.childCount];

        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting) return;

        MoveToWayPoint();

    }

    void MoveToWayPoint()
    {
        Transform target = waypoints[currentWaypointIndex];

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            //wait at the waypoint
            StartCoroutine(WaitAtWaypoint());
        }

    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        currentWaypointIndex = loop ? (currentWaypointIndex++) % waypoints.Length : Mathf.Min(currentWaypointIndex++, waypoints.Length - 1);

        isWaiting = false;
    }
}
