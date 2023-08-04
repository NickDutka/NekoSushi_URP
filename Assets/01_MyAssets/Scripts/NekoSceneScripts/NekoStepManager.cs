using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoStepManager : MonoBehaviour
{

    public MySceneLoader sceneLoader;
    public NekoDialogueManager nekoDialogueManager;
    public int nekoStep;
    public DOTweenPath nekoPath;
    public DOTweenPath shipPath;

    //Step flags
    public bool isShipMoving;
    public bool isNekoMoving;
    public bool isNekoDialogueStarted;

    
    // Start is called before the first frame update
    void Start()
    {
        nekoStep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(nekoStep == 1 && isShipMoving == false)
        {
            isShipMoving = true;
            shipPath.DOPlay();
        }
    }
}
