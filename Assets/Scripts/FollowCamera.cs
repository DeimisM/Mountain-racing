using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Vehicle vehicle;
    public float smoothness = 2f;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        cam.fieldOfView = Mathf.Lerp(60, 90, vehicle.speedRatio);

        transform.position = vehicle.transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, vehicle.transform.rotation, smoothness * Time.deltaTime);
    }
}
