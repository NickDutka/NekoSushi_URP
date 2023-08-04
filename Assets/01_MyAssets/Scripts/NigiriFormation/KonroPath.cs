using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class KonroPath : MonoBehaviour
{
    private DOTweenPath dotweenPath;
    private List<Vector3> reversePath; // The reverse path points
    public ModularRotateTowardsPlayer modularRotateTowardsPlayer;

    private void Start()
    {
        // Get the reference to the DOTweenPath component
        dotweenPath = GetComponent<DOTweenPath>();

        // Reverse the original path
        int numWaypoints = dotweenPath.wps.Count;
        reversePath = new List<Vector3>(numWaypoints);
        for (int i = numWaypoints - 1; i >= 0; i--)
        {
            reversePath.Add(dotweenPath.wps[i]);
        }   
    }

    // Function to make the character follow the initial path
    public void FollowInitialPath()
    {
        // Start the movement of the GameObject along the initial path
        dotweenPath.DOPlay();
        modularRotateTowardsPlayer.enabled = false;
    }

    // Function to make the character follow the reverse path
    public void FollowReversePath()
    {
        transform.DOLookAt(reversePath[1], 0.01f);
        transform.DOPath(reversePath.ToArray(), 5f, PathType.CatmullRom).SetEase(Ease.Linear).SetLookAt(0.01f).OnComplete(OnReversePathComplete);
    }

    // This function will be called on each frame while following the path


    // This function will be called when the reverse path is completed
    private void OnReversePathComplete()
    {
        Debug.Log("Reverse path complete!");
        modularRotateTowardsPlayer.enabled = true;
        // You can add any further actions or logic here after the reverse path is completed.
    }
}