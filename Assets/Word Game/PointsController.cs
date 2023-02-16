using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    public int CountPoints(int[,] matrix)
    { 
        int primaryPoints = 0;
        int combo = 1;

        for(int i = 0; i < matrix.GetLength(0); i++)
        {
            for(int j = 0; j<matrix.GetLength(1); j++)
            {
                if (matrix[i, j] != 0) primaryPoints++;
                if(j+1 < matrix.GetLength(1))
                {
                    if (Mathf.Abs(matrix[i,j] - matrix[i, j+1]) == 1
                        || matrix[i, j+1] == 10)
                    {
                        combo++;
                        C.tape.HighlightNumbers(i, j);
                        C.tape.HighlightNumbers(i, j+1);
                    }
                }
                if(i + 1 < matrix.GetLength(0))
                {
                    if(Mathf.Abs(matrix[i, j] - matrix[i+1, j]) == 1
                        || matrix[i+1, j] == 10)
                    {
                        combo++;
                        C.tape.HighlightNumbers(i, j);
                        C.tape.HighlightNumbers(i+1, j);
                    }
                }
            }
        }
        return primaryPoints * combo;
    }
}
