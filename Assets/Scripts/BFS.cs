using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class BFS
{
    internal class BFSALg
    {
        private string[,] matran = new string[8, 8];
        private MoveScan.MoveInfo moveInfo;
        private MoveScan.MoveInfo[] moveInfoList = new MoveScan.MoveInfo[200];

        public string[,] Matran { get => matran; set => matran = value; }

        public MoveScan.MoveInfo BFS_Search()
        {
            MoveScan.MoveInfo move = new MoveScan.MoveInfo();

            for (int i = 0; i < 200; i++)
            {
                MoveScan.MoveInfo m = new MoveScan.MoveInfo();
                moveInfoList[i] = m;
            }

            FindMove(matran);
            int count = 0;
            string[,] mta = matran;
            mta = ApplyMove(mta, moveInfoList[0]);
            int tam = Heuristic(mta, moveInfoList[0].PieceId);

            for (int i = 1; i < 200; i++) 
            {
                if (moveInfoList[i].PieceId != " ")
                {
                    string[,] mttam = matran;
                    mttam = ApplyMove(mttam, moveInfoList[i]);
                    int h = Heuristic(mttam, moveInfoList[i].PieceId);
                    if (h < tam)
                    {
                        tam = h;
                        count = i;
                    }
                }
                else
                {
                    break;
                }
            }

            move = moveInfoList[count];

            return move;
        }

        private void FindMove(string[,] matran)
        {
            int tam = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (matran[i, j].Contains("pb"))
                    {
                        tam = PawnMoveScan(i, j, matran[i, j], tam);
                    }

                    if (matran[i, j].Contains("sb"))
                    {
                        tam = BishopMoveScan(i, j, matran[i, j], tam);
                    }

                    if (matran[i, j].Contains("nb"))
                    {
                        tam = KnightMoveScan(i, j, matran[i, j], tam);
                    }

                    if (matran[i, j].Contains("rb"))
                    {
                        tam = RookMoveScan(i, j, matran[i, j], tam);
                    }

                    if (matran[i, j].Contains("qb"))
                    {
                        tam = QueenMoveScan(i, j, matran[i, j], tam);
                    }

                    if (matran[i, j].Contains("kb"))
                    {
                        tam = KingMoveScan(i, j, matran[i, j], tam);
                    }
                }
            }
        }

        private int PawnMoveScan(int x, int y, string id, int t)
        {
            int[] location = new int[2];
            string pieceId = id;
            float[,] move = new float[1, 2];
            location[0] = x;
            location[1] = y;
            int tam = t;

            if (location[0] == 1 && pieceId.Contains("b"))
            {
                if (matran[location[0] + 1, location[1]] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] + 2, location[1]] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 2;
                    move[0, 1] = location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[0] != 1 && pieceId.Contains("b"))
            {
                if (location[0] + 1 < 8 && (matran[location[0] + 1, location[1]] == "0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (id.Contains("b"))
            {
                if (location[0] + 1 < 8 && location[1] - 1 >= 0 && matran[location[0] + 1, location[1] - 1].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1] - 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }

                if (location[0] + 1 < 8 && location[1] + 1 < 8 && matran[location[0] + 1, location[1] + 1].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1] + 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            return tam;
        }

        private int RookMoveScan(int x, int y, string id, int t)
        {
            float[,] move = new float[1, 2];
            int[] location = new int[2];
            location[0] = x;
            location[1] = y;
            string pieceId = id;
            int tam = t;

            for (int i = location[0] + 1; i < 8; i++)
            {
                if (id.Contains("b") && matran[i, location[1]].Contains("b"))
                {
                    break;
                }
                if (matran[i, location[1]] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)i;
                    move[0, 1] = (float)location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[i, location[1]].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)i;
                    move[0, 1] = (float)location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
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
                if (matran[location[0], i] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0];
                    move[0, 1] = (float)i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[location[0], i].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0];
                    move[0, 1] = (float)i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
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
                if (matran[location[0], i] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0];
                    move[0, 1] = (float)i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[location[0], i].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0];
                    move[0, 1] = (float)i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                    break;
                }
                if (i == 0)
                {
                    break;
                }
            }

            for (int i = location[0] - 1; i >= 0; i--)
            {
                if (pieceId.Contains("b") && matran[i, location[1]].Contains("b"))
                {
                    break;
                }
                if (matran[i, location[1]] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)i;
                    move[0, 1] = (float)location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[i, location[1]].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)i;
                    move[0, 1] = (float)location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                    break;
                }
                if (i == 0)
                {
                    break;
                }
            }

            return tam;
        }

        private int KnightMoveScan(int x, int y, string id, int t)
        {
            int[] location = new int[2];
            float[,] move = new float[1, 2];
            location[0] = x;
            location[1] = y;
            string pieceId = id;
            int tam = t;

            if (location[0] + 2 < 8 && location[1] + 1 < 8)
            {
                if (matran[location[0] + 2, location[1] + 1].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 2;
                    move[0, 1] = location[1] + 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] + 2, location[1] + 1].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 2;
                    move[0, 1] = location[1] + 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[0] + 2 < 8 && location[1] - 1 >= 0)
            {
                if (matran[location[0] + 2, location[1] - 1].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 2;
                    move[0, 1] = location[1] - 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] + 2, location[1] - 1].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 2;
                    move[0, 1] = location[1] - 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[0] + 1 < 8 && location[1] + 2 < 8)
            {
                if (matran[location[0] + 1, location[1] + 2].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1] + 2;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] + 1, location[1] + 2].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1] + 2;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[0] - 1 >= 0 && location[1] + 2 < 8)
            {
                if (matran[location[0] - 1, location[1] + 2].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 1;
                    move[0, 1] = location[1] + 2;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] - 1, location[1] + 2].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 1;
                    move[0, 1] = location[1] + 2;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[0] - 2 >= 0 && location[1] + 1 < 8)
            {
                if (matran[location[0] - 2, location[1] + 1].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 2;
                    move[0, 1] = location[1] + 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] - 2, location[1] + 1].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 2;
                    move[0, 1] = location[1] + 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[0] - 2 >= 0 && location[1] - 1 >= 0)
            {
                if (matran[location[0] - 2, location[1] - 1].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 2;
                    move[0, 1] = location[1] - 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] - 2, location[1] - 1].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 2;
                    move[0, 1] = location[1] - 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[0] + 1 < 8 && location[1] - 2 >= 0)
            {
                if (matran[location[0] + 1, location[1] - 2].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1] - 2;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] + 1, location[1] - 2].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1] - 2;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[0] - 1 >= 0 && location[1] - 2 >= 0)
            {
                if (matran[location[0] - 1, location[1] - 2].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 1;
                    move[0, 1] = location[1] - 2;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] - 1, location[1] - 2].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 1;
                    move[0, 1] = location[1] - 2;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            return tam;
        }

        private int BishopMoveScan(int x, int y, string id, int t)
        {
            int[] location = new int[2];
            location[0] = x;
            location[1] = y;
            float[,] move = new float[1, 2];
            string pieceId = id;
            int tam = t;

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
                if (matran[location[0] + i, location[1] + i] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] + i;
                    move[0, 1] = (float)location[1] + i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[location[0] + i, location[1] + i].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] + i;
                    move[0, 1] = (float)location[1] + i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
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
                if (matran[location[0] + i, location[1] - i] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] + i;
                    move[0, 1] = (float)location[1] - i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[location[0] + i, location[1] - i].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] + i;
                    move[0, 1] = (float)location[1] - i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
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
                if (matran[location[0] - i, location[1] + i] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] - i;
                    move[0, 1] = (float)location[1] + i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[location[0] - i, location[1] + i].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] - i;
                    move[0, 1] = (float)location[1] + i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
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
                if (matran[location[0] - i, location[1] - i] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] - i;
                    move[0, 1] = (float)location[1] - i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[location[0] - i, location[1] - i].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] - i;
                    move[0, 1] = (float)location[1] - i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                    break;
                }
            }

            return tam;
        }

        private int QueenMoveScan(int x, int y, string id, int t)
        {
            float[,] move = new float[1, 2];
            int[] location = new int[2];
            location[0] = x;
            location[1] = y;
            string pieceId = id;
            int tam = t;

            for (int i = location[0] + 1; i < 8; i++)
            {
                if (pieceId.Contains("b") && matran[i, location[1]].Contains("b"))
                {
                    break;
                }
                if (matran[i, location[1]] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)i;
                    move[0, 1] = (float)location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[i, location[1]].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)i;
                    move[0, 1] = (float)location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
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
                if (matran[location[0], i] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0];
                    move[0, 1] = (float)i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[location[0], i].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0];
                    move[0, 1] = (float)i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
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
                if (matran[location[0], i] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0];
                    move[0, 1] = (float)i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[location[0], i].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0];
                    move[0, 1] = (float)i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                    break;
                }
                if (i == 0)
                {
                    break;
                }
            }

            for (int i = location[0] - 1; i >= 0; i--)
            {
                if (pieceId.Contains("b") && matran[i, location[1]].Contains("b"))
                {
                    break;
                }
                if (matran[i, location[1]] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)i;
                    move[0, 1] = (float)location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[i, location[1]].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)i;
                    move[0, 1] = (float)location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
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
                if (matran[location[0] + i, location[1] + i] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] + i;
                    move[0, 1] = (float)location[1] + i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[location[0] + i, location[1] + i].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] + i;
                    move[0, 1] = (float)location[1] + i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
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
                if (matran[location[0] + i, location[1] - i] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] + i;
                    move[0, 1] = (float)location[1] - i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[location[0] + i, location[1] - i].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] + i;
                    move[0, 1] = (float)location[1] - i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
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
                if (matran[location[0] - i, location[1] + i] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] - i;
                    move[0, 1] = (float)location[1] + i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[location[0] - i, location[1] + i].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] - i;
                    move[0, 1] = (float)location[1] + i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
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
                if (matran[location[0] - i, location[1] - i] == "0")
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] - i;
                    move[0, 1] = (float)location[1] - i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (pieceId.Contains("b") && matran[location[0] - i, location[1] - i].Contains("w"))
                {
                    move = new float[1, 2];
                    move[0, 0] = (float)location[0] - i;
                    move[0, 1] = (float)location[1] - i;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                    break;
                }
            }

            return tam;
        }

        private int KingMoveScan(int x, int y, string id, int t)
        {
            float[,] move = new float[1, 2];
            int[] location = new int[2];
            location[0] = x;
            location[1] = y;
            string pieceId = id;
            int tam = t;

            if (location[0] + 1 < 8)
            {
                if (matran[location[0] + 1, location[1]].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] + 1, location[1]].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[1] + 1 < 8)
            {
                if (matran[location[0], location[1] + 1].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0];
                    move[0, 1] = location[1] + 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0], location[1] + 1].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0];
                    move[0, 1] = location[1] + 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[0] + 1 < 8 && location[1] + 1 < 8)
            {
                if (matran[location[0] + 1, location[1] + 1].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1] + 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] + 1, location[1] + 1].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1] + 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[0] - 1 >= 0)
            {
                if (matran[location[0] - 1, location[1]].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 1;
                    move[0, 1] = location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] - 1, location[1]].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 1;
                    move[0, 1] = location[1];
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[0] - 1 >= 0 && location[1] + 1 < 8)
            {
                if (matran[location[0] - 1, location[1] + 1].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 1;
                    move[0, 1] = location[1] + 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] - 1, location[1] + 1].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 1;
                    move[0, 1] = location[1] + 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[1] - 1 >= 0)
            {
                if (matran[location[0], location[1] - 1].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0];
                    move[0, 1] = location[1] - 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0], location[1] - 1].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0];
                    move[0, 1] = location[1] - 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[0] - 1 >= 0 && location[1] - 1 >= 0)
            {
                if (matran[location[0] - 1, location[1] - 1].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 1;
                    move[0, 1] = location[1] - 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] - 1, location[1] - 1].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] - 1;
                    move[0, 1] = location[1] - 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            if (location[0] + 1 < 8 && location[1] - 1 >= 0)
            {
                if (matran[location[0] + 1, location[1] - 1].Contains("0"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1] - 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
                if (matran[location[0] + 1, location[1] - 1].Contains("w") && pieceId.Contains("b"))
                {
                    move = new float[1, 2];
                    move[0, 0] = location[0] + 1;
                    move[0, 1] = location[1] - 1;
                    MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo(id, move);
                    moveInfoList[tam] = moveInfo;
                    tam++;
                }
            }

            return tam;
        }

        private string[,] ApplyMove(string[,] mt, MoveScan.MoveInfo mi)
        {
            string[,] move = mt;
            int x = (int)mi.Moves[0, 0];
            int y = (int)mi.Moves[0, 1];
            int k = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (move[i, j] == mi.PieceId)
                    {
                        move[i, j] = "0";
                        k = 1;
                        break;
                    }
                }
                if (k == 1)
                {
                    break;
                }
            }

            move[x, y] = mi.PieceId;

            return move;
        }

        private int Heuristic(string[,] mt, string id)
        {
            int h = 0;
            int[,] kingwloc = new int[1, 2];
            kingwloc[0, 0] = -1;
            kingwloc[0, 1] = -1;
            int[,] curloc = new int[1, 2];

            for (int i = 0; i < 8; i++)
            {
                int stop = 1;
                for (int j = 0; j < 8; j++)
                {
                    if (mt[i, j].Contains("kw"))
                    {
                        stop = stop - 1;
                        kingwloc[0, 0] = i;
                        kingwloc[0, 1] = j;
                    }
                    if (mt[i, j].Contains(id))
                    {
                        stop = stop - 1;
                        curloc[0, 0] = i;
                        curloc[0, 1] = j;
                    }
                    if (stop == -1)
                    {
                        break;
                    }
                }
                if (stop == -1)
                {
                    break;
                }
            }

            if (kingwloc[0, 0] == -1 && kingwloc[0, 1] == -1)
            {
                h = -1000;
                return h;
            } else
            {
                h = Math.Abs(kingwloc[0, 0] - curloc[0, 0]) + Math.Abs(kingwloc[0, 1] - curloc[0, 1]);

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (mt[i, j].Contains("qw"))
                        {
                            h = h + 9;
                        }
                        if (mt[i, j].Contains("rw"))
                        {
                            h = h + 5;
                        }
                        if (mt[i, j].Contains("sw"))
                        {
                            h = h + 3;
                        }
                        if (mt[i, j].Contains("nw"))
                        {
                            h = h + 3;
                        }
                        if (mt[i, j].Contains("pw"))
                        {
                            h = h + 1;
                        }
                    }
                }

                if (id.Contains("k"))
                {
                    h = h + 20;
                }

                if (id.Contains("q"))
                {
                    h = h + 5;
                }

                if (id.Contains("r"))
                {
                    h = h + 2;
                }

                if (id.Contains("s"))
                {
                    h = h + 1;
                }

                if (id.Contains("n"))
                {
                    h = h + 1;
                }

                return h;
            }
        }
    }
}
