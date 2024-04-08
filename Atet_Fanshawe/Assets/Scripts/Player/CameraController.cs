using Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool mFlipCamera = true;

    float mTimeStep = 0;
    float mDistance = 7;
    float mPosLerpSpeed = 8;
    float mRotLerpSpeed = 100;
    float mColumnWidth = 150;

    Vector3 mCameraPos;
    Vector3 mCameraDir;
    Vector3 mFollowOffset = new Vector3(0, 3.5f, 0);
    Vector3 mLookAtOffset = new Vector3(0, 1.3f, 0);
    Vector3 mCameraRight;

    public PlayerController mPlayer = null;

    void Start()
    {
        
    }

    void Update()
    {
        
        HandlePosition(Time.deltaTime);
        HandleRotation(Time.deltaTime);

        mCameraRight = GetMainCamera().transform.right;
    }



    Camera GetMainCamera()
    {
        return Camera.main;
    }

    void HandlePosition(float dt)
    {
        float dir = mPlayer.mPlayerFaceDir * (mFlipCamera ? -1 : 1);

        mCameraPos = mPlayer.transform.position + mPlayer.transform.right * mDistance * dir;
        mCameraPos += mFollowOffset;
        mCameraPos = Vector3.Lerp(GetMainCamera().transform.position, mCameraPos, dt * mPosLerpSpeed);

        GetMainCamera().transform.position = mCameraPos;

    }
    void HandleRotation(float dt)
    {
        float dir = 1;

        mCameraDir = (mPlayer.transform.position + mLookAtOffset) - GetMainCamera().transform.position;
        mCameraDir =Vector3.Normalize(mCameraDir);

        Quaternion rotationQuat = Quaternion.LookRotation(mCameraDir * dir, new Vector3(0, 1, 0));
        Quaternion rotation = Quaternion.Slerp(GetMainCamera().transform.rotation, rotationQuat,
           Mathf.Clamp(dt * mRotLerpSpeed, 0.0f, 1.0f));

        GetMainCamera().transform.rotation = rotation ;

    }
}
