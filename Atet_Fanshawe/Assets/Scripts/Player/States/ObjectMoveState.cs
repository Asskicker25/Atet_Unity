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
        }

        public override void Start()
        {

            mMovableObject = mPlayerController.mCurrentMovableObject;

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
                    MoveToLeft();

                }
                else
                {
                    MoveToRight();
                }
            }

            mObjectOffset = mPlayerController.transform.position - mMovableObject.transform.position;


            mPlayerController.mAnimator.CrossFade("Push", 0.1f);

        }

        public override void Update()
        {

            if (!HandleInput())
            {
                mPlayerController.velocity = new Vector3(0, 0, 0);
                mPlayerController.mAnimator.enabled = false;
                return;
            }

            mPlayerController.mAnimator.enabled = true;

            HandleMovement();
            HandleAnimation();
            mMovableObject.transform.position = mPlayerController.transform.position - mObjectOffset;
        }

        public override void Cleanup()
        {
            Vector3 pos = Vector3.zero;
            pos.x = mMovableObject.transform.position.x - mInitOffset.x;
            pos.z = mMovableObject.transform.position.z - mInitOffset.z;

            mPlayerController.transform.position = pos;
            mCurrentAnim = ePushPullAnim.NONE;
            mPlayerController.mAnimator.enabled = true;

            mPlayerController.mAnimator.CrossFade("Idle", 0.1f);
        }

        public void MoveToLeft()
        {
            Debug.Log("Player Is on Left");

            Vector3 movePos = mMovableObject.GetLeftPosition();
            movePos.y = mPlayerController.transform.position.y;
            mPlayerController.transform.position = movePos;
            mIsLeft = true;
            HandleRotation();

        }
        public void MoveToRight()
        {
            Debug.Log("Player Is on Right");

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

            mPlayerController.rb.velocity = dir
                 * 100.0f * Time.deltaTime * mPlayerController.pushSpeed;
        }
        public void HandleRotation()
        {
            mPlayerController.mPlayerFaceDir = mIsLeft ? 1 : -1;
            mPlayerController.mPlayerFaceDir *= mPlayerController.mCameraController.mFlipCamera ? -1 : 1;


            float rotationY = mPlayerController.mCurrentAxis == ePlayerAxis.X ?
                mPlayerController.mPlayerFaceDir == 1 ? -89 : 89 :
                mPlayerController.mPlayerFaceDir == 1 ? 0 : 180;

            //glm.vec3 newRotation = glm.vec3(0, rotationY, 0);
            //mPlayerController.transform.SetRotation(newRotation);

            Vector3 newRotation = new Vector3(0, rotationY, 0);
            mPlayerController.transform.rotation = Quaternion.Euler(newRotation);

        }
        public void HandleAnimation()
        {
            if (mIsLeft)
            {
                if (mPlayerController.mMoveDir > 0)
                {
                    if (mCurrentAnim == ePushPullAnim.PUSH) return;

                    mPlayerController.mAnimator.CrossFade(mPushAnim, 0.1f);
                    mCurrentAnim = ePushPullAnim.PUSH;
                }
                else
                {
                    if (mCurrentAnim == ePushPullAnim.PULL) return;

                    mPlayerController.mAnimator.CrossFade(mPullAnim, 0.1f);
                    mCurrentAnim = ePushPullAnim.PULL;
                }

            }
            else
            {
                if (mPlayerController.mMoveDir > 0)
                {
                    if (mCurrentAnim == ePushPullAnim.PULL) return;

                    mPlayerController.mAnimator.CrossFade(mPullAnim, 0.1f);
                    mCurrentAnim = ePushPullAnim.PULL;
                }
                else
                {
                    if (mCurrentAnim == ePushPullAnim.PUSH) return;

                    mPlayerController.mAnimator.CrossFade(mPushAnim, 0.1f);
                    mCurrentAnim = ePushPullAnim.PUSH;
                }
            }

        }


        Camera GetMainCamera()
        {
            return Camera.main;

        }
    }
}