using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core;

public class LockConstructor : MonoBehaviour
{
    public Lock targetLock;
    public float lockSize;
    public float sectionSpacing;
    public float ringSpacing;

    public void ConstructLock(Lock target, Transform lockHolder)
    {
        targetLock = target;

        foreach (Transform child in lockHolder.Cast<Transform>().ToArray())
        {
            if (Application.isEditor)
                GameObject.DestroyImmediate(child.gameObject);
            else
                GameObject.Destroy(child.gameObject);
        }

        for (int ringIndex = 0; ringIndex < targetLock.rings.GetLength(0); ringIndex++)
        {
            Transform ring = CreateRing(ringIndex);
            ring.SetParent(lockHolder);
        }
    }

    public Transform CreateRing(int ringIndex)
    {
        GameObject ring = new GameObject();

        return ring.transform;
    }

    public Transform CreateSection(int ringIndex, int section)
    {
        return null;
    }

    public Vector3 GetSectionCenter(int ringIndex, int section)
    {
        int sections = targetLock.rings[0].numbers.GetLength(0);
        float distance = (1 + ringIndex) * GetRingSizeWithoutSpacing();
        Vector3 direction = Quaternion.Euler(0, 0, 360.0f * section / sections) * Vector3.up;

        return distance * direction;
    }

    public float GetRingSizeWithoutSpacing()
    {
        return lockSize / (2 * targetLock.rings.GetLength(0) + 1);
    }

    public float GetRingSize()
    {
        return (lockSize - 2 * targetLock.rings.GetLength(0) * ringSpacing) / (2 * targetLock.rings.GetLength(0) + 1);
    }
}
