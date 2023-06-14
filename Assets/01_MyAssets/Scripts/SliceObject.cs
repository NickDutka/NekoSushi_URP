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
    private float cutForce = 2000;
    public VelocityEstimator velocityEstimator;

    void Start()
    {
        
    }


    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
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
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        //SlicedHull hull = target.Slice(planeDebug.position, planeDebug.up);
        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null)
        {

            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull);

            Destroy(target);

        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        //rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
