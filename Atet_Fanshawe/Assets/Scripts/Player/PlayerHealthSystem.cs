using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{



    public string mPlayerId;
    public int mPlayerHealthCount;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Intialize(string playerId)
    {
        SetPlayerId(playerId);

        int healthCount = GetPlayerHealthCount();

        if (healthCount == -1)
        {
            SetPlayerHealth(5);
        }
        else
        {
            mPlayerHealthCount = healthCount;
        }
    }

    public void SetPlayerId(string playerId)
    {
        mPlayerId = playerId;
    }

    public void SetPlayerHealth(int lives)
    {
        mPlayerHealthCount = lives;

    }
    public void ReduceLivesCountby(int lives = 1)
    {
        mPlayerHealthCount -= lives;
    }

    public  string GetPlayerId()
    { 
        return mPlayerId;
    }
       

    public int GetPlayerHealthCount()
    {
        return mPlayerHealthCount;

    }



  
}
