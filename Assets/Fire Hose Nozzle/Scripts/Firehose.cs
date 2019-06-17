using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Firehose : MonoBehaviour
{

    private ParticleSystem particleSystem;
    private List<ParticleCollisionEvent> collisionEvents;
    public int emitValue = 5;
    public float sizeDecrease = 0.99f;
    private Interactable interactable;
    private SteamVR_Action_Boolean pinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    private SteamVR_Action_Boolean grabAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");


    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
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

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

        other.SendMessageUpwards("WaterCollision", numCollisionEvents, SendMessageOptions.DontRequireReceiver);


        //int i = 0;

        //while (i < numCollisionEvents)
        //{
        //    if (other.name != "Ground")
        //    {
        //        other.SendMessageUpwards("WaterCollision");
        //        //other.transform.localScale = Vector3.Scale(other.transform.localScale, new Vector3(sizeDecrease, sizeDecrease, sizeDecrease));
        //        //Vector3.Scale(other.transform.localScale, new Vector3(sizeDecrease, sizeDecrease, sizeDecrease));
        //        Debug.Log(other.name);
        //    }
        //    i++;
        //}
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("--FIRECOLLISION2");
    //}


    //void OnParticleTrigger()
    //{
    //    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    //    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
    //    // get the particles which matched the trigger conditions this frame
    //    int numEnter = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //    int numExit = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

    //    // iterate through the particles which entered the trigger and make them red
    //    for (int i = 0; i < numEnter; i++)
    //    {
    //        ParticleSystem.Particle p = enter[i];

    //        p.size = 10f;
    //        enter[i] = p;
    //    }

    //    // iterate through the particles which exited the trigger and make them green
    //    for (int i = 0; i < numExit; i++)
    //    {
    //        ParticleSystem.Particle p = exit[i];
    //        p.size = 10f;
    //        exit[i] = p;
    //    }

    //    // re-assign the modified particles back into the particle system
    //    particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //    particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    //}

}
