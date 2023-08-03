using UnityEngine;
using Autohand;

public class PlacePointEventTemplate : MonoBehaviour {
    public PlacePoint placePoint;
    public UnParent unParent;

    private void Awake()
    {
        placePoint = GetComponent<PlacePoint>();
        unParent = GetComponent<UnParent>();
    }
    void OnEnable() {
        placePoint.OnPlaceEvent += OnPlace;
        placePoint.OnRemoveEvent += OnPlace;
        placePoint.OnHighlightEvent += OnHighlight;
        placePoint.OnStopHighlightEvent += OnStopHighlight;
    }

    private void OnDisable() {
        placePoint.OnPlaceEvent -= OnPlace;
        placePoint.OnRemoveEvent -= OnPlace;
        placePoint.OnHighlightEvent -= OnHighlight;
        placePoint.OnStopHighlightEvent -= OnStopHighlight;

    }


    public void OnPlace(PlacePoint point, Grabbable grab)
    {
        //Stuff happens when placed
        unParent.Unparent();
        Debug.Log("Event Templtate Placed");
    }


    public void OnRemove(PlacePoint point, Grabbable grab) {
        //Stuff happens when placed was removed

    }
    public void OnHighlight(PlacePoint point, Grabbable grab) {
        //Stuff happens when placepoint was highlighted

    }

    public void OnStopHighlight(PlacePoint point, Grabbable grab) {
        //Stuff happens when placepoint was done highlighting
    }
}
