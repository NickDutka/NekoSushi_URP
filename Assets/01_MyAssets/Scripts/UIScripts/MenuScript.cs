using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public Transform player; // Reference to the player or VR camera
    public float distanceFromPlayer = 2f; // Desired distance from the player

    [Range(0f, 1f)]
    public float positionDamping; // Adjust the damping factor for smooth position following
    [Range(0f, 1f)]
    public float rotationDamping; // Adjust the damping factor for smooth rotation following
    public bool rotationToggle = true;

    private void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the target position based on the player's forward direction and the desired distance
            Vector3 targetPosition = player.position + player.forward * distanceFromPlayer;

            // Smoothly move the canvas to the target position using damping
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * positionDamping);

            // Check if rotation is enabled
            if (rotationToggle)
            {
                // Calculate the direction from the canvas to the player
                Vector3 directionToPlayer = player.position - transform.position;

                // Calculate the rotation needed to face the player
                Quaternion targetRotation = Quaternion.LookRotation(-directionToPlayer);

                // Smoothly rotate the canvas to face the player using damping
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationDamping);
            }
        }
    }
}
