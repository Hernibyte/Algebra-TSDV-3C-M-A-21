using System.Collections;
using System.Collections.Generic;
using UnityEngine.Internal;
using UnityEngine;
using CustomMath;
using System;

namespace CustomMatrix{
    public struct MyMatrix4x4 : IEquatable<MyMatrix4x4> {
        public float m00;
        public float m01;
        public float m02;
        public float m03;
        public float m10;
        public float m11;
        public float m12;
        public float m13;
        public float m20;
        public float m21;
        public float m22;
        public float m23;
        public float m30;
        public float m31;
        public float m32;
        public float m33;

        public static MyMatrix4x4 identity{
            get{
                return new MyMatrix4x4(
                    new Vector4(1, 0, 0, 0),
                    new Vector4(0, 1, 0, 0),
                    new Vector4(0, 0, 1, 0),
                    new Vector4(0, 0, 0, 1)
                );
            }
        }

        public static MyMatrix4x4 zero{
            get{
                return new MyMatrix4x4(
                    new Vector4(0, 0, 0, 0),
                    new Vector4(0, 0, 0, 0),
                    new Vector4(0, 0, 0, 0),
                    new Vector4(0, 0, 0, 0)
                );
            }
        }

        public MyMatrix4x4 transpose{
            get{
                MyMatrix4x4 _transpose = new MyMatrix4x4(
                    new Vector4(m00, m01, m02, m03),
                    new Vector4(m10, m11, m12, m13),
                    new Vector4(m20, m21, m22, m23),
                    new Vector4(m30, m31, m32, m33)
                );
                return _transpose;
            }
        }

        public Vector3 lossyScale{
            get{
                return new Vector3(m00, m11, m22);
            }
        }

        // Contructor ==================
        public MyMatrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3){
            m00 = column0.x;
            m01 = column1.x;
            m02 = column2.x;
            m03 = column3.x;
            m10 = column0.y;
            m11 = column1.y;
            m12 = column2.y;
            m13 = column3.y;
            m20 = column0.z;
            m21 = column1.z;
            m22 = column2.z;
            m23 = column3.z;
            m30 = column0.w;
            m31 = column1.w;
            m32 = column2.w;
            m33 = column3.w;
        }

        // Public Methods =================
        public Vector4 GetColumn(int index){
            Vector4 column = Vector4.zero;
            switch(index){
                case 0:
                    column = new Vector4(m00, m10, m20, m30);
                break;
                case 1:
                    column = new Vector4(m01, m11, m21, m31);
                break;
                case 2:
                    column = new Vector4(m02, m12, m22, m23);
                break;
                case 3:
                    column = new Vector4(m03, m13, m23, m33);
                break;
                default:
                    throw new IndexOutOfRangeException("Invalid Quaternion index: " + index + ", can use only 0,1,2,3");
            }
            return column;
        }

        public Vector4 GetRow(int index){
            Vector4 row = Vector4.zero;
            switch(index){
                case 0:
                    row = new Vector4(m00, m01, m02, m03);
                break;
                case 1:
                    row = new Vector4(m10, m11, m12, m13);
                break;
                case 2:
                    row = new Vector4(m20, m21, m22, m23);
                break;
                case 3:
                    row = new Vector4(m30, m31, m32, m33);
                break;
                default:
                    throw new IndexOutOfRangeException("Invalid Quaternion index: " + index + ", can use only 0,1,2,3");
            }
            return row;
        }

        public Vector3 MultiplyPoint(Vector3 point){
            throw new NotImplementedException();
        }

        public Vector3 MultiplyPoint3x4(Vector3 point){
            throw new NotImplementedException();
        }

        public Vector3 MultiplyVector(Vector3 vector){
            throw new NotImplementedException();
        }

        public void SetColumn(int index, Vector4 column){
            switch(index){
                case 0:
                    m00 = column.x;
                    m10 = column.y;
                    m20 = column.z;
                    m30 = column.w;
                break;
                case 1:
                    m01 = column.x;
                    m11 = column.y;
                    m21 = column.z;
                    m31 = column.w;
                break;
                case 2:
                    m02 = column.x;
                    m12 = column.y;
                    m22 = column.z;
                    m32 = column.w;
                break;
                case 3:
                    m03 = column.x;
                    m13 = column.y;
                    m23 = column.z;
                    m33 = column.w;
                break;
                default:
                    Debug.LogError("Index error: Out of range");
                break;
            }
        }

