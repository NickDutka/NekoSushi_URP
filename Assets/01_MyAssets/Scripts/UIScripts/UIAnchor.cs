using UnityEngine;

public class UIAnchor : MonoBehaviour
{
    public Transform objectToTrack; // Reference to the object you want to track
    public Vector3 offset; // The position offset from the tracked object

    private void LateUpdate()
    {
        // Ensure the UI anchor maintains the same position above the object
        if (objectToTrack != null)
        {
            transform.position = objectToTrack.position - offset;
        }
    }
}