using UnityEngine;

public class KnifeBladeColliderChecker : MonoBehaviour
{
    public LayerMask sliceableLayer; // The layer you want to check against
    public bool isColliding = false; // Whether or not the object is colliding with something in the target layer

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other gameObject is on the target layer
        if (sliceableLayer == (sliceableLayer | (1 << other.gameObject.layer)))
        {
            // Trigger event occurred with an object in the target layer
            Debug.Log("Trigger with object in target layer detected.");
            // Perform your desired actions here
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the other gameObject is on the target layer
        if (sliceableLayer == (sliceableLayer | (1 << other.gameObject.layer)))
        {
            // Trigger event with an object in the target layer ended
            Debug.Log("Trigger with object in target layer ended.");
            // Perform your desired actions here
            isColliding = false;
        }
    }
}