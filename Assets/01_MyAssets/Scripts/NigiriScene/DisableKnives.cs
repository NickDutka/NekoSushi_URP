using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableKnives : MonoBehaviour
{
    public GameObject debaGO;
    public GameObject nakiriGO;

    public void DisableKnife()
    {
        debaGO.SetActive(false);
        nakiriGO.SetActive(false);
    }
}
