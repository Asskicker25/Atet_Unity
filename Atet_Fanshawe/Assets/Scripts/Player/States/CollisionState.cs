using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Scripts.Player
{

    public class CollisionState : BaseState
    {

        bool mAxisChanged = false;
        bool mCanChangeAxis = false;

        public CollisionState(PlayerController player)
        {
            mPlayerController = player;
        }

        public override void Start()
       {

       }
       public override void Update()
       {
            HandleInput();
       }

       public override void Cleanup()
       {
       
       }

       
        public void HandleInput()
        {

            if(Input.GetKeyDown(KeyCode.Space))
            {
                mCanChangeAxis = true;

            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                mCanChangeAxis = false;
                mAxisChanged = false;

            }


        }


       


    }


}