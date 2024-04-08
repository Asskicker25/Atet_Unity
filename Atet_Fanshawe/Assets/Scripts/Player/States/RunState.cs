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
            mPlayerController.mAnimator.CrossFade("Run", 0.1f);
        }

        public override void Update()
        {
            if (!HandleInput()) return;
            HandleMovement();
            HandleRotation();
        }

        public override void Cleanup()
        {
        }

        private bool HandleInput()
        {
            mPlayerController.mMoveDir = Input.GetAxisRaw("Horizontal");

            if (mPlayerController.mMoveDir == 0)
            {
                mPlayerController.ChangeState(ePlayerState.IDLE);
                return false;
            }

            return true;
        }

        void HandleMovement()
        {
            Vector3 mCameraRight = Camera.main.transform.right;

            Vector3 dir = mPlayerController.mCurrentAxis == ePlayerAxis.X ? new Vector3(mCameraRight.x, 0, 0) :
                 new Vector3(0, 0, mCameraRight.z);

            dir = Vector3.Normalize(dir);
            dir *= mPlayerController.mMoveDir * mPlayerController.moveSpeed;

            mPlayerController.rb.velocity = dir;

        }

        void HandleRotation()
        {
            mPlayerController.mPlayerFaceDir = mPlayerController.mMoveDir;
            mPlayerController.mPlayerFaceDir *= mPlayerController.mCameraController.mFlipCamera ? -1 : 1;


            float rotationY = mPlayerController.mCurrentAxis == ePlayerAxis.X ?
                mPlayerController.mPlayerFaceDir == 1 ? 89 : -89 :
                mPlayerController.mPlayerFaceDir == 1 ? 0 : 180;


            /*float newRotationY = MathUtilities::MathUtils::Lerp(mPlayerController->transform.rotation.y, rotationY, 
                Timer::GetInstance().deltaTime * mPlayerController->mRotLerpSpeed);*/

            Vector3 newRotation = new Vector3(0, -rotationY, 0);

            mPlayerController.transform.rotation = Quaternion.Euler(newRotation);

        }
    }

}
