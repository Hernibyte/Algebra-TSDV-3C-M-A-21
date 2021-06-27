using System.Collections;
using System.Collections.Generic;
using UnityEngine.Internal;
using UnityEngine;
using CustomMath;
using System;

namespace CustomQuaternion{
    public struct MyQuaternion : IEquatable<MyQuaternion> {
        const float radToDeg = (float)(180.0 / Math.PI);
        const float degToRad = (float)(Math.PI / 180.0);
        public const float kEpsilon = 1E-06f;

        public float x;
        public float y;
        public float z;
        public float w;

        public float this[int index] {
            get {
                switch (index) {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    case 2:
                        return this.z;
                    case 3:
                        return this.w;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index: " + index + ", can use only 0,1,2,3");
                }
            }
            set {
                switch (index) {
                    case 0:
                        this.x = value;
                    break;
                    case 1:
                        this.y = value;
                    break;
                    case 2:
                        this.z = value;
                    break;
                    case 3:
                        this.w = value;
                    break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index: " + index + ", can use only 0,1,2,3");
                }
            }
        }

        public static MyQuaternion identity{
            get{
                return new MyQuaternion(0f, 0f, 0f, 1f);
            }
        }

        public Quaternion convert{
            get{
                return new Quaternion(x, y, z, w);
            }
        }

        public MyQuaternion normalized{
            get{
                float mag = (float)Math.Sqrt(x * x + y * y + z * z + w * w);
                MyQuaternion quaternion = new MyQuaternion(x / mag, y / mag, z / mag, w / mag);
                return new MyQuaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
            }
        }

        public Vec3 eulerAngles{
            get{
                Vec3 qAsVec3;
                qAsVec3.x = radToDeg * (float)Math.Asin(x * 2);
                qAsVec3.y = radToDeg * (float)Math.Asin(y * 2);
                qAsVec3.z = radToDeg * (float)Math.Asin(z * 2);
                return qAsVec3;
            }
            set{
                MyQuaternion quaternion = Euler(value);
                x = quaternion.x;
                y = quaternion.y;
                z = quaternion.z;
                w = quaternion.w;
            }
        }

        // Constructors ===================
        public MyQuaternion(float _x, float _y, float _z, float _w){
            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }

        public MyQuaternion(Quaternion quaternion){
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }

        public MyQuaternion(MyQuaternion quaternion){
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }

        // Public Methods ====================
        public void Set(float new_x, float new_y, float new_z, float new_w){
            x = new_x;
            y = new_y;
            z = new_z;
            w = new_w;
        }

        public void Normalize(){
            MyQuaternion normal = new MyQuaternion(normalized);
            x = normal.x;
            y = normal.y;
            z = normal.z;
            w = normal.w;
        }

        public void SetFromToRotation(Vec3 fromDirection, Vec3 toDirection){
            MyQuaternion quaternion = FromToRotation(fromDirection, toDirection);
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }

        public void SetLookRotation(Vec3 view){
            Vec3 up = Vec3.Up;
            this.SetLookRotation(view, up);
        }
        public void SetLookRotation(Vec3 view, [DefaultValue("Vec3.Up")] Vec3 up){
            this = MyQuaternion.LookRotation(view, up);
        }

        public void ToAngleAxis(out float angle, out Vec3 axis){
            throw new NotImplementedException();
        }

        public override string ToString(){
            return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", this.x, this.y, this.z, this.w);
        }
        public string ToString(string format){
            return string.Format("({0}, {1}, {2}, {3})", this.x.ToString(format), this.y.ToString(format), this.z.ToString(format), this.w.ToString(format));
        }

        // Statics Methods ==================
        public static float Angle(MyQuaternion a, MyQuaternion b){
            float f = MyQuaternion.Dot(a, b);
            return (float)Math.Acos(Math.Min(Math.Abs(f), 1f)) * 2f * radToDeg;
        }

        public static MyQuaternion AngleAxis(float angle, Vec3 axis){
            if(axis.sqrMagnitude == 0.0f)
                return identity;

            angle *= degToRad * 0.5f;
            axis.Normalize();
            MyQuaternion result = identity;
            axis = axis * (float)Math.Sin(angle);
            result.x = axis.x;
            result.y = axis.y;
            result.z = axis.z;
            result.w = (float)Math.Cos(angle);

            return Normalize(result);
        }

