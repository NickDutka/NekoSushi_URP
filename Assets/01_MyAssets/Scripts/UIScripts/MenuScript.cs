using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public Transform player; // Reference to the player or VR camera
    public float distanceFromPlayer = 2f; // Desired distance from the player
    public float yOffset = 0.5f; // Offset on the Y-axis

    [Range(0f, 1f)]
    public float positionDamping; // Adjust the damping factor for smooth position following
    [Range(0f, 1f)]
    public float rotationDamping; // Adjust the damping factor for smooth rotation following
    public bool rotationToggle = true;
    public bool reverseRotation = false; // Boolean for reversing look rotation
    public bool restrictYRotation = true; // Toggle for restricting rotation to Y-axis
    public float xOffset = 0f; // X-axis rotation offset in degrees

    private void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the target position based on the player's forward direction and the desired distance
            Vector3 targetPosition = player.position + player.forward * distanceFromPlayer;

            targetPosition.y += yOffset; // Add the offset on the Y-axis
            // Smoothly move the canvas to the target position using damping
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * positionDamping);

            // Check if rotation is enabled
            if (rotationToggle)
            {
                // Calculate the direction from the canvas to the player
                Vector3 directionToPlayer = player.position - transform.position;

                // Calculate the rotation needed based on the reverseRotation boolean
                Quaternion targetRotation;
                if (reverseRotation)
                {
                    targetRotation = Quaternion.LookRotation(directionToPlayer);
                }
                else
                {
                    targetRotation = Quaternion.LookRotation(-directionToPlayer);
                }

                // Apply the X-axis rotation offset
                targetRotation *= Quaternion.Euler(xOffset, 0f, 0f);

                // If rotation restriction is enabled, restrict rotation to Y-axis
                if (restrictYRotation)
                {
                    float yRotation = targetRotation.eulerAngles.y;
                    targetRotation = Quaternion.Euler(0f, yRotation, 0f);
                }

                // Smoothly rotate the canvas to the target rotation using damping
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationDamping);
            }
        }
    }
}
