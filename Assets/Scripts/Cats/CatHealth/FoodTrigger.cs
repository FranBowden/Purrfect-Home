using UnityEngine;

public class FoodTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Biscuit") || collision.CompareTag("Litter"))
        {
            Destroy(collision.gameObject);
        }
    }
}
