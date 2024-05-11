using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] points;

    void Awake()
    {
        points = GetComponentsInChildren<Transform>()[1..];
    }

    public Vector3 GetNextPoint(Vector3 position)
    {
        var index = GetClosestIndex(position);
        index++;
        if (index >= points.Length) index = 0;

        return points[index].position;
    }


    public int GetClosestIndex(Vector3 position)
    {
        var minDistance = float.PositiveInfinity;
        var minIndex = 0;

        for (int i = 0; i < points.Length; i++)
        {
            var distance = Vector3.Distance(position, points[i].position);
            if (distance < minDistance)
            {
                minDistance = distance;
                minIndex = i;
            }
        }

        return minIndex;
    }



    public Vector3 GetClosestPoint(Vector3 position)
    {
        var minDistance = float.PositiveInfinity;
        Transform closestPoint = null;

        foreach (var point in points)
        {
            var distance = Vector3.Distance(position, point.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = point;
            }
        }

        return closestPoint.position;
    }
}