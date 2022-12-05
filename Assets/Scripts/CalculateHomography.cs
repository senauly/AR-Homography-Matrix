using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;

public class CalculateHomography : MonoBehaviour
{
    //calculate HomographyMatrix from 4 points
    private static double[] CalculateHM(double[,] src, double[,] dst)
    {
        //calculate 8x9 matrix from 4 points
        double[,] A = new double[8, 9];

        //calculate A
        for (int i = 0; i < 4; i++)
        {
            A[i * 2, 0] = -src[i, 0]; 
            A[i * 2, 1] = -src[i, 1];
            A[i * 2, 2] = -1;
            A[i * 2, 3] = 0;
            A[i * 2, 4] = 0;
            A[i * 2, 5] = 0;
            A[i * 2, 6] = src[i, 0] * dst[i, 0];
            A[i * 2, 7] = src[i, 1] * dst[i, 0];
            A[i * 2, 8] = dst[i, 0];

            A[i * 2 + 1, 0] = 0;
            A[i * 2 + 1, 1] = 0;
            A[i * 2 + 1, 2] = 0;
            A[i * 2 + 1, 3] = -src[i, 0];
            A[i * 2 + 1, 4] = -src[i, 1];
            A[i * 2 + 1, 5] = -1;
            A[i * 2 + 1, 6] = src[i, 0] * dst[i, 1];
            A[i * 2 + 1, 7] = src[i, 1] * dst[i, 1];
            A[i * 2 + 1, 8] = dst[i, 1];
        }

        //calculate SVD of A
        Matrix<double> AMatrix = DenseMatrix.OfArray(A);
        Svd<double> svd = AMatrix.Svd(true);

        //get V matrix
        Matrix<double> V = svd.VT;

        //get the transpose of V
        Matrix<double> Vt = V.Transpose();

        //get the last row of Vt
        double[] h = new double[9];
        for (int i = 0; i < 9; i++)
        {
            h[i] = Vt[i, 8];
        }

        return h;

    }

    public static double[,] GetTheHomographyMatrix(double[,] src, double[,] dst)
    {
        //calculate HomographyMatrix from 4 points
        double[] h = CalculateHM(src, dst);

        //get the 3x3 matrix from h
        double[,] hm = new double[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                hm[i, j] = h[i * 3 + j];
            }
        }

        return hm;
    }

    //calculate the projection for scene point
    private static double[,] CalculateProjection(double[,] hm, double[,] p)
    {
        //get dense matrix from hm
        Matrix<double> hmMatrix = DenseMatrix.OfArray(hm);

        //get dense matrix from p
        Matrix<double> pMatrix = DenseMatrix.OfArray(p);

        //multiply hmMatrix and pMatrix
        Matrix<double> res = hmMatrix.Multiply(pMatrix);
    
        //calculate the projection
        double[,] projection = new double[,] { { res[0, 0] / res[2, 0] }, { res[1, 0] / res[2, 0] }, { 1 } };

        return projection;
    }

    //calculate the projection for scene point
    public static double[,] CalculateProjectionForScenePoint(double[,] hm, double[,] p)
    {
        return CalculateProjection(hm, p);
    }

    //calculate the projection for image point
    public static double[,] CalculateProjectionForImagePoint(double[,] hm, double[,] p)
    {
        //get the inverse of hm
        Matrix<double> hmMatrix = DenseMatrix.OfArray(hm);
        Matrix<double> hmInverse = hmMatrix.Inverse();

        return CalculateProjection(hmInverse.ToArray(), p);

    }

}
