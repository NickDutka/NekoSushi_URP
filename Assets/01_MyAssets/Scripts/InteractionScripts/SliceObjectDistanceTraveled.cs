using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using Autohand;

public class SliceObjectDistanceTraveled : MonoBehaviour
{
    [SerializeField]
    private Transform startSlicePoint;
    [SerializeField] 
    private Transform endSlicePoint;
    [SerializeField]
    private Transform layerSwitchStartPoint;
    [SerializeField]
    private Transform layerSwitchEndPoint;
    [SerializeField]
    private LayerMask sliceableLayer;
    [SerializeField]
    private LayerMask knifeBladeLayer;
    [SerializeField]
    private LayerMask defaultLayer;
    [SerializeField]
    private GameObject knifeBladeGO;
    [SerializeField]
    private Material crossSectionMaterial;
    [SerializeField]
    private float cutForce = 2;
    public VelocityEstimator velocityEstimator;

    [SerializeField]
    private float sliceCooldown = 0.5f; // Cooldown duration in seconds
    [SerializeField]
    private bool canSlice = true; // Flag to track if slicing is allowed

    private void Update()
    {
        int sliceableLayerMask = sliceableLayer.value;
        int knifeBladeLayerMask = knifeBladeLayer.value;
        int defaultLayerMask = defaultLayer.value;

        bool isSlicing = Physics.Linecast(layerSwitchStartPoint.position, layerSwitchEndPoint.position, out RaycastHit layerSwitchhit, sliceableLayerMask);
         
        if (isSlicing && canSlice == true)
        {
            //Debug.Log("Knife is slicing, switch layer");
            int knifeBladeLayerIndex = LayerMask.NameToLayer("KnifeBlade");
            knifeBladeGO.layer = knifeBladeLayerIndex;

        }
        else
        {
            //Debug.Log("Knife is not slicing, switch layer back");
            int defaultLayerIndex = LayerMask.NameToLayer("Default");
            knifeBladeGO.layer = defaultLayerIndex;
        }
    }
    void FixedUpdate()
    {
        int sliceableLayerMask = sliceableLayer;

        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit sliceHit, sliceableLayerMask);

        if (hasHit && canSlice == true)
        {
            GameObject target = sliceHit.transform.gameObject;
            Debug.Log("Hit" + sliceHit.point);
            
            Slice(target);
            StartCoroutine(SliceCooldown());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startSlicePoint.position, endSlicePoint.position);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(layerSwitchStartPoint.position, layerSwitchEndPoint.position);
    }

    public void Slice(GameObject target)
    {

        // dot product slice
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        float dotProduct = Vector3.Dot(velocity.normalized, Vector3.down); // Check if velocity is moving downwards

        float angleThreshold = 0.5f; // Define the angle threshold (adjust as needed)

        if (dotProduct > angleThreshold) // Only slice if moving downwards within the threshold angle
        {
            SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

            if (hull != null)
            {

                GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
                upperHull.layer = LayerMask.NameToLayer("Sliceable");
                upperHull.tag = "SlicedObject";
                SetupSlicedComponent(upperHull);
                
                GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
                lowerHull.layer = LayerMask.NameToLayer("Sliceable");
                lowerHull.tag = "SlicedObject";
                SetupSlicedComponent(lowerHull);
                

                Destroy(target);

            }
        }
    }
    private IEnumerator SliceCooldown()
    {
        // Disable slicing temporarily
        canSlice = false;

        // Wait for the specified cooldown duration
        yield return new WaitForSeconds(sliceCooldown);

        // Enable slicing again
        canSlice = true;
    }


    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        slicedObject.AddComponent<Grabbable>();
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
