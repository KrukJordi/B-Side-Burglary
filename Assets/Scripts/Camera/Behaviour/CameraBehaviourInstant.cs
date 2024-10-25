using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourInstant : CameraBehaviour
{
    public override void Set(CameraController controller)
    {
        controller.transform.position = cameraObjData.position;
        controller.transform.rotation = cameraObjData.rotation;
    }


    void OnDrawGizmos()
    {
        if (cameraObjData != null)
        {
            Gizmos.color = gizmoColor;

            Gizmos.DrawLine(cameraObjData.position, cameraObjData.position + cameraObjData.forward * .5f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f + cameraObjData.right * .1f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f - cameraObjData.right * .1f);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (cameraObjData != null)
        {
            Gizmos.color = new Color(1, 0.4f, 0);

            Gizmos.DrawLine(cameraObjData.position, cameraObjData.position + cameraObjData.forward * .5f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f + cameraObjData.right * .1f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f - cameraObjData.right * .1f);
        }
    }
}
