using UnityEngine;

public class CatEnergy : MonoBehaviour
{
    
   public void CalculateEnergy()
    {

        CatData cat = PlayerController.Instance.catViewing.GetComponent<DisplayCatInformation>().catData;

        cat.health = (cat.hunger + cat.hygiene) / 2;
       


    }



}
