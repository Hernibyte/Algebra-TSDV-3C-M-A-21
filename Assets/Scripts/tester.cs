using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EjerciciosAlgebra;
using CustomMath;

public class tester : MonoBehaviour
{
    float t = 0.0f;
    public enum activity
    {
        Uno,
        Dos,
        Tres,
        Cuatro,
        Cinco,
        Seis,
        Siete,
        Ocho,
        Nueve,
        Diez
    };
    public activity ejercicio;

    public Color color;
    public Vec3 Apos = new Vec3(0 , 0, 0);
    public Vec3 Bpos = new Vec3(0, 0, 0);
    private Vec3 A;
    private Vec3 B;
    private Vec3 Result;

    void Start()
    {
        A = new Vec3(0 , 0, 0);
        B = new Vec3(0 , 0, 0);
        Result = new Vec3(0, 0, 0);
        VectorDebugger.AddVector(A, color, "A");
        VectorDebugger.AddVector(B, color, "B");
        VectorDebugger.AddVector(Result, Color.red, "Result");
        VectorDebugger.EnableCoordinates("A");
        VectorDebugger.EnableCoordinates("B");
        VectorDebugger.EnableCoordinates("Result");
    }


    void Update()
    {
        A.x = Apos.x; A.y = Apos.y; A.z = Apos.z;
        B.x = Bpos.x; B.y = Bpos.y; B.z = Bpos.z;
        VectorDebugger.UpdatePosition("A", A);
        VectorDebugger.UpdatePosition("B", B);
        switch (ejercicio)
        {
            case activity.Uno:
                VectorDebugger.UpdatePosition("Result", A + B);
                break;
            case activity.Dos:
                VectorDebugger.UpdatePosition("Result", A - B);
                break;
            case activity.Tres:
                VectorDebugger.UpdatePosition("Result", CustomMath.Vec3.Max(A, B));
                break;
            case activity.Cuatro:
                VectorDebugger.UpdatePosition("Result", CustomMath.Vec3.Cross(A, B));
                break;
            case activity.Cinco:
                VectorDebugger.UpdatePosition("Result", CustomMath.Vec3.Lerp(A, B, t));
                t += Time.deltaTime;
                if (t >= 1) t = 0;
                break;
            case activity.Seis:
                VectorDebugger.UpdatePosition("Result", CustomMath.Vec3.Max(A, B));
                break;
            case activity.Siete:
                VectorDebugger.UpdatePosition("Result", CustomMath.Vec3.Project(A, B));
                break;
            case activity.Ocho:
                float num = CustomMath.Vec3.Distance(A, B);
			    CustomMath.Vec3 val = A + B;
			    VectorDebugger.UpdatePosition("Result", num * ((CustomMath.Vec3)(val)).normalized);
                break;
            case activity.Nueve:
                VectorDebugger.UpdatePosition("Result", CustomMath.Vec3.Reflect(A, B));
                break;
            case activity.Diez:
                VectorDebugger.UpdatePosition("Result", CustomMath.Vec3.LerpUnclamped(A, B, t));
                t += Time.deltaTime;
                if(t >= 10) t = 0;
                break;
        }
    }
}
