using UnityEngine;
using Autohand;

public class UnParent : MonoBehaviour
{
    public PlacePoint[] placePoint;
    public void Unparent()
    {
        placePoint = GetComponentsInParent<PlacePoint>();

        if (placePoint.Length > 0)
        {
            placePoint[1].Remove();
            gameObject.tag = "Nigiri";
            gameObject.layer = 0;
            // Set the parent of the GameObject to null to unparent it.
            transform.parent = null;

            // Check if the Rigid Body component already exists on the GameObject
            Rigidbody rb = GetComponent<Rigidbody>();

            // If the Rigid Body component doesn't exist, add it
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }

            // Set any desired properties for the Rigid Body component
            rb.mass = 0.1f;
            rb.drag = 0.0f;
            rb.angularDrag = 0.05f;
            rb.useGravity = true;
            rb.isKinematic = false;
            // ... Add other properties as needed
        }

    }

}
