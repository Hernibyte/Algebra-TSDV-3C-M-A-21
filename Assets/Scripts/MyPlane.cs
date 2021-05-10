using System;
using CustomMath;

namespace SystemPlane
{
    [Serializable]
    public struct MyPlane{
        public Vec3 normal { get; set; }
        public float distance { get; set; }
        public MyPlane flipped
        {
            get
            {
                return new MyPlane(-normal, -normal * distance);
            }
        }
        
        public MyPlane(Vec3 inNormal, Vec3 inPoint)
        {
            normal = inNormal;
            distance = -Vec3.Dot(normal, inPoint);     
        }
        public MyPlane(Vec3 a, Vec3 b, Vec3 c)
        {
            normal = Vec3.Cross(b - a, c - a).normalized;
            distance = -Vec3.Dot(normal, a);
        }
        public void SetNormalAndPoint(Vec3 inNormal, Vec3 inPoint)
        {
            normal = inNormal;
            distance = -Vec3.Dot(normal, inPoint);
        }
        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            normal = Vec3.Cross(b - a, c -a).normalized;
            distance = -Vec3.Dot(normal, a);
        }
                public void Flip()
        {
            normal = -normal;
            distance = -distance;
        }
        public void Translate(Vec3 translation)
        {
            distance += Vec3.Dot(normal, translation);
        }
                public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            return (point - normal * GetDistanceToThePoint(point));
        }
        public float GetDistanceToThePoint(Vec3 point)
        {
            return Vec3.Dot(normal,point) + distance;
        }
        public bool GetSide(Vec3 point)
        {
            return GetDistanceToThePoint(point) > 0;
        }
        public bool SameSide(Vec3 point1, Vec3 point2)
        {
            return GetDistanceToThePoint(point1) > 0f && GetDistanceToThePoint(point2) > 0f ||
            GetDistanceToThePoint(point1) <= 0f && GetDistanceToThePoint(point2) <= 0f;
        }
    }
}