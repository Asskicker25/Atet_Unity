using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    }

    private void Reset()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            mListOfChildPlatform.Add(transform.GetChild(i).gameObject);
        }
    }
}
