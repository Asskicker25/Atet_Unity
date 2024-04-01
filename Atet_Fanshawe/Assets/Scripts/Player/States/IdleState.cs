using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    public class IdleState : BaseState
    {
        public IdleState(PlayerController controller)
        {
            mPlayerController = controller;
        }

        public override void Start() 
        { 
        }

        public override void Update() 
        {
            if(mPlayerController.mInput > 0)
            {
                mPlayerController.ChangeState(ePlayerState.RUN);
            }
        }

        public override void Cleanup() 
        {
        }


    }

}
