using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tapes;
using System;

public class Points : MonoBehaviour
{
    public event Action PointsChanged;

    private const int minPoints = 0;
    private int maxPointsOnLvl = 50;
    private int currentPoints;

    public int CurrentPoints
    {
        get => currentPoints;
        set => currentPoints = value;
    }

    public int MaxPointsOnLvl => maxPointsOnLvl;
    public int MinPoints => minPoints;

    public void Increment(int amount)
    {
        currentPoints += amount;
        UpdatePoints();
    }
    public void Decrement(int amount)
    {
        currentPoints-= amount;
        currentPoints = Mathf.Clamp(currentPoints, minPoints, maxPointsOnLvl);
        UpdatePoints();
    }
    public void Restart()
    {
        currentPoints = minPoints;
        UpdatePoints();
    }
    public void UpdatePoints()
    {
        PointsChanged?.Invoke();
    }

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
                        //TapeHandler.HighlightNumbers(i, j);
                        //TapeHandler.HighlightNumbers(i, j+1);
                    }
                }
                if(i + 1 < matrix.GetLength(0))
                {
                    if(Mathf.Abs(matrix[i, j] - matrix[i+1, j]) == 1
                        || matrix[i+1, j] == 10)
                    {
                        combo++;
                        //TapeHandler.HighlightNumbers(i, j);
                        //TapeHandler.HighlightNumbers(i+1, j);
                    }
                }
            }
        }
        return primaryPoints * combo;
    }
}
