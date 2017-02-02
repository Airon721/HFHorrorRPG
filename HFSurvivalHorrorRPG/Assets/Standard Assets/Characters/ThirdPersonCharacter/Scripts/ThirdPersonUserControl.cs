using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
		public bool leftMouseClick=false;
		public bool rightMouseClick=false;
		public float leftMouseClicks;

        
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }





			leftMouseClick = Input.GetMouseButtonDown(0);

			if(leftMouseClick){
				StartCoroutine("TimerClickTime");
				//animator.SetBool("LeftMouseClick", leftMouseClick);
				//animator.SetBool("RightMouse", rightMouseClick);
				//m_Character.Attack(leftMouseClick);
				m_Character.leftMouseClick = true;
				//m_Character.leftMouseClicks = 1;
				FightCombo();

			}
	
        }

		void FightCombo(){   //every left mouse click +1 to animation number counter
			
			leftMouseClicks += 1f;
			//animator.SetFloat("LeftMouseClicks", leftMouseClicks);

			if(leftMouseClicks>2f){
				leftMouseClicks = 1f;
			}

			print (leftMouseClicks);

			m_Character.leftMouseClicks = leftMouseClicks;

		}	

		IEnumerator TimerClickTime(){  //timer, few seconds after click mouse bool leftMouseClick = true

			yield return new WaitForSeconds(0.1f);
			leftMouseClick=false;
			m_Character.leftMouseClick = false;
			m_Character.leftMouseClicks = 0;
			yield return null;

		}

		IEnumerator InAction(){ //recieve message from fight animation in mecanim controller

			yield return null;
		}

		IEnumerator AnimationEnd(){//recieve message from fight animation in mecanim controller

			yield return null;
		}








        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);


            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
				m_Move = (v*m_CamForward + h*m_Cam.right) * 0.5f;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move /= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
