using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{

    //[SerializeField]
    //private Transform planeDebug;
    //[SerializeField]
    //private GameObject target;
    [SerializeField]
    private Transform startSlicePoint;
    [SerializeField] 
    private Transform endSlicePoint;
    [SerializeField]
    private LayerMask sliceableLayer;
    [SerializeField]
    private Material crossSectionMaterial;
    [SerializeField]
    private float cutForce = 2;
    public VelocityEstimator velocityEstimator;

    void Start()
    {
        
    }


    void FixedUpdate()
    {

        int sliceableLayerMask = 1 << LayerMask.NameToLayer("SliceableLayer");
        int knifeBladeLayerMask = 1 << LayerMask.NameToLayer("KnifeBladeLayer");
        int layerMask = Physics.DefaultRaycastLayers & ~(sliceableLayerMask | knifeBladeLayerMask);
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, layerMask);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }   
        // Editor Testing
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Slice(target);
        //}
    }

    public void Slice(GameObject target)
    {
        // og slice
        //Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        //Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        //planeNormal.Normalize();

        // dot product slice
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        float dotProduct = Vector3.Dot(velocity.normalized, Vector3.down); // Check if velocity is moving downwards

        float angleThreshold = 0.5f; // Define the angle threshold (adjust as needed)

        if (dotProduct > angleThreshold) // Only slice if moving downwards within the threshold angle
        {
            //SlicedHull hull = target.Slice(planeDebug.position, planeDebug.up);
            SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

            if (hull != null)
            {

                GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
                SetupSlicedComponent(upperHull);
                upperHull.layer = target.layer;
                GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
                SetupSlicedComponent(lowerHull);
                lowerHull.layer = target.layer;

                Destroy(target);

            }
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
