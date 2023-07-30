using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonGrab : MonoBehaviour
{
    // When the player grabs the salmon fillet:

    // - disable konro dialogue canvas
    // - enable video instruction UI canvas

    // - disable UI button
    // - enable cutting reset button
    // - nigiri step ++

    public NigiriStepManager nigiriStepManager;

    public void InitializeNigiriStep()
    {
        // Ensure that the nigiriStepManager is not null before using it
        if (nigiriStepManager != null)
        {
            // If the player grabs the salmon fillet, start the nigiri instruction
            if (nigiriStepManager.nigiriStep == 0)
            {
                nigiriStepManager.nigiriStep++;
            }
        }

    }


}
