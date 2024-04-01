using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class PlatformLoader : MonoBehaviour
{
    public List<GameObject> mListOfChildPlatform;

    private void Start()
    {
        foreach (var item in mListOfChildPlatform)
        {
            item.SetActive(false);
        }

        int randomIndex = Random.Range(0, mListOfChildPlatform.Count);
        mListOfChildPlatform[randomIndex].SetActive(true);
    }

    private void Reset()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            mListOfChildPlatform.Add(transform.GetChild(i).gameObject);
        }
    }
}
