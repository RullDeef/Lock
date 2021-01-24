using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class GameManager : MonoBehaviour
{
    public new Camera camera;
    private Plane plane;

    public Lock targetLock;
    public Transform lockHolder;

    private LockConstructor lockConstructor;
    private LockMover lockMover;

    private Vector3 lastMousePosition;

    private void Start()
    {
        plane = new Plane(Vector3.up, 0);

        lockConstructor = GetComponent<LockConstructor>();
        lockMover = GetComponent<LockMover>();

        GenerateLock(1);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            lockMover.HoldRing(0);
        else if (Input.GetMouseButtonUp(0))
            lockMover.DisposeRing();

        if (lockMover.IsHolding())
        {
            Vector3 currMousePosition = RaycastMousePosition(Input.mousePosition);
            float lastAngle = Mathf.Atan2(lastMousePosition.z, lastMousePosition.x);
            float currAngle = Mathf.Atan2(currMousePosition.z, currMousePosition.x);

            float deltaAngle = currAngle - lastAngle;
            lockMover.UpdateRingAngle(deltaAngle);
        }

        lastMousePosition = RaycastMousePosition(Input.mousePosition);
    }

    private void GenerateLock(int level)
    {
        targetLock = new Lock(level);
        lockConstructor.ConstructLock(targetLock, lockHolder);
    }

    private Vector3 RaycastMousePosition(Vector3 position)
    {
        Ray ray = camera.ScreenPointToRay(position);
        Vector3 hitPoint = Vector3.zero;

        float enter = 0.0f;
        if (plane.Raycast(ray, out enter))
            hitPoint = ray.GetPoint(enter);

        return hitPoint;
    }
}
