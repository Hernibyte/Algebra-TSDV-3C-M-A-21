using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using CustomQuaternion;
using MathDebugger;

public enum Ejercicio{
    uno,
    dos,
    tres
}

public class EjerciciosQuaternion : MonoBehaviour
{
    public Ejercicio ejercicio;
    public float angle;
    Vec3 ejer1;
    List<Vector3> ejer2 = new List<Vector3>();
    List<Vector3> ejer3 = new List<Vector3>();

    void Awake(){
        ejer1 = new Vec3(10, 0, 0);

        ejer2.Add(new Vec3(10, 0, 0));
        ejer2.Add(new Vec3(10, 10, 0));
        ejer2.Add(new Vec3(20, 10, 0));

        ejer3.Add(new Vec3(10, 0, 0));
        ejer3.Add(new Vec3(10, 10, 0));
        ejer3.Add(new Vec3(20, 10, 0));
        ejer3.Add(new Vec3(20, 20, 0));
    }

    void Start()
    {
        Vector3Debugger.AddVector(ejer1, Color.red, "ejer1");
        Vector3Debugger.AddVectorsSecuence(ejer2, false, Color.red, "ejer2");
        Vector3Debugger.AddVectorsSecuence(ejer3, false, Color.red, "ejer3");
    }

    void FixedUpdate()
    {
        switch(ejercicio){
            case Ejercicio.uno:

                SwitchEnableEditorView("ejer1");
                ejer1 = MyQuaternion.Euler(new Vec3(0, angle, 0)) * ejer1;
                Vector3Debugger.UpdatePosition("ejer1", ejer1);

            break;
            case Ejercicio.dos:

                SwitchEnableEditorView("ejer2");
                for(int i = 0; i < ejer2.Count; i++){
                    ejer2[i] = MyQuaternion.Euler(new Vec3(0, angle, 0)) * new Vec3(ejer2[i]);
                }
                Vector3Debugger.UpdatePositionsSecuence("ejer2", ejer2);

            break;
            case Ejercicio.tres:

                SwitchEnableEditorView("ejer3");
                for(int i = 0; i < ejer3.Count; i++){
                    ejer3[1] = MyQuaternion.Euler(new Vec3(angle, angle, 0f)) * new Vec3(ejer3[1]);
                    ejer3[3] = MyQuaternion.Euler(new Vec3(-angle, -angle, 0f)) * new Vec3(ejer3[3]);
                }
                Vector3Debugger.UpdatePositionsSecuence("ejer3", ejer3);

            break;
        }
    }

    void SwitchEnableEditorView(string _string){
        if(_string == "ejer1")
            Vector3Debugger.EnableEditorView("ejer1");
        else
            Vector3Debugger.DisableEditorView("ejer1");

        if(_string == "ejer2")
            Vector3Debugger.EnableEditorView("ejer2");
        else
            Vector3Debugger.DisableEditorView("ejer2");

        if(_string == "ejer3")
            Vector3Debugger.EnableEditorView("ejer3");
        else
            Vector3Debugger.DisableEditorView("ejer3");
    }
}
