using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{


    bool mSpikeUp = false;
    bool mSpikeDown = false;
    bool mIsSpikeUp = false;
    float mCurrentTimeStep = 0;

    static float SPIKE_UP_VALUE = 0.5f;
    static float SPIKE_DOWN_VALUE = 1.0f;

    static float SPIKE_UP_INTERVAL = 1.5f;
    static float SPIKE_DOWN_INTERVAL = 0.55f;

    static float SPIKE_UP_SPEED = 5.0f;
    static float SPIKE_DOWN_SPEED = 1.0f;


    void Start()
    {
        
    }

   
    void Update()
    {
        HandleSpike(Time.deltaTime);
        HandleSpikeUp(Time.deltaTime);
        HandleSpikeDown(Time.deltaTime);

    }

    void Initialize()
    {
        mIsPlaying = true;
        mControlTime = true;
        isPhysicsEnabled = false;
        mControlTimeParam = 1;

    }
    void SpikeUp()
    {
        mSpikeUp = true;
        mCurrentTimeStep = 0;
        mControlTimeParam = SPIKE_DOWN_VALUE;
        isPhysicsEnabled = true;
    }
    void SpikeDown()
    {
        mSpikeDown = true;
        mCurrentTimeStep = 0;
        mControlTimeParam = SPIKE_UP_VALUE;
    }

    void HandleSpikeUp(float deltaTime)
    {
        if (!mSpikeUp) return;

        mCurrentTimeStep += deltaTime * SPIKE_UP_SPEED;

        mControlTimeParam = MathUtils::Remap(mCurrentTimeStep, 0, 1, SPIKE_DOWN_VALUE, SPIKE_UP_VALUE);

        if (mCurrentTimeStep > 1)
        {
            mSpikeUp = false;
            mIsSpikeUp = true;
            mCurrentTimeStep = 0;
        }

    }
    void HandleSpikeDown(float deltaTime)
    {
        if (!mSpikeDown) return;

        mCurrentTimeStep += deltaTime * SPIKE_DOWN_SPEED;
        mControlTimeParam = MathUtils::Remap(mCurrentTimeStep, 0, 1, SPIKE_UP_VALUE, SPIKE_DOWN_VALUE);


        if (mCurrentTimeStep > 1)
        {
            mSpikeDown = false;
            mIsSpikeUp = false;
            isPhysicsEnabled = false;
            mCurrentTimeStep = 0;
        }
    }
    void HandleSpike(float deltaTime)
    {
        	if (mSpikeUp || mSpikeDown) return;

	mCurrentTimeStep += deltaTime;

	if (mCurrentTimeStep > (mIsSpikeUp ? SPIKE_DOWN_INTERVAL : SPIKE_UP_INTERVAL))
	{
		if (mIsSpikeUp)
		{
			SpikeDown();
		}
		else
		{
			SpikeUp();
		}
	}

    }

}
