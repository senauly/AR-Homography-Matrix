using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part1 : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        //calculate HomographyMatrix from 4 points
        double[,] src = new double[4, 2] { { 5, 1 }, { 4, 1 }, { 10, 12 }, { 6, 6 } };
        double[,] dst = new double[4, 2] { { 3, 2 }, { 7, 8 }, { 4, 4 }, { 2, 3 } };

        double[,] H = CalculateHomography.GetTheHomographyMatrix(src, dst);
        printMatrix(H, "Homography Matrix");

        //projection 

        double[,] sp = new double[3, 1] { { src[0,0] }, { src[1,0] } , { src[2,0] } };
        double[,] projS = CalculateHomography.CalculateProjectionForScenePoint(H, sp);
        Debug.Log("Projection of the scene point (" + src[0,0] + "," + src[1,0]  + "," + src[2,0] +  ") onto the target image: (" + projS[0, 0] + "," + projS[1, 0] + "," + projS[2, 0] + ")");

        //projection for image point
        double[,] ip = new double[3, 1] { { dst[0, 0] }, { dst[1, 0] }, { dst[2,0] } };
        double[,] projI = CalculateHomography.CalculateProjectionForImagePoint(H, ip);
        Debug.Log("Projection of the image point (" + dst[0,0] + "," + dst[1,0]  + "," + dst[2,0] + ") onto the scene: (" + projI[0, 0] + "," + projI[1, 0] + "," + projI[2, 0] + ")");

    }

    private void printMatrix(double[,] matrix, string header)
    {
        string res = "";
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                res += matrix[i, j] + "  ";
            }
            res += "\n";
        }

        Debug.Log(header + "\n" + res);
    }
}
