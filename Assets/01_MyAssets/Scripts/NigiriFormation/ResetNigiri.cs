using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetNigiri : MonoBehaviour
{
    public Transform nigiriSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SalmonSlice"))
        {
            other.gameObject.transform.position = nigiriSpawnPoint.position;
        }
    }
}
