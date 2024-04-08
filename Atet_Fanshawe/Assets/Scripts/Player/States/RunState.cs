using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            HandleMovement();
            HandleRotation();
        }

        public override void Cleanup()
        {
        }

        private void HandleInput()
        {
            mPlayerController.mMoveDir = Input.GetAxis("Horizontal");

            if (mPlayerController.mInput == 0)
            {
                mPlayerController.ChangeState(ePlayerState.IDLE);
            }
        }

        void HandleMovement()
        {
            Vector3 mCameraRight = Camera.main.transform.right;

            Vector3 dir = mPlayerController.mCurrentAxis == ePlayerAxis.X ? new Vector3(mCameraRight.x, 0, 0) :
                 new Vector3(0, 0, mCameraRight.z);

            dir = Vector3.Normalize(dir);
            dir *= mPlayerController.mMoveDir;

            mPlayerController.rb.velocity = dir;

        }

        void HandleRotation()
        {
            //mPlayerController.mPlayerFaceDir = mPlayerController.mMoveDir;
            //mPlayerController.mPlayerFaceDir *= mPlayerController.mCameraController ? -1 : 1;


            //float rotationY = mPlayerController.mCurrentAxis == ePlayerAxis.X ?
            //    mPlayerController.mPlayerFaceDir == 1 ? 89 : -89 :
            //    mPlayerController.mPlayerFaceDir == 1 ? 0 : 180;


            ///*float newRotationY = MathUtilities::MathUtils::Lerp(mPlayerController->transform.rotation.y, rotationY, 
            //    Timer::GetInstance().deltaTime * mPlayerController->mRotLerpSpeed);*/

            //Vector3 newRotation = new Vector3(0, rotationY, 0);

            //mPlayerController.transform.rotation = Quaternion.Euler(newRotation);

        }
    }

}
