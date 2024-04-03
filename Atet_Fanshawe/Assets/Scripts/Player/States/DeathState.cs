using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{

 public class DeathState : BaseState
 {
        public DeathState(PlayerController controller)
        {
            mPlayerController = controller;
        }

        public override void Start() 
        {


        }
        public override void Update() 
        {
            //play animation
            mPlayerController.Kill();
            //player health 

        }
        public override void Cleanup()
        { 


        }
 }


}
