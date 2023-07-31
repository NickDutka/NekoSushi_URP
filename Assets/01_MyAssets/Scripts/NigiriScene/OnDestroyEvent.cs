using UnityEngine;

public class OnDestroyEvent : MonoBehaviour
{
    // Event to notify when the object is destroyed
    public event System.Action<GameObject> OnObjectDestroyed;

    private void OnDestroy()
    {
        // Call the event when the object is destroyed
        OnObjectDestroyed?.Invoke(gameObject);
    }
}
