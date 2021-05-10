using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class MyFrustrum : MonoBehaviour
{
    public Plane far;
    public Plane near;
    public Plane left;
    public Plane right;
    public Plane top;
    public Plane bot;
    private bool upCheck;
    private bool downCheck;
    private bool leftCheck;
    private bool rightCheck;
    private bool farCheck;
    private bool nearCheck;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject[] frustrumPoints;
    [SerializeField] public List<Vector3> points;
    [SerializeField] public ObjectRender[] objs;

    void Start()
    {
        objs = FindObjectsOfType<ObjectRender>();

        for (int i = 0; i < frustrumPoints.Length; i++)
        {
            frustrumPoints[i] = Instantiate(prefab, points[i], transform.rotation, transform);
            frustrumPoints[i].name = "Point" + i.ToString();
        }
        near = new Plane(frustrumPoints[0].transform.position, frustrumPoints[1].transform.position,
            frustrumPoints[2].transform.position);
        right = new Plane(frustrumPoints[5].transform.position, frustrumPoints[4].transform.position,
            frustrumPoints[3].transform.position);
        left = new Plane(frustrumPoints[6].transform.position, frustrumPoints[7].transform.position,
            frustrumPoints[2].transform.position);
        bot = new Plane(frustrumPoints[7].transform.position, frustrumPoints[4].transform.position,
            frustrumPoints[3].transform.position);
        top = new Plane(frustrumPoints[5].transform.position, frustrumPoints[6].transform.position,
            frustrumPoints[0].transform.position);
        far = new Plane(frustrumPoints[6].transform.position, frustrumPoints[5].transform.position,
            frustrumPoints[4].transform.position);

        right.Flip();
        near.Flip();
    }

    void Update()
    {
        //Update position
        near.Set3Points(frustrumPoints[0].transform.position, frustrumPoints[1].transform.position,
            frustrumPoints[2].transform.position);
        right.Set3Points(frustrumPoints[5].transform.position, frustrumPoints[4].transform.position,
            frustrumPoints[3].transform.position);
        left.Set3Points(frustrumPoints[6].transform.position, frustrumPoints[7].transform.position,
            frustrumPoints[2].transform.position);
        bot.Set3Points(frustrumPoints[7].transform.position, frustrumPoints[4].transform.position,
            frustrumPoints[3].transform.position);
        top.Set3Points(frustrumPoints[5].transform.position, frustrumPoints[6].transform.position,
            frustrumPoints[0].transform.position);
        far.Set3Points(frustrumPoints[6].transform.position, frustrumPoints[5].transform.position,
            frustrumPoints[4].transform.position);
        right.Flip();
        near.Flip();
        //CheckObjectsIsRender
        foreach (ObjectRender obj in objs)
        {
            upCheck = top.GetSide(obj.transform.position);
            downCheck = bot.GetSide(obj.transform.position);
            leftCheck = left.GetSide(obj.transform.position);
            rightCheck = right.GetSide(obj.transform.position);
            farCheck = far.GetSide(obj.transform.position);
            nearCheck = near.GetSide(obj.transform.position);
            if (upCheck && downCheck && leftCheck && rightCheck && farCheck && nearCheck)
                obj.ChangeRenderState(true);
            else
                obj.ChangeRenderState(false);
        }
    }
}
