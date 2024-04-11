using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLight : MonoBehaviour
{
    public List<Transform> mListOfChilds;

    public LineRenderer lineRenderer;

    public int mCurrentIndexCount = 2;

    public Vector3[] lightPos;

    List<int> mListOfIndexes = new List<int>();

    private void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            mListOfChilds.Add(transform.GetChild(i));
        }

        lineRenderer = GetComponentInChildren<LineRenderer>();
        InitializeLine();

    }

    private void Start()
    {
        lightPos = new Vector3[mListOfChilds.Count];

        lineRenderer.GetPositions(lightPos);

        SetIndex(2);
    }

    void InitializeLine()
    {
        lineRenderer.positionCount = mListOfChilds.Count;

        for (int i = 0; i < mListOfChilds.Count; i++)
        {
            lineRenderer.SetPosition(i, mListOfChilds[i].position);
        }
    }

    public void SetIndex(int index)
    {
        mCurrentIndexCount = index;

        lineRenderer.positionCount = index;
        lineRenderer.SetPositions(lightPos);
    }

    public void AddIndex(int index)
    {
        mListOfIndexes.Add(index);
        UpdateSunlight();
    }

    public void RemoveIndex(int index)
    {
        mListOfIndexes.Remove(index);
        UpdateSunlight();
    }


    [ContextMenu("UpdateSunlight")]
    void EditorUpdate()
    {
        lineRenderer.positionCount = mListOfChilds.Count;

        for(int i = 0;i < mListOfChilds.Count;i++)
        {
            lineRenderer.SetPosition(i, mListOfChilds[i].position);
        }
    }

    void UpdateSunlight()
    {
        int mCurrentIndexCount = 2;

        mListOfIndexes.Sort();

        for (int i = 0; i < mListOfIndexes.Count; i++)
        {
            if (mListOfIndexes[i] == mCurrentIndexCount + 1)
            {
                mCurrentIndexCount = mListOfIndexes[i];
                continue;
            }
            break;
        }

        SetIndex(mCurrentIndexCount);
    }
}
