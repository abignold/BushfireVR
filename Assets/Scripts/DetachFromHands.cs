//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Handles the spawning and returning of the ItemPackage
//
//=============================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class DetachFromHands : MonoBehaviour
    {


        public bool requireGrabActionToTake = false;
        public bool showTriggerHint = false;
        public bool justPickedUpItem = false;
        public bool emptyBothHands = false;


        //-------------------------------------------------
        private void HandHoverUpdate(Hand hand)
        {

            if (requireGrabActionToTake)
            {
                GrabTypes startingGrab = hand.GetGrabStarting();

                if (startingGrab != GrabTypes.None)
                {
                    RemoveAllItemsFromHandStack(hand);

                    if (emptyBothHands)
                    {
                        RemoveAllItemsFromHandStack(hand.otherHand);
                    }
                }
            }
        }


        //-------------------------------------------------
        private void OnHandHoverEnd(Hand hand)
        {
            if (!justPickedUpItem && requireGrabActionToTake && showTriggerHint)
            {
                hand.HideGrabHint();
            }

            justPickedUpItem = false;
        }

        private void RemoveAllItemsFromHandStack(Hand hand)
        {
            if (hand == null)
                return;

            for (int i = 0; i < hand.AttachedObjects.Count; i++)
            {
                //ItemPackageReference packageReference = hand.AttachedObjects[i].attachedObject.GetComponent<ItemPackageReference>();
                hand.DetachObject(hand.AttachedObjects[i].attachedObject);
                
            }
        }
    }
}
