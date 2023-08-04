using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasabiAnchor : MonoBehaviour
{
    [SerializeField] private Transform anchorPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wasabi")) // Replace "YourTagHere" with the tag of the objects you want to trigger the spawn.
        {
            other.transform.position = anchorPoint.position;
            other.transform.parent = anchorPoint;
        }
    }

}