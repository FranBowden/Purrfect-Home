using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{

    public enum BarType { EnergyBar, HungerBar, HygieneBar }
   
    [SerializeField] private GameObject[] BarParents;

    private Dictionary<BarType, GameObject[]> barMap;

    private void Start()
    {
        barMap = new Dictionary<BarType, GameObject[]>
         {
            { BarType.EnergyBar, GetChildrenBars(BarParents[0]) },
            { BarType.HungerBar,        GetChildrenBars(BarParents[1]) },
            { BarType.HygieneBar,      GetChildrenBars(BarParents[2]) },
        };
    }


    private GameObject[] GetChildrenBars(GameObject parent)
    {
        Transform parentTransform = parent.transform;
        GameObject[] children = new GameObject[parentTransform.childCount];

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            children[i] = parentTransform.GetChild(i).gameObject;
        }

        return children;
    }

    //which bar are we getting
    public void CalculateBar(float data, BarType type)
    {
        int totalBars = 6;
        int barsToShow = Mathf.RoundToInt(data * totalBars);

        BarData(barsToShow, type);
    }

    private void DeactivateAllBars(GameObject[] bars)
    {
        foreach (GameObject bar in bars)
        {
            bar.SetActive(false);
        }
    }

    void BarData(int index, BarType type)
    {

        DeactivateAllBars(barMap[type]);
        if (index >= 0 && index <= 6) //check index is between 0 to 6
        {
            barMap[type][index].SetActive(true);
        }

    }
}
