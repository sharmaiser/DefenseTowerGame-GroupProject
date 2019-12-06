using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
public class OcculusGazeInputModule : PointerInputModule
{
    public GameObject reticle;
    public Transform centerEyeTransform;
    public float reticleSizeMultiplier = 0.02f;
    private PointerEventData pointerEventData;
    private RaycastResult currentRaycast;
    private GameObject currentLookAtHandler;

    public override void Process()
    {

    }

    void HandleLook()
    {
        //1
        if (pointerEventData == null)
        {
            pointerEventData = new PointerEventData(eventSystem);
        }
        pointerEventData.position = Camera.main.ViewportToScreenPoint
            (new Vector3(.5f, .5f));

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        eventSystem.RaycastAll(pointerEventData, raycastResults);
        currentRaycast = pointerEventData.pointerCurrentRaycast = FindFirstRaycast(raycastResults);
        reticle.transform.position = centerEyeTransform.position +
             (centerEyeTransform.forward * currentRaycast.distance);
        float reticleSize = currentRaycast.distance *
             reticleSizeMultiplier;
        reticle.transform.localScale = new Vector3(reticleSize, reticleSize, reticleSize);

        ProcessMove(pointerEventData);
    }

    void HandleSelection()
    {
        if (pointerEventData.pointerEnter != null)
        {

            currentLookAtHandler = ExecuteEvents.GetEventHandler
            <IPointerClickHandler>(pointerEventData.pointerEnter);

            if (currentLookAtHandler != null && OVRInput.GetDown(OVRInput.Button.One))
            {
                ExecuteEvents.ExecuteHierarchy(currentLookAtHandler,
                pointerEventData, ExecuteEvents.pointerClickHandler);
            }
        }
        else
        {
            currentLookAtHandler = null;
        }
    }
}