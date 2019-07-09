using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Firehose : MonoBehaviour
{

    private ParticleSystem particleSystem;
    private List<ParticleCollisionEvent> collisionEvents;
    private AudioSource audioSource;
    private Canvas canvas;
    private TextAsset text;
    public int emitValue = 400;
    public float powerModifier = 1f;
    private Interactable interactable;
    private SteamVR_Action_Boolean pinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    private SteamVR_Action_Boolean grabAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    private float waterPercentage = 100f;
    public float waterUsePerSecond = 1;
    private bool useWater = true;
    private SteamVR_Input_Sources currentHand = SteamVR_Input_Sources.Any;


    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        bool isBoth = false;
        if (interactable.attachedToHand)
        {
            SteamVR_Input_Sources hand = interactable.attachedToHand.handType;
            bool isPinch = pinchAction.GetState(hand);
            bool isGrab = grabAction.GetState(hand);
            isBoth = isGrab && isPinch;

            if (isBoth)
            {   
                particleSystem.Emit(this.emitValue);
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }

        if (!isBoth)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
        other.SendMessageUpwards("WaterCollision", (numCollisionEvents * this.powerModifier), SendMessageOptions.DontRequireReceiver);
    }



}
