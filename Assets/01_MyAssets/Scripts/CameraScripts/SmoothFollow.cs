using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;

    [Range(0, 1)]
    public float positionDampening;

    [Range(0, 1)]
    public float rotationDampening;

    // Start is called before the first frame update
    void OnEnable()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // slow down when close to target
        transform.position = Vector3.Lerp(transform.position, target.position, positionDampening);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationDampening);
    }
}
