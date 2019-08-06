using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_3D
{
    public class Matrix3
    {
        public float[,] M = new float[4, 4];

        public Matrix3()
        {
            Identify3();
        }

        public Matrix3(
            float m00, float m01, float m02, float m03,
            float m10, float m11, float m12, float m13,
            float m20, float m21, float m22, float m23,
            float m30, float m31, float m32, float m33)
        {
            M[0, 0] = m00; M[0, 1] = m01; M[0, 2] = m02; M[0, 3] = m03;
            M[1, 0] = m10; M[1, 1] = m11; M[1, 2] = m12; M[1, 3] = m13;
            M[2, 0] = m20; M[2, 1] = m21; M[2, 2] = m22; M[2, 3] = m23;
            M[3, 0] = m30; M[3, 1] = m31; M[3, 2] = m32; M[3, 3] = m33;
        }

        public void Identify3()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (i == j) 
                        M[i, j] = 1;
                    else
                        M[i, j] = 0;
        }

        public static Matrix3 operator *(Matrix3 m1, Matrix3 m2)
        {
            Matrix3 result = new Matrix3();

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    float element = 0;
                    for (int k = 0; k < 4; k++)
                        element += m1.M[i, k] * m2.M[k, j];
                    result.M[i, j] = element;
                }

            return result;
        }

        public float[] VectorMultiply(float[] vector)
        {
            float[] result = new float[vector.Length];

            for (int i = 0; i < M.GetLength(0); i++)
                for (int j = 0; j < M.GetLength(1); j++)
                    result[i] += M[i, j] * vector[j];

            return result;
        }

        public static Matrix3 AzimuthElevation(float elevation, float azimuth)
        {
            Matrix3 result;

            if (elevation > 90)
                elevation = 90;
            else if (elevation < -90)
                elevation = -90;

            if (azimuth > 180)
                azimuth = 180;
            else if (azimuth < -180)
                azimuth = -180;

            elevation *= (float)(Math.PI / 180.0);
            azimuth *= (float)(Math.PI / 180.0);

            float sne = (float)Math.Sin(elevation),
                cne = (float)Math.Cos(elevation),
                sna = (float)Math.Sin(azimuth),
                cna = (float)Math.Cos(azimuth);

            //result = new Matrix3(
            //    cna, sna, 0, 0,
            //    -sne * sna, sne * cna, cne, 0,
            //    cne * sna, -cne * cna, sne, 0,
            //    0, 0, 0, 1);

            result = Rx(elevation - (float)(Math.PI / 2.0)) * Rz(-azimuth);

            return result;
        }

        /// <summary>
        /// Обертання навколо осі Х (вверх/вниз)
        /// вісь направлена від центра вправо
        /// </summary>
        /// <param name="alpha">кут в радіанах</param>
        /// <returns></returns>
        private static Matrix3 Rx(float alpha)
        {
            float sna = (float)Math.Sin(alpha),
                cna = (float)Math.Cos(alpha);

            Matrix3 result = new Matrix3(
                1, 0, 0, 0,
                0, cna, -sna, 0,
                0, sna, cna, 0,
                0, 0, 0, 1);

            return result;
        }

        /// <summary>
        /// Обертання навколо осі Y (вліво/вправо)
        /// вісь направлена від центра вгору
        /// </summary>
        /// <param name="alpha">кут в радіанах</param>
        /// <returns></returns>
        private static Matrix3 Ry(float alpha)
        {
            float sna = (float)Math.Sin(alpha),
                cna = (float)Math.Cos(alpha);

            Matrix3 result = new Matrix3(
                cna, 0, sna, 0,
                0, 1, 0, 0,
                -sna, 0, cna, 0,
                0, 0, 0, 1);

            return result;
        }

        /// <summary>
        /// Обертання навколо осі Z (вперед/назад)
        /// вісь направлена від центра вперед
        /// </summary>
        /// <param name="alpha">кут в радіанах</param>
        /// <returns></returns>
        private static Matrix3 Rz(float alpha)
        {
            float sna = (float)Math.Sin(alpha),
                cna = (float)Math.Cos(alpha);

            Matrix3 result = new Matrix3(
                cna, -sna, 0, 0,
                sna, cna, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

            return result;
        }

    }
}
