using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{

    public class ObjectMoveState : BaseState
    {

        MovableObject mMovableObject;


        private enum ePushPullAnim
        {
            NONE = 0,
            PUSH = 1,
            PULL = 2
        };

        bool mIsLeft = false;
        ePushPullAnim mCurrentAnim = ePushPullAnim.NONE;

        string mPullAnim = "Pull";
        string mPushAnim = "Push";

        Vector3 mInitOffset = new Vector3(0, 0, 0);
        Vector3 mObjectOffset = new Vector3(0, 0, 0);



        public ObjectMoveState(PlayerController player)
        {
            mPlayerController = player;
            mMovableObject = mPlayerController.mCurrentMovableObject;

           

        }

        public override void Start()
        {

            mInitOffset = mMovableObject.transform.position - mPlayerController.transform.position;



            if (mPlayerController.mCurrentAxis == ePlayerAxis.X)
            {
                if (mInitOffset.x > 0)
                {
                    MoveToLeft();
                }
                else
                {
                    MoveToRight();
                }
            }
            else
            {
                if (mInitOffset.z > 0)
                {
                    MoveToRight();
                }
                else
                {
                    MoveToLeft();
                }
            }

            mObjectOffset = mPlayerController.transform.position - mMovableObject.transform.position;

            //mPlayerController->PlayAnimation("Push"); // Play Animation

        }

        public override void Update()
        {
            if (!HandleInput())
            {
                mPlayerController.velocity = new Vector3(0,0,0);
                //mPlayerController.mIsPlaying = false;
                return;
            }

            HandleMovement();

            HandleAnimation();

            mMovableObject.transform.position = mPlayerController.transform.position - mObjectOffset;

            //mPlayerController.mIsPlaying = true;
        }

        public override void Cleanup()
        {
            //mPlayerController.transform.position.x = mMovableObject.transform.position.x - mInitOffset.x;
            //mPlayerController.transform.position.z = mMovableObject.transform.position.z - mInitOffset.z;
            //mPlayerController.mIsPlaying = true;
            //mCurrentAnim = ePushPullAnim.NONE;

        }

        public void MoveToLeft()
        {
            Vector3 movePos = mMovableObject.GetLeftPosition();
            movePos.y = mPlayerController.transform.position.y;
            mPlayerController.transform.position = movePos;
            mIsLeft = true;
            HandleRotation();

        }
        public void MoveToRight()
        {
            Vector3 movePos = mMovableObject.GetRightPosition();
            movePos.y = mPlayerController.transform.position.y;
            mPlayerController.transform.position = movePos;
            mIsLeft = false;
            HandleRotation();
        }

        public bool HandleInput()
        {

            mPlayerController.mMoveDir = Input.GetAxis("Horizontal");

            if (mPlayerController.mMoveDir == 0)
            {
                return false;
            }

            return true;
        }
        public void HandleMovement()
        {
            Vector3 mCameraRight = GetMainCamera().transform.right;

            Vector3 dir = mPlayerController.mCurrentAxis == ePlayerAxis.X ? new Vector3(mCameraRight.x, 0, 0) :
               new Vector3(0, 0, mCameraRight.z);

            dir = Vector3.Normalize(dir);
            dir *= mPlayerController.mMoveDir;

            mPlayerController.velocity = dir
                 * 100.0f * Time.deltaTime;
        }
        public void HandleRotation()
        {
            mPlayerController.mPlayerFaceDir = mIsLeft ? 1 : -1;
            mPlayerController.mPlayerFaceDir *= mPlayerController.mCameraController.mFlipCamera ? -1 : 1;


            float rotationY = mPlayerController.mCurrentAxis == ePlayerAxis.X ?
                mPlayerController.mPlayerFaceDir == 1 ? 89 : -89 :
                mPlayerController.mPlayerFaceDir == 1 ? 0 : 180;

            //glm::vec3 newRotation = glm::vec3(0, rotationY, 0);
            //mPlayerController->transform.SetRotation(newRotation);


            Quaternion newRotation = new Quaternion(0, rotationY, 0,1);
            mPlayerController.transform.rotation = newRotation;

        }
        public void HandleAnimation()
        {
            //if (mIsLeft)
            //{
            //    if (mPlayerController->mMoveDir > 0)
            //    {
            //        if (mCurrentAnim == ePushPullAnim::PUSH) return;

            //        mPlayerController->PlayAnimation(mPushAnim);
            //        mCurrentAnim = ePushPullAnim::PUSH;
            //    }
            //    else
            //    {
            //        if (mCurrentAnim == ePushPullAnim::PULL) return;

            //        mPlayerController->PlayAnimation(mPullAnim);
            //        mCurrentAnim = ePushPullAnim::PULL;
            //    }

            //}
            //else
            //{
            //    if (mPlayerController->mMoveDir > 0)
            //    {
            //        if (mCurrentAnim == ePushPullAnim::PULL) return;
            //        mPlayerController->PlayAnimation(mPullAnim);
            //        mCurrentAnim = ePushPullAnim::PULL;
            //    }
            //    else
            //    {
            //        if (mCurrentAnim == ePushPullAnim::PUSH) return;

            //        mPlayerController->PlayAnimation(mPushAnim);
            //        mCurrentAnim = ePushPullAnim::PUSH;
            //    }
            //}

        }


        Camera GetMainCamera()
        {
            return Camera.main;

        }
    }
}