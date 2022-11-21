using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string statName;
    public int value;

    public bool affectsStats = true;
    public bool collectEvenIfNoMatchingStat = false;    
    
}
