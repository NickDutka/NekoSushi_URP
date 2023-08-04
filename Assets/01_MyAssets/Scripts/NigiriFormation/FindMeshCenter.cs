
using UnityEngine;

public class FindMeshCenter : MonoBehaviour
{
    public GameObject centerPointGO; // Public field to store the reference to the "CenterPoint" GameObject

    void Awake()
    {
        // Create an empty GameObject at the center position
        centerPointGO = new GameObject("CenterPoint");

        // Set the position of the "CenterPoint" in local space (center of the cube's mesh)
        centerPointGO.transform.localPosition = CalculateMeshCenter();

        // Make the "CenterPoint" GameObject a child of the original cube
        centerPointGO.transform.SetParent(transform);
    }

    private Vector3 CalculateMeshCenter()
    {
        // Get the MeshFilter component attached to the GameObject
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            // Get the mesh vertices (using sharedMesh to consider position and scale)
            Vector3[] vertices = meshFilter.sharedMesh.vertices;

            // Calculate the total position
            Vector3 totalPosition = Vector3.zero;

            // Calculate the center position
            for (int i = 0; i < vertices.Length; i++)
            {
                totalPosition += vertices[i];
            }

            // Calculate the average position
            Vector3 centerPosition = totalPosition / vertices.Length;

            // Convert center position from local space to world space
            centerPosition = transform.TransformPoint(centerPosition);

            return centerPosition;
        }
        else
        {
            Debug.LogError("MeshFilter component not found!");
            return Vector3.zero;
        }
    }
}