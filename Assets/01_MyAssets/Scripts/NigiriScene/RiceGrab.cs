using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceGrab : MonoBehaviour
{
    // This function will be called when something enters the trigger collider

    public bool isInsideRice = false;
    public bool tryGrabRice = false;
    //public bool enteredRice = false;

    public GameObject ricePrefab;

    //public GameObject playerHand;
    public Transform playerHandSpawnPoint;
    public Collider handCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other == handCollider)
        {
            // Do something specific for the player object.

            Debug.Log("Hand entered the Rice!");
            //enteredRice = true;
            isInsideRice = true;

            //GrabRiceFromSteamer();
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("Something is in the Rice!");
    //    isInsideRice = true;
    //    // You can perform any desired actions here when something stays in the trigger.
    //    // For example, you can check the tag of the collider that stayed in the trigger:
    //    if (other.CompareTag("Player"))
    //    {
    //        // Do something specific for the player object.
    //    }
    //}
    private void OnTriggerExit(Collider other)
    {
        if (other == handCollider)
        {
            // Do something specific for the player object.

            Debug.Log("Hand exited the Rice!");
            //enteredRice = true;
            isInsideRice = false;
            tryGrabRice = false;
        }
    }

    // When player hand calls "OnGrab" function, check if player hand is inside the rice, if so, grab rice
    public void GrabRiceFromSteamer()
    {
        if (isInsideRice == true && tryGrabRice == false)
        {

            Instantiate(ricePrefab, playerHandSpawnPoint.position, playerHandSpawnPoint.rotation);
            tryGrabRice = true;
            Debug.Log("Grabbed Rice!");

        }
    }
}
