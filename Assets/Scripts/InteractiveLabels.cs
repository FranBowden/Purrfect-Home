using System;
using TMPro;
using UnityEngine;

public class InteractiveLabels : MonoBehaviour
{
    [SerializeField] private string task;
    [SerializeField] private GameObject arrow;
    [SerializeField] private float radius = 0.4f;
    private Vector3 newPosition;
    private bool textDisplaying = false;
    private GameObject text;
    private void Start()
    {
        Vector3 positions = gameObject.transform.position;
        newPosition = new Vector3(positions.x + 1.5f, positions.y - 1.5f, positions.z);
    }
    void Update()
    {
        Collider[] HitCollider = Physics.OverlapSphere(gameObject.transform.position, radius);
        foreach(Collider c in HitCollider)
        {
            Debug.Log(c.gameObject.tag);
            if(c.gameObject.tag == "Player" && !textDisplaying)
            {

                text = Instantiate(arrow, newPosition, Quaternion.identity);
                arrow.GetComponentInChildren<TextMeshPro>().text = task;
                textDisplaying = true;

            }

            if(textDisplaying && c.gameObject.tag != "Player")
            {
                textDisplaying=false;
                Destroy(text);
            }
        }
    }
}
