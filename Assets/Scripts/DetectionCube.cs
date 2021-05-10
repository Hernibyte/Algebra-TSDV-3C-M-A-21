using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class DetectionCube : MonoBehaviour
{
    [SerializeField] GameObject Cube;
    [SerializeField] GameObject Cube2;

    public Plane up;
    public Plane down;
    public Plane left;
    public Plane right;
    public Plane front;
    public Plane back;

    void Start()
    {
        Vec3 point1 = new Vec3(-5, -5, -5);
        Vec3 point2 = new Vec3(5, -5, -5);
        Vec3 point3 = new Vec3(5, 5, -5);
        Vec3 point4 = new Vec3(-5, 5, -5);
        Vec3 point5 = new Vec3(-5, -5, 5);
        Vec3 point6 = new Vec3(5, -5, 5);
        Vec3 point7 = new Vec3(5, 5, 5);
        Vec3 point8 = new Vec3(-5, 5, 5);

        front = new Plane(point5, point7);
        back = new Plane(point1, point3);
        up = new Plane(point4, point7);
        down = new Plane(point1, point6);
        left = new Plane(point1, point8);
        right = new Plane(point2, point7);
    }

    void Update()
    {
        if (up.GetSide(Cube.transform.position) == down.GetSide(Cube.transform.position) ==
            left.GetSide(Cube.transform.position) == right.GetSide(Cube.transform.position) ==
            back.GetSide(Cube.transform.position) == front.GetSide(Cube.transform.position))
        {
            Debug.Log("El cubo01 esta adentro");
        } else
            Debug.Log("El cubo esta fuera");

        if(up.GetSide(Cube2.transform.position) == down.GetSide(Cube2.transform.position) ==
            left.GetSide(Cube2.transform.position) == right.GetSide(Cube2.transform.position) ==
            back.GetSide(Cube2.transform.position) == front.GetSide(Cube2.transform.position))
        {
            Debug.Log("El cubo02 esta adentro");
        } else
            Debug.Log("El cubo02 esta fuera");
    }
}
