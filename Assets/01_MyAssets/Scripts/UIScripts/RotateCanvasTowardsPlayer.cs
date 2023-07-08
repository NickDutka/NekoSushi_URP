using UnityEngine;

public class RotateCanvasTowardsPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player or VR camera
    private Quaternion initialRotation;

    private void Start()
    {
        // Store the initial rotation of the canvas
        initialRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the direction from the canvas to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Calculate the rotation needed to face the player
            Quaternion targetRotation = Quaternion.LookRotation(-directionToPlayer);

            // Apply the rotation to the canvas
            transform.rotation = targetRotation * initialRotation;
        }
    }
}