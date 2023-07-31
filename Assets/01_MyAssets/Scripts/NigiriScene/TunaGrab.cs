using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunaGrab : MonoBehaviour
{
    public NigiriStepManager nigiriStepManager;

    public void InitializeTunaSakudori()
    {
        if (nigiriStepManager != null)
        {
            if (nigiriStepManager.nigiriStep == 4)
            {
                nigiriStepManager.nigiriStep++;
            }
        }
    }

}
