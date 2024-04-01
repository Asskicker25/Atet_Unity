using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    public class RunState : BaseState
    {
        public RunState(PlayerController controller)
        {
            mPlayerController = controller;
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

        private void HandleInput()
        {
            if (mPlayerController.mInput == 0)
            {
                mPlayerController.ChangeState(ePlayerState.IDLE);
            }
        }
    }

}
