using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath; 

public class ObjectRender : MonoBehaviour
{
    public MeshRenderer mesh;
    private Vec3 pos;
    private void Awake()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
        pos = new Vec3(transform.position);
    }

    public Vec3 getRenderPos() { return pos; }

    public void ChangeRenderState(bool state)
    {
        mesh.enabled = state;
    }
}
