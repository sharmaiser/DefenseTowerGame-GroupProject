using System.Collections.Generic;
using UnityEngine.EventSystems;
using Valve.VR;


public class ViveController : PointerInputModule
{
    public Camera ControllerCamera;
    public SteamVR_TrackedObject RightController;
    public GameObject reticle; 
    public Transform laserTransform; 
    private Transform rightControllerTransform;
    
    // The size of the reticle will get scaled with this value 
    public float reticleSizeMultiplier = 0.02f; 
    private PointerEventData pointerEventData; 
    private RaycastResult currentRaycast;
    private GameObject currentLookAtHandler;
}

public override void Process()
{

}

void HandleLook()
{   //1  
    if (pointerEventData == null)
    {
        pointerEventData = new PointerEventData(eventSystem);
    }

    //2   
    pointerEventData.position = ControllerCamera.     ViewportToScreenPoint(new Vector3(.5f, .5f));

    //3  
    List<RaycastResult> raycastResults = new     List<RaycastResult>();

    //4  
    eventSystem.RaycastAll(pointerEventData, raycastResults);

    //5  
    currentRaycast = pointerEventData.pointerCurrentRaycast =     FindFirstRaycast(raycastResults);

    //6  
    // Move reticle  
    reticle.transform.position = rightControllerTransform.position     + (rightControllerTransform.forward     * currentRaycast.distance);

    //7   
    laserTransform.position = Vector3.Lerp(     rightControllerTransform.position, reticle.transform.     position, .5f);

    //8 
    laserTransform.LookAt(reticle.transform);

    //9  
    laserTransform.localScale = new Vector3(     laserTransform.localScale.x, laserTransform.localScale.y,     currentRaycast.distance);

    //10 
    float reticleSize = currentRaycast.distance *     reticleSizeMultiplier;
    //Scale reticle so it’s always the same size 
    reticle.transform.localScale = new Vector3(reticleSize,
    reticleSize, reticleSize);
    //11  
    // Pass the pointer data to the event system so entering and   
    // exiting of objects is detected   
    ProcessMove(pointerEventData);
}


    void Awake()
{
    rightControllerTransform = RightController.transform;
}


