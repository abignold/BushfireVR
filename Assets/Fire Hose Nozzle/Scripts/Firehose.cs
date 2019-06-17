using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Firehose : MonoBehaviour
{

    private ParticleSystem particleSystem;
    public int emitValue = 5;
    private Interactable interactable;
    private SteamVR_Action_Boolean pinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    private SteamVR_Action_Boolean grabAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");


    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (interactable.attachedToHand)
        {
            SteamVR_Input_Sources hand = interactable.attachedToHand.handType;
            bool isPinch = pinchAction.GetState(hand);
            bool isGrab = grabAction.GetState(hand);
            bool isBoth = isGrab && isPinch;

            if (isBoth)
            {
                particleSystem.Emit(emitValue);
            }
        }

        
    }
}
