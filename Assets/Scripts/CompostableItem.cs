using System.Collections.Generic;
using UnityEngine;
public class CompostableItem : MonoBehaviour
{

    public string itemName;
    public float nitrogenContent; // For "green" materials
    public float carbonContent; // For "brown" materials
    public float itemWeight; // in lbs 

    public CompostableItem(string name, float nitrogen, float carbon, float weight)
    {
        itemName = name;
        nitrogenContent = nitrogen;
        carbonContent = carbon;
        itemWeight = weight;
    }
}