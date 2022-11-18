using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speen : MonoBehaviour
{
    public Vector3 spinRate;

    void Update()
    {
        transform.Rotate(new Vector3(spinRate.x * Time.deltaTime, spinRate.y * Time.deltaTime, spinRate.z * Time.deltaTime));
    }    
}
