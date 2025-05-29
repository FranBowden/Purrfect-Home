using UnityEngine;

public class PauseEvents : MonoBehaviour
{
    public bool pauseEvents = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void onPause()
    {
        if (player != null && !pauseEvents)
        {
            if (player.TryGetComponent<PlayerMovement>(out var movement))
            {
                movement.enabled = false;
                pauseEvents = true;
            }
        }
    }
}
