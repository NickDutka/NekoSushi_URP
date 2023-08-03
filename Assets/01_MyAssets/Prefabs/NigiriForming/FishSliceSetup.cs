using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand; 
public class FishSliceSetup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SlicedObject"))
        {
            ConfigureFishSlice(other.gameObject);
        }
    }

    public void ConfigureFishSlice(GameObject fishSlices)
    {

        fishSlices.tag = "SalmonSlice";

        // Add the FindMeshCenter component to the game object.
        FindMeshCenter findMeshCenterComponent = fishSlices.AddComponent<FindMeshCenter>();

        GameObject targetObject = findMeshCenterComponent.centerPointGO;


        // Add the PlacePoint component to the game object.
        PlacePoint placePointComponent = fishSlices.AddComponent<PlacePoint>();

        // Add the UnParent component to the game object.
        UnParent unParentComponent = fishSlices.AddComponent<UnParent>();

        // Add the PlacePointEventTemplate component to the game object.
        PlacePointEventTemplate placePointEventTemplateComponent = fishSlices.AddComponent<PlacePointEventTemplate>();

        placePointComponent.placedOffset = targetObject.transform;
        placePointComponent.parentOnPlace = true;
        placePointComponent.forcePlace = true;
        placePointComponent.forceHandRelease = true;
        placePointComponent.disableRigidbodyOnPlace = true;
        placePointComponent.disableGrabOnPlace = true;
        placePointComponent.disablePlacePointOnPlace = true;

        placePointComponent.nameCompareType = PlacePointNameType.tag;
        placePointComponent.placeNames = new string[] { "SalmonSlice" };

        Debug.Log("Fish Slice Setup Complete");
    }
}
