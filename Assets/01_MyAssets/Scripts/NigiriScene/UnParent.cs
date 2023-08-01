using UnityEngine;
using Autohand;

public class UnParent : MonoBehaviour
{
    public PlacePoint[] placePoint;

    [SerializeField] private float massValue = 0.1f;
    [SerializeField] private float dragValue = 0.0f;
    [SerializeField] private float angularDragValue = 0.05f;
    
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
            rb.mass = massValue;
            rb.drag = dragValue;
            rb.angularDrag = angularDragValue;
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            // ... Add other properties as needed
        }

    }

}
