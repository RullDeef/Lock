using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMover : MonoBehaviour
{
    public Transform lockTransform;
    public int ringIndex;

    public Transform[] layers;

    void Start()
    {
        int layersCount = lockTransform.childCount - 1;
        layers = new Transform[layersCount];

        for (int i = 0; i < layersCount; i++)
        {
            layers[i] = lockTransform.Find($"Layer{i}");
            if (layers[i] == null)
                Debug.LogError("Invalid lock structure!");
        }

        ringIndex = -1;
    }

    public bool IsHolding()
    {
        return ringIndex != -1;
    }

    public void HoldRing(int ringIndex)
    {
        if (this.ringIndex == -1)
            this.ringIndex = ringIndex;
        else
            Debug.LogError("Try to hold before dispose");
    }

    public void UpdateRingAngle(float deltaAngle)
    {
        if (ringIndex == -1)
            Debug.LogError("Invalid ring index");
        else
        {
            layers[ringIndex].Rotate(-180.0f / Mathf.PI * deltaAngle * Vector3.up);
        }
    }

    public void DisposeRing()
    {
        this.ringIndex = -1;
    }
}
