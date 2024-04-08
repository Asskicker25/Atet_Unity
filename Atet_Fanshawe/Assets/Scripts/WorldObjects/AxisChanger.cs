using Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisChanger : MonoBehaviour
{

    bool mUsed = false;
    public bool[] mCameraFlips = new bool[2] { false, false };
    public bool[] mPlayerFlips = new bool[2] { false, false };

    public PlayerController mPlayerController;

    public bool mAxisChanged = false;
    public bool mCanChangeAxis = false;
    public bool isInAxisChanger = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isInAxisChanger)
        {
            HandleInput();

        }

    }

    public bool GetCameraFlip()
    {
        return mUsed ? mCameraFlips[1] : mCameraFlips[0];
    }
    public bool GetPlayerFlip()
    {
        return mUsed ? mPlayerFlips[1] : mPlayerFlips[0];

    }
    public void SetUsed() 
    { 
        mUsed = !mUsed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isInAxisChanger = true;
            print("Player\n");
            if (mCanChangeAxis && !mAxisChanged)
            {
               
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInAxisChanger = false;
        }
    }

    public void HandleInput()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            mPlayerController.mCurrentAxisChanger = this.GetComponent<AxisChanger>();
            ChangeAxis();
            mAxisChanged = true;

            mCanChangeAxis = true;

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            mPlayerController.mCurrentAxisChanger = null;

            mCanChangeAxis = false;
            mAxisChanged = false;

        }


    }

    void ChangeAxis()
    {
        mPlayerController.ChangeState(ePlayerState.AXIS_CHANGE);
        print("Axis Change\n");

    }


}
