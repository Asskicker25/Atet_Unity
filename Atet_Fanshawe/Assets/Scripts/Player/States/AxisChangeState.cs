using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    public class AxisChangeState : BaseState
    {
        AxisChanger currentAxisChange;
        public AxisChangeState(PlayerController player)
        {
            mPlayerController = player;
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {

            ePlayerAxis currentAxis = mPlayerController.mCurrentAxis;

            currentAxisChange = mPlayerController.mCurrentAxisChanger;

            mPlayerController.ChangeAxis(currentAxis == ePlayerAxis.X ?
                ePlayerAxis.Z : ePlayerAxis.X);

            mPlayerController.mCameraController.mFlipCamera = currentAxisChange.GetCameraFlip();
            mPlayerController.mPlayerFaceDir = currentAxisChange.GetPlayerFlip() ? 1 : -1;
            mPlayerController.mPlayerFaceDir *= mPlayerController.mCameraController.mFlipCamera ? -1 : 1;


            float rotationY = mPlayerController.mCurrentAxis == ePlayerAxis.X ?
                mPlayerController.mPlayerFaceDir == 1 ? 90 : -90 :
                mPlayerController.mPlayerFaceDir == 1 ? 0 : 180;

            //mPlayerController.transform.SetRotation(glm::vec3(0, rotationY, 0));
            mPlayerController.transform.Rotate(new Vector3(0, rotationY, 0));

            currentAxisChange.SetUsed();

            mPlayerController.ChangeState(ePlayerState.IDLE);

        }

        public override void Cleanup()
        {

        }
    }
}