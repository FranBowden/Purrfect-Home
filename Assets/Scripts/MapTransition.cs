using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{

    //https://www.youtube.com/watch?v=9r9YbHsjSKs&list=PLaaFfzxy_80HtVvBnpK_IjSC8_Y9AOhuP&index=7&ab_channel=GameCodeLibrary

    [SerializeField] private PolygonCollider2D mapBoundary;
    [SerializeField] private CinemachineConfiner2D confiner;
    [SerializeField] Direction direction;
    enum Direction { up, down, left, right }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundary;
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newpos = player.transform.position;

        switch(direction)
        {
            case Direction.up:
                newpos.y += 0.8f;
                break;

            case Direction.down:
                newpos.y -= 0.8f;
                break;
            case Direction.left:
                newpos.x -= 0.8f;
                break;
            case Direction.right:
                newpos.x += 0.8f;
                break;
        }

        player.transform.position = newpos;
    }
}
