using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MoveScan
{
    private int curX;
    private int curY;
    private string[,] matran = new string[8, 8];
    private float[,] aattack = new float[100,2];
    private int countA = 0;
    private int[] kq = new int[2];

    public int[] Kq { get => kq; set => kq = value; }

    private void PawnAttackScan(int x, int y, string id)
    {
        int[] location = new int[2];
        location[0] = x;
        location[1] = y;

        if (id.Contains("w"))
        {
            if (location[0] - 1 >= 0 && location[1] - 1 >= 0 && matran[location[0] - 1, location[1] - 1].Contains("b"))
            {
                aattack[countA, 0] = location[0] - 1;
                aattack[countA, 1] = location[1] - 1;
                countA++;
            }

            if (location[0] - 1 >= 0 && location[1] + 1 < 8 && matran[location[0] - 1, location[1] + 1].Contains("b"))
            {
                aattack[countA, 0] = location[0] - 1;
                aattack[countA, 1] = location[1] + 1;
                countA++;
            }
        }

        if (id.Contains("b"))
        {
            if (location[0] + 1 < 8 && location[1] - 1 >= 0 && matran[location[0] + 1, location[1] - 1].Contains("w"))
            {
                aattack[countA, 0] = location[0] + 1;
                aattack[countA, 1] = location[1] - 1;
                countA++;
            }

            if (location[0] + 1 < 8 && location[1] + 1 < 8 && matran[location[0] + 1, location[1] + 1].Contains("w"))
            {
                aattack[countA, 0] = location[0] + 1;
                aattack[countA, 1] = location[1] + 1;
                countA++;
            }
        }
    }

    private void BishopAttackScan(int x, int y, string id)
    {
        int[] location = new int[2];
        location[0] = x;
        location[1] = y;
        string pieceId = id;

        for (int i = 1; i < 8; i++)
        {
            if (location[0] + i > 7 || location[1] + i > 7)
            {
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] + i, location[1] + i].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] + i, location[1] + i].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] + i, location[1] + i].Contains("b"))
            {
                aattack[countA, 0] = (float)location[0] + i;
                aattack[countA, 1] = (float)location[1] + i;
                countA++;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] + i, location[1] + i].Contains("w"))
            {
                aattack[countA, 0] = (float)location[0] + i;
                aattack[countA, 1] = (float)location[1] + i;
                countA++;
                break;
            }
        }

        for (int i = 1; i < 8; i++)
        {
            if (location[0] + i > 7 || location[1] - i < 0)
            {
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] + i, location[1] - i].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] + i, location[1] - i].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] + i, location[1] - i].Contains("b"))
            {
                aattack[countA, 0] = (float)location[0] + i;
                aattack[countA, 1] = (float)location[1] - i;
                countA++;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] + i, location[1] - i].Contains("w"))
            {
                aattack[countA, 0] = (float)location[0] + i;
                aattack[countA, 1] = (float)location[1] - i;
                countA++;
                break;
            }
        }

        for (int i = 1; i < 8; i++)
        {
            if (location[0] - i < 0 || location[1] + i > 7)
            {
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] - i, location[1] + i].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] - i, location[1] + i].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] - i, location[1] + i].Contains("b"))
            {
                aattack[countA, 0] = (float)location[0] - i;
                aattack[countA, 1] = (float)location[1] + i;
                countA++;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] - i, location[1] + i].Contains("w"))
            {
                aattack[countA, 0] = (float)location[0] - i;
                aattack[countA, 1] = (float)location[1] + i;
                countA++;
                break;
            }
        }

        for (int i = 1; i < 8; i++)
        {
            if (location[0] - i < 0 || location[1] - i < 0)
            {
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] - i, location[1] - i].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] - i, location[1] - i].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] - i, location[1] - i].Contains("b"))
            {
                aattack[countA, 0] = (float)location[0] - i;
                aattack[countA, 1] = (float)location[1] - i;
                countA++;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] - i, location[1] - i].Contains("w"))
            {
                aattack[countA, 0] = (float)location[0] - i;
                aattack[countA, 1] = (float)location[1] - i;
                countA++;
                break;
            }
        }
    }

    private void KnightAttackScan(int x, int y, string id)
    {
        int[] location = new int[2];
        location[0] = x;
        location[1] = y;
        string pieceId = id;

        if (location[0] + 2 < 8 && location[1] + 1 < 8)
        {
            if (matran[location[0] + 2, location[1] + 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0] + 2;
                aattack[countA, 1] = location[1] + 1;
                countA++;
            }
            if (matran[location[0] + 2, location[1] + 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0] + 2;
                aattack[countA, 1] = location[1] + 1;
                countA++;
            }
        }

        if (location[0] + 2 < 8 && location[1] - 1 >= 0)
        {
            if (matran[location[0] + 2, location[1] - 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0] + 2;
                aattack[countA, 1] = location[1] - 1;
                countA++;
            }
            if (matran[location[0] + 2, location[1] - 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0] + 2;
                aattack[countA, 1] = location[1] - 1;
                countA++;
            }
        }

        if (location[0] + 1 < 8 && location[1] + 2 < 8)
        {
            if (matran[location[0] + 1, location[1] + 2].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0] + 1;
                aattack[countA, 1] = location[1] + 2;
                countA++;
            }
            if (matran[location[0] + 1, location[1] + 2].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0] + 1;
                aattack[countA, 1] = location[1] + 2;
                countA++;
            }
        }

        if (location[0] - 1 >= 0 && location[1] + 2 < 8)
        {
            if (matran[location[0] - 1, location[1] + 2].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0] - 1;
                aattack[countA, 1] = location[1] + 2;
                countA++;
            }
            if (matran[location[0] - 1, location[1] + 2].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0] - 1;
                aattack[countA, 1] = location[1] + 2;
                countA++;
            }
        }

        if (location[0] - 2 >= 0 && location[1] + 1 < 8)
        {
            if (matran[location[0] - 2, location[1] + 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0] - 2;
                aattack[countA, 1] = location[1] + 1;
                countA++;
            }
            if (matran[location[0] - 2, location[1] + 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0] - 2;
                aattack[countA, 1] = location[1] + 1;
                countA++;
            }
        }

        if (location[0] - 2 >= 0 && location[1] - 1 >= 0)
        {
            if (matran[location[0] - 2, location[1] - 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0] - 2;
                aattack[countA, 1] = location[1] - 1;
                countA++;
            }
            if (matran[location[0] - 2, location[1] - 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0] - 2;
                aattack[countA, 1] = location[1] - 1;
                countA++;
            }
        }

        if (location[0] + 1 < 8 && location[1] - 2 >= 0)
        {
            if (matran[location[0] + 1, location[1] - 2].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0] + 1;
                aattack[countA, 1] = location[1] - 2;
                countA++;
            }
            if (matran[location[0] + 1, location[1] - 2].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0] + 1;
                aattack[countA, 1] = location[1] - 2;
                countA++;
            }
        }

        if (location[0] - 1 >= 0 && location[1] - 2 >= 0)
        {
            if (matran[location[0] - 1, location[1] - 2].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0] - 1;
                aattack[countA, 1] = location[1] - 2;
                countA++;
            }
            if (matran[location[0] - 1, location[1] - 2].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0] - 1;
                aattack[countA, 1] = location[1] - 2;
                countA++;
            }
        }
    }

    private void RookAttackScan(int x, int y, string id)
    {
        int[] location = new int[2];
        location[0] = x;
        location[1] = y;
        string pieceId = id;

        for (int i = location[0] + 1; i < 8; i++)
        {
            if (pieceId.Contains("b") && matran[i, location[1]].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[i, location[1]].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[i, location[1]].Contains("b"))
            {
                aattack[0, 0] = (float)i;
                aattack[0, 1] = (float)location[1];
                break;
            }
            if (pieceId.Contains("b") && matran[i, location[1]].Contains("w"))
            {
                aattack[0, 0] = (float)i;
                aattack[0, 1] = (float)location[1];
                break;
            }
            if (i == 7)
            {
                break;
            }
        }

        for (int i = location[1] + 1; i < 8; i++)
        {
            if (pieceId.Contains("b") && matran[location[0], i].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0], i].Contains("w"))
            {
                break;
            }

            if (pieceId.Contains("w") && matran[location[0], i].Contains("b"))
            {
                aattack[1, 0] = (float)location[0];
                aattack[1, 1] = (float)i;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0], i].Contains("w"))
            {
                aattack[1, 0] = (float)location[0];
                aattack[1, 1] = (float)i;
                break;
            }
            if (i == 7)
            {
                break;
            }
        }

        for (int i = location[1] - 1; i >= 0; i--)
        {
            if (pieceId.Contains("b") && matran[location[0], i].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0], i].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0], i].Contains("b"))
            {
                aattack[2, 0] = (float)location[0];
                aattack[2, 1] = (float)i;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0], i].Contains("w"))
            {
                aattack[2, 0] = (float)location[0];
                aattack[2, 1] = (float)i;
                break;
            }
            if (i == 0)
            {
                break;
            }
        }

        for (int i = location[0] - 1; i >= 0; i--)
        {
            if (pieceId.Contains("w") && matran[i, location[1]].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("b") && matran[i, location[1]].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[i, location[1]].Contains("b"))
            {
                aattack[3, 0] = (float)i;
                aattack[3, 1] = (float)location[1];
                break;
            }
            if (pieceId.Contains("b") && matran[i, location[1]].Contains("w"))
            {
                aattack[3, 0] = (float)i;
                aattack[3, 1] = (float)location[1];
                break;
            }
            if (i == 0)
            {
                break;
            }
        }
    }

    private void QueenAttackScan(int x, int y, string id)
    {
        int[] location = new int[2];
        location[0] = x;
        location[1] = y;
        string pieceId = id;

        for (int i = location[0] + 1; i < 8; i++)
        {
            if (pieceId.Contains("b") && matran[i, location[1]].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[i, location[1]].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[i, location[1]].Contains("b"))
            {
                aattack[countA, 0] = (float)i;
                aattack[countA, 1] = (float)location[1];
                countA++;
                break;
            }
            if (pieceId.Contains("b") && matran[i, location[1]].Contains("w"))
            {
                aattack[countA, 0] = (float)i;
                aattack[countA, 1] = (float)location[1];
                countA++;
                break;
            }
            if (i == 7)
            {
                break;
            }
        }

        for (int i = location[1] + 1; i < 8; i++)
        {
            if (pieceId.Contains("b") && matran[location[0], i].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0], i].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0], i].Contains("b"))
            {
                aattack[countA, 0] = (float)location[0];
                aattack[countA, 1] = (float)i;
                countA++;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0], i].Contains("w"))
            {
                aattack[countA, 0] = (float)location[0];
                aattack[countA, 1] = (float)i;
                countA++;
                break;
            }
            if (i == 7)
            {
                break;
            }
        }

        for (int i = location[1] - 1; i >= 0; i--)
        {
            if (pieceId.Contains("b") && matran[location[0], i].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0], i].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0], i].Contains("b"))
            {
                aattack[countA, 0] = (float)location[0];
                aattack[countA, 1] = (float)i;
                countA++;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0], i].Contains("w"))
            {
                aattack[countA, 0] = (float)location[0];
                aattack[countA, 1] = (float)i;
                countA++;
                break;
            }
            if (i == 0)
            {
                break;
            }
        }

        for (int i = location[0] - 1; i >= 0; i--)
        {
            if (pieceId.Contains("w") && matran[i, location[1]].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("b") && matran[i, location[1]].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[i, location[1]].Contains("b"))
            {
                aattack[countA, 0] = (float)i;
                aattack[countA, 1] = (float)location[1];
                countA++;
                break;
            }
            if (pieceId.Contains("b") && matran[i, location[1]].Contains("w"))
            {
                aattack[countA, 0] = (float)i;
                aattack[countA, 1] = (float)location[1];
                countA++;
                break;
            }
            if (i == 0)
            {
                break;
            }
        }

        for (int i = 1; i < 8; i++)
        {
            if (location[0] + i > 7 || location[1] + i > 7)
            {
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] + i, location[1] + i].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] + i, location[1] + i].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] + i, location[1] + i].Contains("b"))
            {
                aattack[countA, 0] = (float)location[0] + i;
                aattack[countA, 1] = (float)location[1] + i;
                countA++;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] + i, location[1] + i].Contains("w"))
            {
                aattack[countA, 0] = (float)location[0] + i;
                aattack[countA, 1] = (float)location[1] + i;
                countA++;
                break;
            }
        }

        for (int i = 1; i < 8; i++)
        {
            if (location[0] + i > 7 || location[1] - i < 0)
            {
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] + i, location[1] - i].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] + i, location[1] - i].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] + i, location[1] - i].Contains("b"))
            {
                aattack[countA, 0] = (float)location[0] + i;
                aattack[countA, 1] = (float)location[1] - i;
                countA++;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] + i, location[1] - i].Contains("w"))
            {
                aattack[countA, 0] = (float)location[0] + i;
                aattack[countA, 1] = (float)location[1] - i;
                countA++;
                break;
            }
        }

        for (int i = 1; i < 8; i++)
        {
            if (location[0] - i < 0 || location[1] + i > 7)
            {
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] - i, location[1] + i].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] - i, location[1] + i].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] - i, location[1] + i].Contains("b"))
            {
                aattack[countA, 0] = (float)location[0] - i;
                aattack[countA, 1] = (float)location[1] + i;
                countA++;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] - i, location[1] + i].Contains("w"))
            {
                aattack[countA, 0] = (float)location[0] - i;
                aattack[countA, 1] = (float)location[1] + i;
                countA++;
                break;
            }
        }

        for (int i = 1; i < 8; i++)
        {
            if (location[0] - i < 0 || location[1] - i < 0)
            {
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] - i, location[1] - i].Contains("b"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] - i, location[1] - i].Contains("w"))
            {
                break;
            }
            if (pieceId.Contains("w") && matran[location[0] - i, location[1] - i].Contains("b"))
            {
                aattack[countA, 0] = (float)location[0] - i;
                aattack[countA, 1] = (float)location[1] - i;
                countA++;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] - i, location[1] - i].Contains("w"))
            {
                aattack[countA, 0] = (float)location[0] - i;
                aattack[countA, 1] = (float)location[1] - i;
                countA++;
                break;
            }
        }
    }

    private void KingAttackScan(int x, int y, string id)
    {
        int[] location = new int[2];
        location[0] = x;
        location[1] = y;
        string pieceId = id;

        if (location[0] + 1 < 8)
        {
            if (matran[location[0] + 1, location[1]].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0] + 1;
                aattack[countA, 1] = location[1];
                countA++;
            }
            if (matran[location[0] + 1, location[1]].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0] + 1;
                aattack[countA, 1] = location[1];
                countA++;
            }
        }

        if (location[1] + 1 < 8)
        {
            if (matran[location[0], location[1] + 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0];
                aattack[countA, 1] = location[1] + 1;
                countA++;
            }
            if (matran[location[0], location[1] + 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0];
                aattack[countA, 1] = location[1] + 1;
                countA++;
            }
        }

        if (location[0] + 1 < 8 && location[1] + 1 < 8)
        {
            if (matran[location[0] + 1, location[1] + 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0] + 1;
                aattack[countA, 1] = location[1] + 1;
                countA++;
            }
            if (matran[location[0] + 1, location[1] + 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0] + 1;
                aattack[countA, 1] = location[1] + 1;
                countA++;
            }
        }

        if (location[0] - 1 >= 0)
        {
            if (matran[location[0] - 1, location[1]].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0] - 1;
                aattack[countA, 1] = location[1];
                countA++;
            }
            if (matran[location[0] - 1, location[1]].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0] - 1;
                aattack[countA, 1] = location[1];
                countA++;
            }
        }

        if (location[0] - 1 >= 0 && location[1] + 1 < 8)
        {
            if (matran[location[0] - 1, location[1] + 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0] - 1;
                aattack[countA, 1] = location[1] + 1;
                countA++;
            }
            if (matran[location[0] - 1, location[1] + 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0] - 1;
                aattack[countA, 1] = location[1] + 1;
                countA++;
            }
        }

        if (location[1] - 1 >= 0)
        {
            if (matran[location[0], location[1] - 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0];
                aattack[countA, 1] = location[1] - 1;
                countA++;
            }
            if (matran[location[0], location[1] - 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0];
                aattack[countA, 1] = location[1] - 1;
                countA++;
            }
        }

        if (location[0] - 1 >= 0 && location[1] - 1 >= 0)
        {
            if (matran[location[0] - 1, location[1] - 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[6, 0] = location[0] - 1;
                aattack[6, 1] = location[1] - 1;
            }
            if (matran[location[0] - 1, location[1] - 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[6, 0] = location[0] - 1;
                aattack[6, 1] = location[1] - 1;
            }
        }

        if (location[0] + 1 < 8 && location[1] - 1 >= 0)
        {
            if (matran[location[0] + 1, location[1] - 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[countA, 0] = location[0] + 1;
                aattack[countA, 1] = location[1] - 1;
                countA++;
            }
            if (matran[location[0] + 1, location[1] - 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[countA, 0] = location[0] + 1;
                aattack[countA, 1] = location[1] - 1;
                countA++;
            }
        }
    }

    public int[] KingCheck(string[,] mt)
    {
        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j< 8; j++)
            {
                matran[i, j] = mt[i,j];
            }
        }

        for (int i = 0; i < 100; i++)
        {
            aattack[i, 0] = -2;
            aattack[i, 1] = -2;
        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (matran[i, j].Contains("p"))
                {
                    PawnAttackScan(i, j, matran[i, j]);
                }

                if (matran[i, j].Contains("s"))
                {
                    BishopAttackScan(i, j, matran[i, j]);
                }

                if (matran[i, j].Contains("n"))
                {
                    KnightAttackScan(i, j, matran[i, j]);
                }

                if (matran[i, j].Contains("r"))
                {
                    RookAttackScan(i, j, matran[i, j]);
                }

                if (matran[i, j].Contains("q"))
                {
                    QueenAttackScan(i, j, matran[i, j]);
                }

                if (matran[i, j].Contains("k"))
                {
                    KingAttackScan(i, j, matran[i, j]);
                }
            }
        }

        for (int i = 0; i < 100; i++)
        {
            int x = (int)aattack[i, 0];
            int y = (int)aattack[i, 1];

            if (x == -2 && y == -2)
            {
                break;
            }

            if (matran[x, y] == "kw")
            {
                Kq[0] = 1;
                Debug.Log("kw check");
            }

            if (matran[x, y] == "kb")
            {
                Kq[1] = 1;
                Debug.Log("kb check");
            }
        }

        return Kq;
    }

    internal class MoveInfo
    {
        private string pieceId;
        private float[,] moves;

        public MoveInfo()
        {
            pieceId = " ";
            moves = new float[1, 2];
            for (int i = 0; i < 2; i++)
            {
                moves[0, i] = -1;
            }
        }

        public MoveInfo(string id, float[,] move)
        {
            this.PieceId = id;
            this.Moves = move;
        }

        public string PieceId { get => pieceId; set => pieceId = value; }
        public float[,] Moves { get => moves; set => moves = value; }
    }
}
