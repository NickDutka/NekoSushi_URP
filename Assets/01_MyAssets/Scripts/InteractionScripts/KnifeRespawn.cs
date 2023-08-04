using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeRespawn : MonoBehaviour
{
    public GameObject yanagiba; // The prefab you want to spawn
    public GameObject deba; // The prefab you want to spawn
    public GameObject nakiri; // The prefab you want to spawn  

    public Transform yanagiRP; // The specific transform where you want to spawn the prefab
    public Transform debaRP; // The specific transform where you want to spawn the prefab
    public Transform nakiriRP; // The specific transform where you want to spawn the prefab

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == yanagiba)
        {
            Debug.Log("Yanagiba is inside the trigger.");
            yanagiba.transform.SetPositionAndRotation(yanagiRP.position, yanagiRP.rotation);
        }

        if (other.gameObject == deba)
        {
            Debug.Log("Deba is inside the trigger.");
            deba.transform.SetPositionAndRotation(debaRP.position, debaRP.rotation);  
        }

        if (other.gameObject == nakiri)
        {
            Debug.Log("Nakiri is inside the trigger.");
            nakiri.transform.SetPositionAndRotation(nakiriRP.position, nakiriRP.rotation);
        }
    }
}
