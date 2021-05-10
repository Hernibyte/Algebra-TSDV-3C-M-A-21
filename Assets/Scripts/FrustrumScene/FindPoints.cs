using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPoints : MonoBehaviour
{
    [SerializeField] GameObject reference;
    private LineRenderer line;
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        reference = FindObjectOfType<MyFrustrum>().gameObject;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, reference.transform.position);
    }
}
