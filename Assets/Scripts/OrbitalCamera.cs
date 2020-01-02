using UnityEngine;
using System.Collections;

public class OrbitalCamera : MonoBehaviour
{
    public float speed;
    public Transform Target;

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.RotateAround(Target.position, transform.up, -Input.GetAxis("Mouse X") * speed);
        }
    }
}