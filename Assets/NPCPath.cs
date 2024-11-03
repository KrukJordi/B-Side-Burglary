using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class NPCPath : MonoBehaviour
{
    public Color gizmoColor = new Color(0, 0, 0, 0.8f);

    public Transform[] pathData;


    void Start()
    {
        if (pathData == null)
        {
            Debug.LogError("Path was not assigned at '" + gameObject + "'!");
        }
    }

    public Vector3 GetPosition(int index)
    {
        return pathData[index].position;
    }

    public Vector3 GetPoint(float point)
    {
        int index = Mathf.FloorToInt(point) % pathData.Length;
        int nextIndex = index + 1;

        if (nextIndex >= pathData.Length)
        {
            nextIndex = 0;
        }

        return Vector3.Lerp(pathData[index].position, pathData[nextIndex].position, point % 1);
    }

    /// <summary>
    /// Useful if you don't know how many points there are.
    /// Only from 0-1.
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public Vector3 GetPointUnclamped(float point)
    {
        float truePoint = point * pathData.Length;

        return GetPoint(truePoint);
    }

    public float GetPointDistance(int point)
    {
        int index = point % pathData.Length;
        int nextIndex = index + 1;

        if (nextIndex >= pathData.Length)
        {
            nextIndex = 0;
        }

        return (pathData[index].position - pathData[nextIndex].position).magnitude;
    }

    public int GetIndexFromPoint(float point)
    {
        return Mathf.FloorToInt(point) % pathData.Length;
    }


    void OnDrawGizmos()
    {
        if (pathData != null)
        {
            Gizmos.color = gizmoColor;

            Vector3 lastPoint = pathData[0].position;
            for (int i = 0; i < pathData.Length; ++i)
            {
                if (pathData[i] == null) return;

                Vector3 pos = pathData[i].position;

                Gizmos.DrawLine(lastPoint, pos);

                Gizmos.DrawLine(pos + Vector3.down * .1f, pos + Vector3.up * .1f);
                Gizmos.DrawLine(pos + Vector3.left * .1f, pos + Vector3.right * .1f);
                Gizmos.DrawLine(pos + Vector3.back * .1f, pos + Vector3.forward * .1f);

                lastPoint = pos;
            }

            Vector3 posLast = pathData[0].position;

            Gizmos.DrawLine(lastPoint, posLast);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (pathData != null)
        {
            Gizmos.color = new Color(1, 0.4f, 0);

            Vector3 lastPoint = pathData[0].position;
            for (int i = 0; i < pathData.Length; ++i)
            {
                if (pathData[i] == null) return;

                Vector3 pos = pathData[i].position;

                Gizmos.DrawLine(lastPoint, pos);

                Gizmos.DrawLine(pos + Vector3.down * .1f, pos + Vector3.up * .1f);
                Gizmos.DrawLine(pos + Vector3.left * .1f, pos + Vector3.right * .1f);
                Gizmos.DrawLine(pos + Vector3.back * .1f, pos + Vector3.forward * .1f);

                lastPoint = pos;
            }

            Vector3 posLast = pathData[0].position;

            Gizmos.DrawLine(lastPoint, posLast);
        }
    }
}
