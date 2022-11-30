using Microsoft.Xna.Framework;

public struct Mat4
{
    float []asArray = new float[16];
    public float _11, _12, _13, _14, 
                _21, _22, _23, _24,
                _31, _32, _33, _34,
                _41, _42, _43, _44;

    public float this[int i]
    {
        get {return asArray[i * 4];}
    }

    public Mat4(float m11, float m12, float m13, float m14,
    float m21, float m22, float m23, float m24, 
    float m31, float m32, float m33 ,float m34,
    float m41, float m42, float m43, float m44)
    {
        this._11 = m11;
        this._12 = m12;
        this._13 = m13;
        this._14 = m14;
        this._21 = m21;
        this._22 = m22;
        this._23 = m23;
        this._24 = m24;
        this._31 = m31;
        this._32 = m32;
        this._33 = m33;
        this._34 = m34;
        this._41 = m41;
        this._42 = m42;
        this._43 = m43;
        this._44 = m44;

        asArray[0] = this._11;
        asArray[1] = this._12;
        asArray[2] = this._13;
        asArray[3] = this._14;
        asArray[4] = this._21;
        asArray[5] = this._22;
        asArray[6] = this._23;
        asArray[7] = this._24;
        asArray[8] = this._31;
        asArray[9] = this._32;
        asArray[10] = this._33;
        asArray[11] = this._34;
        asArray[12] = this._41;
        asArray[13] = this._42;
        asArray[14] = this._43;
        asArray[15] = this._44;
    }

    public Mat4 (Vector4 r1, Vector4 r2, Vector4 r3, Vector4 r4)
    {
        this._11 = r1.X;
        this._12 = r2.X;
        this._13 = r3.X;
        this._14 = r4.X;
        this._21 = r1.Y;
        this._22 = r2.Y;
        this._23 = r3.Y;
        this._24 = r4.Y;
        this._31 = r1.Z;
        this._32 = r2.Z;
        this._33 = r3.Z;
        this._34 = r4.Z;
        this._41 = r1.W;
        this._42 = r2.W;
        this._43 = r3.W;
        this._44 = r4.W;

        asArray[0] = this._11;
        asArray[1] = this._12;
        asArray[2] = this._13;
        asArray[3] = this._14;
        asArray[4] = this._21;
        asArray[5] = this._22;
        asArray[6] = this._23;
        asArray[7] = this._24;
        asArray[8] = this._31;
        asArray[9] = this._32;
        asArray[10] = this._33;
        asArray[11] = this._34;
        asArray[12] = this._41;
        asArray[13] = this._42;
        asArray[14] = this._43;
        asArray[15] = this._44;
    }
}

public struct Mat3
{
    float []asArray = new float[9];
    public float _11, _12, _13, _21, _22, _23, _31, _32, _33;

    public float this[int i]
    {
        get {return asArray[i * 3];}
    }

    public Mat3(float m11, float m12, float m13, 
    float m21, float m22, float m23, 
    float m31, float m32, float m33)
    {
        this._11 = m11;
        this._12 = m12;
        this._13 = m13;
        this._21 = m21;
        this._22 = m22;
        this._23 = m23;
        this._31 = m31;
        this._32 = m32;
        this._33 = m33;

        asArray[0] = this._11;
        asArray[1] = this._12;
        asArray[2] = this._13;
        asArray[3] = this._21;
        asArray[4] = this._22;
        asArray[5] = this._23;
        asArray[6] = this._31;
        asArray[7] = this._32;
        asArray[8] = this._33;
    }

    public Mat3 (Vector3 r1, Vector3 r2, Vector3 r3)
    {
        this._11 = r1.X;
        this._12 = r2.X;
        this._13 = r3.X;
        this._21 = r1.Y;
        this._22 = r2.Y;
        this._23 = r3.Y;
        this._31 = r1.Z;
        this._32 = r2.Z;
        this._33 = r3.Z;

        asArray[0] = this._11;
        asArray[1] = this._12;
        asArray[2] = this._13;
        asArray[3] = this._21;
        asArray[4] = this._22;
        asArray[5] = this._23;
        asArray[6] = this._31;
        asArray[7] = this._32;
        asArray[8] = this._33;
    }
}

public struct Mat2
{
    float []asArray = new float[4];
    public float _11, _12, _21, _22;

    public float this[int i]
    {
        get {return asArray[i * 2];}
    }

    public Mat2(float m11, float m12, float m21, float m22)
    {
        this._11 = m11;
        this._12 = m12;
        this._21 = m21;
        this._22 = m22;

        asArray[0] = this._11;
        asArray[1] = this._12;
        asArray[3] = this._21;
        asArray[4] = this._22;
    }

    public Mat2 (Vector2 r1, Vector2 r2)
    {
        this._11 = r1.X;
        this._12 = r2.X;
        this._21 = r1.Y;
        this._22 = r2.Y;

        asArray[0] = this._11;
        asArray[1] = this._12;
        asArray[3] = this._21;
        asArray[4] = this._22;
    }
}

class Matrices
{
    public void Transpose(float []srcMat, float []destMat, int srcRows, int srcCols)
    {
        for (int i = 0; i < srcRows * srcCols; i++) 
        {
            int row = i / srcRows;
            int col = i % srcRows;
            destMat[i] = srcMat[srcCols * col + row];
        }
    }
    
    Mat4 Scale(float x, float y, float z) 
    {
    return new Mat4( x, 0.0f, 0.0f, 0.0f, 0.0f, y, 0.0f, 0.0f,
        0.0f, 0.0f, z, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);
    }


    Mat4 Translation(float x, float y, float z) 
    {
        return new Mat4(1.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 1.0f, 0.0f,x,y,z,1.0f);
    }

    float Rotation(float pitch, float yaw, float roll) 
    {
            return roll * pitch * yaw;
    }
}