        public void SetRow(int index, Vector4 row){
            switch(index){
                case 0:
                    m00 = row.x;
                    m01 = row.y;
                    m02 = row.z;
                    m03 = row.w;
                break;
                case 1:
                    m10 = row.x;
                    m11 = row.y;
                    m12 = row.z;
                    m13 = row.w;
                break;
                case 2:
                    m20 = row.x;
                    m21 = row.y;
                    m22 = row.z;
                    m23 = row.w;
                break;
                case 3:
                    m30 = row.x;
                    m31 = row.y;
                    m32 = row.z;
                    m33 = row.w;
                break;
                default:
                    Debug.LogError("Index error: Out of range");
                break;
            }
        }

        public void SetTRS(Vector3 position, Quaternion rotation, Vector3 scale){
            this = TRS(position, rotation, scale);
        }

        // Static Methods =================
        public static MyMatrix4x4 Rotate(Quaternion quaternion){
            MyMatrix4x4 mat = identity;
            mat.m02 = 2.0f * (quaternion.x * quaternion.z) + 2.0f * (quaternion.y * quaternion.w);
            mat.m12 = 2.0f * (quaternion.y * quaternion.z) - 2.0f * (quaternion.x * quaternion.w);
            mat.m22 = 1 - 2.0f * (quaternion.x * quaternion.x) - 2.0f * (quaternion.y * quaternion.y);

            mat.m00 = 1 - 2.0f * (quaternion.y * quaternion.y) - 2.0f * (quaternion.z * quaternion.z);
            mat.m10 = 2.0f * (quaternion.x * quaternion.y) + 2.0f * (quaternion.z * quaternion.w);
            mat.m20 = 2.0f * (quaternion.x * quaternion.z) - 2.0f * (quaternion.y * quaternion.w);

            mat.m01 = 2.0f * (quaternion.x * quaternion.y) - 2.0f * (quaternion.z * quaternion.w);
            mat.m11 = 1 - 2.0f * (quaternion.x * quaternion.x) - 2.0f * (quaternion.z * quaternion.z);
            mat.m21 = 2.0f * (quaternion.y * quaternion.z) + 2.0f * (quaternion.x * quaternion.w);
            return mat;
        }

        public static MyMatrix4x4 Scale(Vector3 scale){
            MyMatrix4x4 mat = zero;
            mat.m00 *= scale.x;
            mat.m11 *= scale.y;
            mat.m22 *= scale.z;
            mat.m33 = 1;
            return mat;
        }

        public static MyMatrix4x4 Translate(Vector3 translate){
            MyMatrix4x4 mat = zero;
            mat.m03 += translate.x;
            mat.m13 += translate.y;
            mat.m23 += translate.z;
            mat.m33 = 1;
            return mat;
        }

        public static MyMatrix4x4 TRS(Vector3 position, Quaternion rotation, Vector3 scale){
            MyMatrix4x4 _tranlate = Translate(position);
            MyMatrix4x4 _rotate = Rotate(rotation);
            MyMatrix4x4 _scale = Scale(scale);
            MyMatrix4x4 trs = _tranlate * _rotate * _scale;
            return trs;
        }

        // Operators =============
        public static Vector4 operator *(MyMatrix4x4 a, Vector4 vector){
            Vector4 newVec4 = Vector4.zero;
            newVec4.x = (a.m00 * vector.x) + (a.m01 * vector.y) + (a.m02 * vector.z) + (a.m03 * vector.w);
            newVec4.y = (a.m10 * vector.x) + (a.m11 * vector.y) + (a.m12 * vector.z) + (a.m13 * vector.w);
            newVec4.z = (a.m20 * vector.x) + (a.m21 * vector.y) + (a.m22 * vector.z) + (a.m23 * vector.w);
            newVec4.w = (a.m30 * vector.x) + (a.m31 * vector.y) + (a.m32 * vector.z) + (a.m33 * vector.w);
            return newVec4;
        }

