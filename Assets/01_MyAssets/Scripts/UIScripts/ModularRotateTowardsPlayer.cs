using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ModularRotateTowardsPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player or VR camera

    // Rotation axis flags
    public bool rotateOnXAxis = true;
    public bool rotateOnYAxis = true;
    public bool rotateOnZAxis = true;

    private Quaternion initialRotation;

    [Range(0, 1)]
    [SerializeField]
    private float rotationDampening;
    
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

            // Calculate the rotation needed to face the player based on selected axes
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            // Apply the rotation to the canvas based on selected axes
            Vector3 eulerRotation = targetRotation.eulerAngles;
            if (!rotateOnXAxis) eulerRotation.x = initialRotation.eulerAngles.x;
            if (!rotateOnYAxis) eulerRotation.y = initialRotation.eulerAngles.y;
            if (!rotateOnZAxis) eulerRotation.z = initialRotation.eulerAngles.z;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulerRotation), rotationDampening);
        }
    }
}
