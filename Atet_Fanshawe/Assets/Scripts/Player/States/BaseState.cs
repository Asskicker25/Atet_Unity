using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    public class BaseState 
    {

        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void Cleanup() { }

        protected PlayerController mPlayerController;
    }

}
