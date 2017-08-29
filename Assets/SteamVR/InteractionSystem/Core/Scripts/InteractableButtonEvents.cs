//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Sends simple controller button events to UnityEvents
//
//=============================================================================

using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class InteractableButtonEvents : MonoBehaviour
	{
        public UnityEvent onTriggerHold;
		public UnityEvent onTriggerDown;
		public UnityEvent onTriggerUp;
		public UnityEvent onGripDown;
		public UnityEvent onGripUp;
		public UnityEvent onTouchpadDown;
		public UnityEvent onTouchpadUp;
		public UnityEvent onTouchpadTouch;
		public UnityEvent onTouchpadRelease;
        public UnityEvent onMenuPress;
        public UnityEvent onLeftMenu;
        public UnityEvent onRightMenu;
        //-------------------------------------------------
        void Update()
		{
            if (Player.instance.leftHand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
            {
                onLeftMenu.Invoke();
                ControllerButtonHints.HideAllTextHints(Player.instance.leftHand);

            }
            else if (Player.instance.rightHand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
            {
                onRightMenu.Invoke();
                ControllerButtonHints.HideAllTextHints(Player.instance.rightHand);
            }

            for ( int i = 0; i < Player.instance.handCount; i++ )
			{
				Hand hand = Player.instance.GetHand( i );

                if ( hand.controller != null )
				{                    
                    if (hand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).magnitude > 0f)
                    {
                        onTriggerHold.Invoke();
                    }

                    if ( hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger ) )
					{
						onTriggerDown.Invoke();
					}

					if ( hand.controller.GetPressUp( Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger ) )
					{
						onTriggerUp.Invoke();
					}

					if ( hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) )
					{
						onGripDown.Invoke();
                        ControllerButtonHints.HideButtonHint(hand, EVRButtonId.k_EButton_Grip);
                    }

					if ( hand.controller.GetPressUp( Valve.VR.EVRButtonId.k_EButton_Grip ) )
					{
						onGripUp.Invoke();
					}

					if ( hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ) )
					{
						onTouchpadDown.Invoke();
					}

					if ( hand.controller.GetPressUp( Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ) )
					{
						onTouchpadUp.Invoke();
					}

					if ( hand.controller.GetTouchDown( Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ) )
					{
						onTouchpadTouch.Invoke();
					}

					if ( hand.controller.GetTouchUp( Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ) )
					{
						onTouchpadRelease.Invoke();
					}

                    if (hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
                    {
                        onMenuPress.Invoke();
                    }
				}
			}

		}
	}
}
