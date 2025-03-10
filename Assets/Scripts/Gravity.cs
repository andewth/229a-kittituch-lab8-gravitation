using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody rb;
    private const float G = 0.00674f;

    public static List <Gravity> GravityObjectsList;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        if (GravityObjectsList == null)
        {
            GravityObjectsList = new List<Gravity>();
        }
        
        GravityObjectsList.Add(this);
    }

    private void FixedUpdate()
    {
        foreach (var obj in GravityObjectsList)
        {
            Attract(obj);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void Attract(Gravity other)
    {
        Rigidbody rbOther = other.rb;
        
        Vector3 direction = rb.position - rbOther.position;
        
        float distance = direction.magnitude;
        
        if (distance == 0)
        {
            return;
        }

        float forceMagnitude = G * (rb.mass * rbOther.mass)/Mathf.Pow(distance,2);
        
        Vector3 force = forceMagnitude * direction.normalized;
        
        rbOther.AddForce(force);

    }

}
