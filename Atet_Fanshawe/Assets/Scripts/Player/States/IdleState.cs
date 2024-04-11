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
            mPlayerController.mAnimator.CrossFade("Idle", 0.05f);
            mPlayerController.rb.velocity = Vector3.zero;
        }

        public override void Update() 
        {
            float input = Input.GetAxis("Horizontal");
            if(Mathf.Abs(input) > 0)
            {
                if(mPlayerController.mDead)
                {
                    return;
                }
                mPlayerController.ChangeState(ePlayerState.RUN);

            }
        }

        public override void Cleanup() 
        {
        }


    }

}
