using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSfix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        // Set the fixed framerate based on the display frequency
        OVRPlugin.systemDisplayFrequency = 90.0f;

    }

    // Update is called once per frame
    void Update()
    {

    }
}