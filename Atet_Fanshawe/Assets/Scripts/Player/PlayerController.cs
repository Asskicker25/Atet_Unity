using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        Dictionary<ePlayerState, BaseState> mListOfStates = new Dictionary<ePlayerState, BaseState>();
        [SerializeField] ePlayerState mCurrentState = ePlayerState.IDLE;


        public float mInput = 0;

        void Start()
        {
            AddStates();
        }

        void Update()
        {
            HandleInput();
            GetCurrentState().Update();
        }

        void AddStates()
        {
            mListOfStates.Add(ePlayerState.IDLE, new IdleState(this));
            mListOfStates.Add(ePlayerState.RUN, new RunState(this));
        }

        public void ChangeState(ePlayerState state)
        {
            mCurrentState = state;
        }

        public BaseState GetState(ePlayerState state)
        {
            return mListOfStates[state];
        }

        public BaseState GetCurrentState()
        {
            return mListOfStates[mCurrentState];
        }

        private void HandleInput()
        {
            mInput = Input.GetAxis("Horizontal");
        }


    }

}
