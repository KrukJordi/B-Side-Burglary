using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviourPath : MonoBehaviour
{
    [Header("Settings")]

    public NPCPath path;

    public float interval;
    public float speed;

    public float turnSpeed = 3;


    [Header("Values")]

    public int index;
    int indexNext;
    float point;

    float waiting;
    float pointSpeed;

    Quaternion lookRotation;


    void Start()
    {
        point = index;
        indexNext = (index + 1) % path.pathData.Length;

        pointSpeed = 1 / path.GetPointDistance(index);
    }

    //Could (and should) use fixedUpdate, but its a prototype so it looks better and I likely wont re-use, so eh.
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

        waiting -= Time.deltaTime;

        if (waiting > 0)
        {
            return;
        }

        point += Time.deltaTime * pointSpeed * speed;

        transform.position = path.GetPoint(point);

        if (point - index > 1)
        {
            Turn();

            transform.position = path.GetPosition(index);
        }
    }

    void Turn()
    {
        index = path.GetIndexFromPoint(point);
        indexNext = path.GetIndexFromPoint(point + 1);
        point %= path.pathData.Length;

        pointSpeed = 1 / path.GetPointDistance(index);

        Quaternion oldRotation = transform.rotation;
        transform.LookAt(path.GetPosition(indexNext));
        lookRotation = transform.rotation;
        transform.rotation = oldRotation;

        waiting = interval;
    }
}
