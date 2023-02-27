using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tapes
{
    public class MatrixCreator : TapeHandler
    {
        public int[,] CreateMatrix()
        {
            Bonus[] bs = FindObjectsOfType<Bonus>();
            if (bs.Length != 0)
            {
                foreach (var b in bs) Destroy(b.gameObject);
            }
            SortColumns();

            if (preferredXPos.Count == 0)
            {
                Debug.Log("There are no saved numbers on field!");

                C.main.StartGame();
            }

            numberGOs = new GameObject[tapes.Count, preferredXPos.Count];
            int[,] matrix = new int[tapes.Count, preferredXPos.Count];

            for (int i = 0; i < tapes.Count; i++)
            {
                tapes[i].StopTape();
                for (int j = 0; j < preferredXPos.Count; j++)
                {
                    numberGOs[i, j] = tapes[i].transform.GetChild(j).gameObject;
                    matrix[i, j] = Convert.ToInt32(numberGOs[i, j].name);

                    if (numberGOs[i, j].name == 0.ToString())
                    {
                        SpriteRenderer spr = tapes[i].transform.GetChild(j).GetComponent<SpriteRenderer>();
                        Sprite[] s = Resources.LoadAll<Sprite>("Sprites/numbers");
                        spr.sprite = s[0];
                    }
                    //Debug.Log($"{i}'s row {j} column {matrix[i, j]} value");
                }
            }
            return matrix;
        }
    }
}
