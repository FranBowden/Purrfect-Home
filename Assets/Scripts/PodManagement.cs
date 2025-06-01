using System.Linq;
using UnityEngine;

public class PodManagement : MonoBehaviour
{
    [SerializeField] GameObject[] LockedPods;




    private void Update()
    {
        for (int i = 0; i < LockedPods.Length; i++)
        {
            if (i < PlayerController.Instance.CatPod)
            {
                LockedPods[i].SetActive(false);
            }
            else
            {
                LockedPods[i].SetActive(true);
            }
        }

    }

}
