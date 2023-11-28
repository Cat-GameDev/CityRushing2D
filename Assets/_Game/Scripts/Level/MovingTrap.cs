using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform[] movePoint;
    [SerializeField] CircleCollider2D circleCollider2D;
    [SerializeField] Transform model;

    int i;

    void Update()
    {
        ModelFollowMovePoint(movePoint);
        CollisionFollowTarget(model);
    }

    private void CollisionFollowTarget(Transform target)
    {
        circleCollider2D.offset = target.localPosition;
    }

    private void ModelFollowMovePoint(Transform[] movePoint)
    {
         model.position = Vector3.MoveTowards(model.position, movePoint[i].position, moveSpeed * Time.deltaTime);

        if(Vector2.Distance(model.position, movePoint[i].position) < 0.25f)
        {
            i++;

            if(i >= movePoint.Length)
                i = 0;
        }

        if(model.position.x > movePoint[i].position.x)
            model.Rotate(new Vector3(0f,0f,rotationSpeed * Time.deltaTime));
        else
            model.Rotate(new Vector3(0f,0f,-rotationSpeed * Time.deltaTime));
    }

}
