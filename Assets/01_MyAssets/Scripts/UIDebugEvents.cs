using UnityEngine;
using UnityEngine.Events;

public class UIDebugEvents : MonoBehaviour
{
    public UnityEvent StepForward;
    public UnityEvent StepBackward;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StepForwardEvent();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StepBackwardEvent();
        }
    }

    public void StepForwardEvent()
    {
        StepForward?.Invoke();
    }
    public void StepBackwardEvent()
    {
          StepBackward?.Invoke();
    }
}
