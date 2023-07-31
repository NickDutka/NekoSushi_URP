using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEnableDelay : MonoBehaviour
{

    public MonoBehaviour scriptToEnable;

    private void Start()
    {
        // StartCoroutine(EnableTargetScriptDelayed());
    }
    private void OnEnable()
    {
        Debug.Log("GameObject is now active.");
        // Any other actions you want to perform when the GameObject is activated

        StartCoroutine(EnableTargetScriptDelayed());

    }
    private void OnDisable()
    {
        DisableScript();
    }

    private IEnumerator EnableTargetScriptDelayed()
    {
        yield return new WaitForSeconds(3f);
        scriptToEnable.enabled = true;
    }

    private void DisableScript()
    {
        scriptToEnable.enabled = false;
    }
}
