using UnityEngine;

[System.Serializable]
public class SlicedObjectData
{
    public int layerNum;
    public string tagName;

    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    // Additional properties for other components
    public bool hasMeshFilter;
    public bool hasMeshRenderer;
    public bool hasRigidbody;
    public bool hasMeshCollider;
    public bool hasGrabbableScript;
    public bool hasOnDestroyEventScript;
    // Add any other necessary data here.
    public Mesh mesh;
    public Material[] materials;
    // Constructor that automatically populates the data from the given GameObject
    public SlicedObjectData(GameObject obj)
    {
        layerNum = obj.layer;
        tagName = obj.tag;
        position = obj.transform.position;
        rotation = obj.transform.rotation;
        scale = obj.transform.localScale;
        hasMeshFilter = obj.GetComponent<MeshFilter>() != null;
        hasMeshRenderer = obj.GetComponent<MeshRenderer>() != null;
        hasRigidbody = obj.GetComponent<Rigidbody>() != null;
        hasMeshCollider = obj.GetComponent<MeshCollider>() != null;
        hasGrabbableScript = obj.GetComponent<Autohand.Grabbable>() != null;
        hasOnDestroyEventScript = obj.GetComponent<OnDestroyEvent>() != null;
        
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            mesh = meshFilter.sharedMesh;
        }

        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            materials = meshRenderer.sharedMaterials;
        }


        // Add any other necessary data here.
    }

    // Static method to create SlicedObjectData instance from a GameObject
    public static SlicedObjectData FromGameObject(GameObject obj)
    {
        return new SlicedObjectData(obj);
    }
}