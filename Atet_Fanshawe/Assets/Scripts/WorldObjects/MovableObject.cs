using Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{

    public float mPosOffset = 0.8f;
    public bool mMoveInputPressed = false;
    public bool mPlayerToMoveObject = false;
    public PlayerController mPlayerController;

    public int sunLightIndex = 2;
    public Transform targetPosition;

    public float targetReachedDistance = 0.5f;

    bool mTargetReached = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        ObjectMove();
        HandleTarget();
    }

   public Vector3 GetLeftPosition()
   {
        Debug.Log("Player Is on Left");
        return transform.position - (transform.right * mPosOffset);
    }
    public Vector3 GetRightPosition()
    {
        Debug.Log("Player Is on Right");
        return transform.position + (transform.right * mPosOffset);

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


        foreach (MovableObject obj in mPlayerController.mListOfMovableObjects)
        {
            if (IsPlayerNearObject(obj))
            {
                Debug.Log("Player Near Object");

                if (mMoveInputPressed)
                {
                    mPlayerController.mCurrentMovableObject = obj;
                    mPlayerToMoveObject = true;
                    mPlayerController.ChangeState(ePlayerState.OBJECT_MOVE);

                    return;
                }

            }

        }



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

    void HandleTarget()
    {
        Vector3 diff = targetPosition.position - transform.position;

        if(diff.sqrMagnitude < targetReachedDistance * targetReachedDistance)
        {
            if(!mTargetReached)
            {
                mTargetReached = true;
                mPlayerController.sunLight.AddIndex(sunLightIndex);
            }
        }
        else
        {
            if(mTargetReached)
            {
                mTargetReached = false;
                mPlayerController.sunLight.RemoveIndex(sunLightIndex);
            }
        }


    }

}
