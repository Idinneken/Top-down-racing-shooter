using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInitialRotation : MonoBehaviour
{
    public Quaternion initialRotation;
    
    void Start()
    {
        transform.rotation = initialRotation;
    }

}
