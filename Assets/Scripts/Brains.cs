using UnityEngine;

public class Brains : MonoBehaviour
{
    Vehicle vehicle;
    Path path;
    Vector3 targetPos;

    void Start()
    {
        vehicle = GetComponent<Vehicle>();
        path = FindObjectOfType<Path>();

        transform.position = path.GetClosestPoint(transform.position);
        targetPos = path.GetNextPoint(transform.position);
    }

    void Update()
    {
        Debug.DrawLine(transform.position, targetPos, Color.red);

        if(Vector3.Distance(transform.position, targetPos) < 3)
        {
            targetPos = path.GetNextPoint(transform.position);
        }

        var angle = Vector3.SignedAngle(transform.forward, targetPos - transform.position, Vector3.up);

        vehicle.Steer(angle);
        //vehicle.Steer(Mathf.PerlinNoise1D(Time.time) * 2 - 1);
        vehicle.Accelerate();
    }
}