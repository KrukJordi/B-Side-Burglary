using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourSplineSmooth : CameraBehaviourSpline
{
    [Header("Smooth")]

    public float smoothSpeed = 3;
    public float rotateSpeed = 3;


    public override void UpdateCamera(Transform camera, Vector3 point)
    {
        camera.position = Vector3.Lerp(camera.position, point, Time.fixedDeltaTime * smoothSpeed);

        Quaternion oldRotation = camera.rotation;
        camera.LookAt(objLookAt);
        camera.rotation = Quaternion.Lerp(oldRotation, camera.rotation, Time.fixedDeltaTime * rotateSpeed);
    }
}