        public static MyMatrix4x4 operator *(MyMatrix4x4 a, MyMatrix4x4 b){
            MyMatrix4x4 matXMat = zero;
            matXMat.m00 = (a.m00 * b.m00) + (a.m01 * b.m10) + (a.m02 * b.m20) + (a.m03 * b.m30);
            matXMat.m01 = (a.m00 * b.m01) + (a.m01 * b.m11) + (a.m02 * b.m21) + (a.m03 * b.m31);
            matXMat.m02 = (a.m00 * b.m02) + (a.m01 * b.m12) + (a.m02 * b.m22) + (a.m03 * b.m32);
            matXMat.m03 = (a.m00 * b.m03) + (a.m01 * b.m13) + (a.m02 * b.m23) + (a.m03 * b.m33);

            matXMat.m10 = (a.m10 * b.m00) + (a.m11 * b.m10) + (a.m12 * b.m20) + (a.m13 * b.m30);
            matXMat.m11 = (a.m10 * b.m01) + (a.m11 * b.m11) + (a.m12 * b.m21) + (a.m13 * b.m31);
            matXMat.m12 = (a.m10 * b.m02) + (a.m11 * b.m12) + (a.m12 * b.m22) + (a.m13 * b.m32);
            matXMat.m13 = (a.m10 * b.m03) + (a.m11 * b.m13) + (a.m12 * b.m23) + (a.m13 * b.m33);
            
            matXMat.m20 = (a.m20 * b.m00) + (a.m21 * b.m10) + (a.m22 * b.m20) + (a.m23 * b.m30);
            matXMat.m21 = (a.m20 * b.m01) + (a.m21 * b.m11) + (a.m22 * b.m21) + (a.m23 * b.m31);
            matXMat.m22 = (a.m20 * b.m02) + (a.m21 * b.m12) + (a.m22 * b.m22) + (a.m23 * b.m32);
            matXMat.m23 = (a.m20 * b.m03) + (a.m21 * b.m13) + (a.m22 * b.m23) + (a.m23 * b.m33);
            
            matXMat.m30 = (a.m30 * b.m00) + (a.m31 * b.m10) + (a.m32 * b.m20) + (a.m33 * b.m30);
            matXMat.m31 = (a.m30 * b.m01) + (a.m31 * b.m11) + (a.m32 * b.m21) + (a.m33 * b.m31);
            matXMat.m32 = (a.m30 * b.m02) + (a.m31 * b.m12) + (a.m32 * b.m22) + (a.m33 * b.m32);
            matXMat.m33 = (a.m30 * b.m03) + (a.m31 * b.m13) + (a.m32 * b.m23) + (a.m33 * b.m33);
            return matXMat;
        }

        public static bool operator ==(MyMatrix4x4 a, MyMatrix4x4 b) {
            return (a.m00 == b.m00 && a.m01 == b.m01 && a.m02 == b.m02 && a.m03 == b.m03 &&
                a.m10 == b.m10 && a.m11 == b.m11 && a.m12 == b.m12 && a.m13 == b.m13 &&
                a.m20 == b.m20 && a.m21 == b.m21 && a.m22 == b.m22 && a.m23 == b.m23 &&
                a.m30 == b.m30 && a.m31 == b.m31 && a.m32 == b.m32 && a.m33 == b.m33);
        }

        public static bool operator !=(MyMatrix4x4 a, MyMatrix4x4 b) {
            return !(a==b);
        }

        public override bool Equals(object other) {
            if (!(other is MyMatrix4x4)) 
                return false;
            return Equals((MyMatrix4x4)other);
        }
        public bool Equals(MyMatrix4x4 other) {
            return GetColumn(0).Equals(other.GetColumn(0)) && GetColumn(1).Equals(other.GetColumn(1)) && GetColumn(2).Equals(other.GetColumn(2)) && GetColumn(3).Equals(other.GetColumn(3));
        }
    }
}
