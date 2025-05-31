using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public enum BarType { EnergyBar, HungerBar, HygieneBar }
   
    private Dictionary<BarType, GameObject[]> barMap;
    public int barsToShow = 0;

    public void ReAssignBarMap(GameObject[] BarParents)
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
        if(parent == null) return null; 

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
        barsToShow = Mathf.RoundToInt(data * totalBars);

        BarData(barsToShow, type);
    }

    private void DeactivateAllBars(GameObject[] bars)
    {

        if (bars == null)
        {
            return;
        }

        foreach (GameObject bar in bars)
        {
            if (bar != null)
                bar.SetActive(false);
        }
    }

    void BarData(int index, BarType type)
    {
        if (barMap == null || !barMap.ContainsKey(type))
        {
            return;
        }


        DeactivateAllBars(barMap[type]);


        if (index >= 0 && index <= 6) //check index is between 0 to 6
        {
            //ERROR 
            GameObject[] bars = barMap[type];

            if (bars == null)
            {
                Debug.LogError($"barMap[{type}] array is null.");
                return;
            }
            if (index >= 0 && index < bars.Length)
            {
                GameObject bar = bars[index];
                if (bar != null)
                {
                    bar.SetActive(true);
                }
                else
                {
                    Debug.LogWarning($"barMap[{type}][{index}] is null.");
                }
            }
            else
            {
                Debug.LogWarning($"Index {index} out of bounds for barMap[{type}] (Length = {bars.Length})");
            }
        }
    }

 }

