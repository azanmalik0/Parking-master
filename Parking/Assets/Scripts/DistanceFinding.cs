//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class DistanceFinding : MonoBehaviour
{
    int minDistance = 2;
    float dist;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            dist = Vector3.Distance(transform.position, other.transform.position);

            if (dist > minDistance)
            {
                transform.GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                transform.GetComponent<Rigidbody>().isKinematic = true;

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            transform.GetComponent<Rigidbody>().isKinematic = false;
            other.transform.GetComponent<Rigidbody>().isKinematic = false;
        }


    }
}
