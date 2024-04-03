using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisChanger : MonoBehaviour
{

    bool mUsed = false;
    public bool[] mCameraFlips;
    public bool[] mPlayerFlips;

    void Start()
    {
        mCameraFlips = new bool[2] { false, false };
        mPlayerFlips = new bool[2] { false, false };

    }

    // Update is called once per frame
    void Update()
    {
        
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

  
}
