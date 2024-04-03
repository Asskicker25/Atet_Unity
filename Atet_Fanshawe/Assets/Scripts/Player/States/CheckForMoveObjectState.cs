using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Player
{
    public class CheckForMoveObjectState : BaseState
    {

        bool mMoveInputPressed = false;
        bool mPlayerToMoveObject = false;

        public CheckForMoveObjectState(PlayerController player)
        {   
            mPlayerController = player;
        }

        public override void Start()
        {

        }

        public override void Update()
        {
            ObjectMove();
            HandleInput();
        }

        public override void Cleanup()
        {

        }


        private bool IsPlayerNearObject(MovableObject obj)
        {
             Vector3 diff = obj.transform.position - mPlayerController.transform.position;
             float sqDist = Vector3.Dot(diff, diff);

            if (sqDist < mPlayerController.mInteractDistance * mPlayerController.mInteractDistance)
            {
                return true;
            }

            return false;

        }

        public void ObjectMove()
        {
            if (mPlayerToMoveObject) return;


            foreach(MovableObject obj in mPlayerController.mListOfMovableObjects)
            {
                if (IsPlayerNearObject(obj))
                {
                    if (mMoveInputPressed)
                    {
                        mPlayerController.mCurrentMovableObject = obj;
                        mPlayerController.ChangeState(ePlayerState.OBJECT_MOVE);
                        mPlayerToMoveObject = true;

                        return;
                    }

                }

            }

           

            mMoveInputPressed = false;
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                mMoveInputPressed = true;

            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                mMoveInputPressed = false;
                mPlayerToMoveObject = false;

                if (mPlayerController.mCurrentState == ePlayerState.OBJECT_MOVE)
                {
                    mPlayerController.ChangeState(ePlayerState.IDLE);
                }

            }
        }

    }
}