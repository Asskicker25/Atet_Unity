using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        Dictionary<ePlayerState, BaseState> mListOfStates = new Dictionary<ePlayerState, BaseState>();
        [SerializeField] PlayerHealthSystem healthSystem;

        public float moveSpeed = 1.0f;
        public PlayerData mPlayerData;
        public CameraController mCameraController;
        public MovableObject mCurrentMovableObject;
        public AxisChanger mCurrentAxisChanger;
        public Rigidbody rb;
        public bool mDead = false;
        public float mMoveDir = 0;       
        public float mPlayerFaceDir = 1;
        public float mRotLerpSpeed = 10;
        public float mInteractDistance = 2.3f;
        public Vector3 velocity = new Vector3(0,0,0);
        public ePlayerState mCurrentState = ePlayerState.IDLE;
        public ePlayerAxis mCurrentAxis = ePlayerAxis.X;
        public List<MovableObject> mListOfMovableObjects;

        public Animator mAnimator;

        private int mCurrentAxisInt = 0;

        void Start()
        {
            AddStates();
            healthSystem.Intialize("Player");
            GetCurrentState().Start();
        }

        void Update()
        {
            GetCurrentState().Update();
        }

        void AddStates()
        {
            mListOfStates.Add(ePlayerState.IDLE, new IdleState(this));
            mListOfStates.Add(ePlayerState.RUN, new RunState(this));
            mListOfStates.Add(ePlayerState.AXIS_CHANGE, new AxisChangeState(this));
            mListOfStates.Add(ePlayerState.DEATH, new DeathState(this));
            mListOfStates.Add(ePlayerState.OBJECT_MOVE, new ObjectMoveState(this));
        }

        public void ChangeState(ePlayerState state)
        {
            GetCurrentState().Cleanup();
            mCurrentState = state;
            GetCurrentState().Start();
        }

        public BaseState GetState(ePlayerState state)
        {
            return mListOfStates[state];
        }

        public BaseState GetCurrentState()
        {
            return mListOfStates[mCurrentState];
        }

        public void Kill()
        {
            mDead = true;
            //disable physics
            //play sound
            Debug.Log("Player Dead");
        }

        public void ChangeAxis(ePlayerAxis axis)
        {
            mCurrentAxis = axis;
            mCurrentAxisInt = (int)mCurrentAxis;
        }

    }

}