        public static float Dot(MyQuaternion a, MyQuaternion b){
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static MyQuaternion Euler(Vec3 euler){
            MyQuaternion quaternionX = identity;
            MyQuaternion quaternionY = identity;
            MyQuaternion quaternionZ = identity;

            float sin = (float)Math.Sin(degToRad * euler.x * 0.5f);
            float cos = (float)Math.Cos(degToRad * euler.x * 0.5f);
            quaternionX.Set(sin, 0f, 0f, cos);

            sin = (float)Math.Sin(degToRad * euler.y * 0.5f);
            cos = (float)Math.Cos(degToRad * euler.y * 0.5f);
            quaternionY.Set(0f, sin, 0f, cos);

            sin = (float)Math.Sin(degToRad * euler.z * 0.5f);
            cos = (float)Math.Cos(degToRad * euler.z * 0.5f);
            quaternionZ.Set(0f, 0f, sin, cos);

            return new MyQuaternion(quaternionX * quaternionY * quaternionZ);
        }

        public static MyQuaternion FromToRotation(Vec3 fromDirection, Vec3 toDirection){
            Vec3 cross = Vec3.Cross(fromDirection, toDirection);
            MyQuaternion quaternion;
            quaternion.x = cross.x;
            quaternion.y = cross.y;
            quaternion.z = cross.z;
            quaternion.w = fromDirection.magnitude * toDirection.magnitude + Vec3.Dot(fromDirection, toDirection);
            quaternion.Normalize();
            return quaternion;
        }

        public static MyQuaternion Inverse(MyQuaternion rotation){
            return new MyQuaternion(-rotation.x, -rotation.y, -rotation.z, rotation.w);
        }

        public static MyQuaternion Lerp(MyQuaternion a, MyQuaternion b, float t){
            if(t > 1) t = 1;
            if(t < 0) t = 0;
            return LerpUnclamped(a, b, t);
        }

        public static MyQuaternion LerpUnclamped(MyQuaternion a, MyQuaternion b, float t){
            MyQuaternion resultInterpolated = identity;

            if(t >= 1)
                resultInterpolated = b;
            else if (t <= 0)
                resultInterpolated = a;
            else{
                MyQuaternion difference = new MyQuaternion(b.x - a.x, b.y - a.y, b.z - a.z, b.w - b.w);
                MyQuaternion differenceLerped = new MyQuaternion(difference.x * t, difference.y * t, difference.z * t, difference.w * t);

                resultInterpolated = new MyQuaternion(a.x + differenceLerped.x, a.y + differenceLerped.y, a.z + differenceLerped.z, a.w + differenceLerped.w);
            }
            return resultInterpolated.normalized;
        }

        public static MyQuaternion LookRotation(Vec3 forward, [DefaultValue("Vec3.Up")] Vec3 upwards){
            return MyQuaternion.LookRotation(ref forward, ref upwards);
        }
        public static MyQuaternion LookRotation(Vec3 forward){
            Vec3 up = Vec3.Up;
            return MyQuaternion.LookRotation(ref forward, ref up);
        }
        private static MyQuaternion LookRotation(ref Vec3 forward, ref Vec3 up){
            throw new NotImplementedException();
        }

        public static MyQuaternion Normalize(MyQuaternion quaternion){
            return new MyQuaternion(quaternion.normalized);
        }

        public static MyQuaternion RotateTowards(MyQuaternion from, MyQuaternion to, float maxDegreesDelta){
            float num = MyQuaternion.Angle(from, to);
            if(num == 0f)
                return to;
            float t = Math.Min(1f, maxDegreesDelta / num);
            return MyQuaternion.SlerpUnclamped(from, to, t);
        }

        public static MyQuaternion Slerp(MyQuaternion a, MyQuaternion b, float t){
            if(t > 1) t = 1;
            if(t < 0) t = 0;
            return SlerpUnclamped(a, b, t);
        }

        public static MyQuaternion SlerpUnclamped(MyQuaternion a, MyQuaternion b, float t){
            MyQuaternion resultInterpolated = identity;

            float cosHalfTheta = Dot(a, b);
            if(Math.Abs(cosHalfTheta) >= 1f){
                resultInterpolated.Set(a.x, a.y, a.z, a.w);
                return resultInterpolated;
            }

            float halfTheta = (float)Math.Acos(cosHalfTheta);
            float sinHalfTheta = (float)Math.Sqrt(1 - cosHalfTheta * cosHalfTheta);
            if(Math.Abs(sinHalfTheta) < 0.001f){
                resultInterpolated.w = (a.w * 0.5f + b.w * 0.5f);
                resultInterpolated.x = (a.x * 0.5f + b.x * 0.5f);
                resultInterpolated.y = (a.y * 0.5f + b.y * 0.5f);
                resultInterpolated.z = (a.z * 0.5f + b.z * 0.5f);
                return resultInterpolated;
            }

            float ratioA = (float)Math.Sin((1 - t) * halfTheta) / sinHalfTheta;
            float ratioB = (float)Math.Sin(t * halfTheta) / sinHalfTheta;
            resultInterpolated.w = (a.w * ratioA + b.w * ratioB);
            resultInterpolated.x = (a.x * ratioA + b.x * ratioB);
            resultInterpolated.y = (a.y * ratioA + b.y * ratioB);
            resultInterpolated.z = (a.z * ratioA + b.z * ratioB);
            return resultInterpolated;
        }

        //operators ====================
        public static Vec3 operator *(MyQuaternion rotation, Vec3 point){
            float num = rotation.x * 2f;
            float num2 = rotation.y * 2f;
            float num3 = rotation.z * 2f;
            float num4 = rotation.x * num;
            float num5 = rotation.y * num2;
            float num6 = rotation.z * num3;
            float num7 = rotation.x * num2;
            float num8 = rotation.x * num3;
            float num9 = rotation.y * num3;
            float num10 = rotation.w * num;
            float num11 = rotation.w * num2;
            float num12 = rotation.w * num3;
            
            Vec3 result;
            result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
            result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
            result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
            return result;
        }

        public static MyQuaternion operator *(MyQuaternion a, MyQuaternion b){
            float awXbw = a.w * b.w;
            float awXbx = a.w * b.x;
            float awXby = a.w * b.y;
            float awXbz = a.w * b.z;

            float axXbw = a.x * b.w;
            float axXbx = a.x * b.x; 
            float axXby = a.x * b.y; 
            float axXbz = a.x * b.z; 

            float ayXbw = a.y * b.w;
            float ayXbx = a.y * b.x; 
            float ayXby = a.y * b.y; 
            float ayXbz = a.y * b.z; 

            float azXbw = a.z * b.w;
            float azXbx = a.z * b.x; 
            float azXby = a.z * b.y; 
            float azXbz = a.z * b.z; 


            float rowRealResult = (awXbw - axXbx - ayXby - azXbz);
            float rowIResult = (awXbx + axXbw + ayXbz - azXby);
            float rowJResult = (awXby - axXbz + ayXbw + azXbx);
            float rowKResult = (awXbz + axXby - ayXbx + azXbw);

            return new MyQuaternion(rowIResult, rowJResult, rowKResult, rowRealResult);
        }

        public static bool operator ==(MyQuaternion lhs, MyQuaternion rhs) {
            return MyQuaternion.Dot(lhs, rhs) > 0.999999f;
        }

        public static bool operator !=(MyQuaternion lhs, MyQuaternion rhs) {
            return MyQuaternion.Dot(lhs, rhs) <= 0.999999f;
        }

        public override int GetHashCode() {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
        }

        public override bool Equals(object other) {
            if(!(other is MyQuaternion))
                return false;
            MyQuaternion quaternion = (MyQuaternion)other;
            return this.x.Equals(quaternion.x) && this.y.Equals(quaternion.y) && this.z.Equals(quaternion.z) && this.w.Equals(quaternion.w);
        }
        public bool Equals(MyQuaternion other){
            return this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z) && this.w.Equals(other.w);
        }
    }
}