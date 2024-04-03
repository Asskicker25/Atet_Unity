using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{

    public float mPosOffset = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public Vector3 GetLeftPosition()
   {
        return transform.position - (transform.right * mPosOffset);
    }
    public Vector3 GetRightPosition()
    {
        return transform.position + (transform.right * mPosOffset);

    }
}
