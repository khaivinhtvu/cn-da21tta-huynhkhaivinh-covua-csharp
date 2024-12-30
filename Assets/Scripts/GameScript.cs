using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class GameScript : MonoBehaviour
{
    private int[] curlocation;
    private string curturn = "w";
    private float[,] curamove;
    private float[,] curaattack;
    private float curpositionx;
    private float curpositiony;
    private string curpieceId = " ";
    private string[,] matran = new string[8, 8];
    private string path = Application.dataPath + "/chessbroad.txt";
    [SerializeField] private GameObject selectsquare;
    [SerializeField] private GameObject movesquare;
    [SerializeField] private GameObject attacksquare;
    public delegate void PieceMoved(float x, float y, string pieceId);
    public static event PieceMoved RaisePieceMoved;
    public delegate void ClearAllSquare();
    public static event ClearAllSquare RaiseClearAllSquare;
    public delegate void PieceAttacked(float x, float y, string pieceId, string attackId);
    public static event PieceAttacked RaisePieceAttacked;
    public delegate void CallAi();
    public static event CallAi CallAiMove;
    public delegate void AiMoved(float x, float y, string pieceId, string attackId);
    public static event AiMoved RaiseAiMoved;

    private void InitBroad()
    {
        if (File.Exists(path))
        {
            if (new FileInfo(path).Length == 0)
            {
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine("rb1 nb1 sb1 qb kb sb2 nb2 rb2");
                writer.WriteLine("pb1 pb2 pb3 pb4 pb5 pb6 pb7 pb8");
                writer.WriteLine("0 0 0 0 0 0 0 0");
                writer.WriteLine("0 0 0 0 0 0 0 0");
                writer.WriteLine("0 0 0 0 0 0 0 0");
                writer.WriteLine("0 0 0 0 0 0 0 0");
                writer.WriteLine("pw1 pw2 pw3 pw4 pw5 pw6 pw7 pw8");
                writer.WriteLine("rw1 nw1 sw1 qw kw sw2 nw2 rw2");
                writer.Close();
                Debug.Log("write into empty file");
            }
            else
            {
                File.WriteAllText(path, String.Empty);
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine("rb1 nb1 sb1 qb kb sb2 nb2 rb2");
                writer.WriteLine("pb1 pb2 pb3 pb4 pb5 pb6 pb7 pb8");
                writer.WriteLine("0 0 0 0 0 0 0 0");
                writer.WriteLine("0 0 0 0 0 0 0 0");
                writer.WriteLine("0 0 0 0 0 0 0 0");
                writer.WriteLine("0 0 0 0 0 0 0 0");
                writer.WriteLine("pw1 pw2 pw3 pw4 pw5 pw6 pw7 pw8");
                writer.WriteLine("rw1 nw1 sw1 qw kw sw2 nw2 rw2");
                writer.Close();
                Debug.Log("write into non-empty file");
            }
        }
        else
        {
            Debug.Log("no file");
        }
    }

    private void ReadBroad()
    {
        string[] line = File.ReadAllLines(path);

        for (int i = 0; i < 8; i++)
        {
            string linei = line[i].Trim();
            string[] arr = linei.Split(' ');
            for (int j = 0; j < 8; j++)
            {
                this.matran[i, j] = Convert.ToString(arr[j]);
            }
        }
    }

    private void ReceivePieceMoveInfo(int[] a, float[,] b, float c, float d, string e, float[,] f)
    {
        if ((e.Contains("w") && curturn != "w") || (e.Contains("b") && curturn != "b"))
        {
            Debug.Log("not your turn");
        }
        else if (curpieceId == e)
        {
            RaiseClearAllSquare?.Invoke();
            curpieceId = " ";
        }
        else
        {
            curlocation = a;
            curamove = b;
            curpositionx = c;
            curpositiony = d;
            curpieceId = e;
            curaattack = f;

            RaiseClearAllSquare?.Invoke();

            Instantiate(selectsquare, new Vector3(curpositionx, curpositiony, -3), transform.rotation);

            for (int i = 0; i < curamove.GetLength(0); i++)
            {
                if (curamove[i, 0] != -1 && curamove[i, 1] != -1)
                {
                    float x = (float)(curamove[i, 1] * 3 - 10.5);
                    float y = (float)(curamove[i, 0] * (-3) + 10.5);
                    Instantiate(movesquare, new Vector3(x, y, -3), transform.rotation);
                }
            }

            for (int i = 0; i < curaattack.GetLength(0); i++)
            {
                if (curaattack[i, 0] != -1 && curaattack[i, 1] != -1)
                {
                    float x = (float)(curaattack[i, 1] * 3 - 10.5);
                    float y = (float)(curaattack[i, 0] * (-3) + 10.5);
                    Instantiate(attacksquare, new Vector3(x, y, -6), transform.rotation);
                }
            }
        }
    }

    private void PieceMove(float x, float y)
    {
        RaisePieceMoved?.Invoke(x, y, curpieceId);

        ReadBroad();

        int newi = (int)((y - 10.5) / (-3));
        int newj = (int)((x + 10.5) / 3);

        matran[curlocation[0], curlocation[1]] = "0";
        matran[newi, newj] = curpieceId;

        File.WriteAllText(path, String.Empty);
        StreamWriter writer = new StreamWriter(path, true);
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (j == 7)
                {
                    writer.Write(matran[i, j]);
                }
                else
                {
                    writer.Write(matran[i, j] + " ");
                }
            }
            writer.WriteLine();
        }
        writer.Close();

        RaiseClearAllSquare?.Invoke();
        if (curpieceId.Contains("w"))
        {
            curturn = "b";
            curpieceId = " ";
            CallAiMove?.Invoke();
        }
        else
        {
            curturn = "w";
            curpieceId = " ";
        }

        MoveScan ms = new MoveScan();
        ms.KingCheck(matran);
        Debug.Log(ms.Kq[0]);
        Debug.Log(ms.Kq[1]);
    }

    private void PieceAttack(float x, float y)
    {

        ReadBroad();

        int newi = (int)((y - 10.5) / (-3));
        int newj = (int)((x + 10.5) / 3);
        string curattack = matran[newi, newj];
        RaisePieceAttacked?.Invoke(x, y, curpieceId, curattack);

        matran[curlocation[0], curlocation[1]] = "0";
        matran[newi, newj] = curpieceId;

        File.WriteAllText(path, String.Empty);
        StreamWriter writer = new StreamWriter(path, true);
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (j == 7)
                {
                    writer.Write(matran[i, j]);
                }
                else
                {
                    writer.Write(matran[i, j] + " ");
                }
            }
            writer.WriteLine();
        }
        writer.Close();

        RaiseClearAllSquare?.Invoke();
        if (curpieceId.Contains("w"))
        {
            curturn = "b";
            curpieceId = " ";
            CallAiMove?.Invoke();
        }
        else
        {
            curturn = "w";
            curpieceId = " ";
        }


        MoveScan ms = new MoveScan();
        ms.KingCheck(matran);
        Debug.Log(ms.Kq[0]);
        Debug.Log(ms.Kq[1]);
    }

    private void ReceiveAiMoveInfo(string a, float[,] b)
    {
        ReadBroad();

        if (a.Contains(" ") || b[0, 0] == -1 || b[0, 1] == -1)
        {
            Debug.Log("error with ai result");
        }
        else
        {
            int it = (int)b[0, 0];
            int jt = (int)b[0, 1];
            int curi = 0;
            int curj = 0;
            float x = (float)(jt * 3 - 10.5);
            float y = (float)(it * (-3) + 10.5);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (matran[i, j] == a)
                    {
                        curi = i;
                        curj = j;
                        break;
                    }
                }
            }

            string attackId = "e";

            if (matran[it, jt] != "0")
            {
                attackId = matran[it, jt];
            }
            else
            {
                attackId = "0";
            }

            RaiseAiMoved(x, y, a, attackId);

            matran[curi, curj] = "0";
            matran[it, jt] = a;

            File.WriteAllText(path, String.Empty);
            StreamWriter writer = new StreamWriter(path, true);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j == 7)
                    {
                        writer.Write(matran[i, j]);
                    }
                    else
                    {
                        writer.Write(matran[i, j] + " ");
                    }
                }
                writer.WriteLine();
            }
            writer.Close();

            curturn = "w";
        }
    }

    void Start()
    {
        InitBroad();
        MoveSquareScript.RaiseMoveSquareClicked += PieceMove;
        AttackSquareScript.RaiseAttackSquareClicked += PieceAttack;
        RookScript.ShowRookMoveInfo += ReceivePieceMoveInfo;
        KnightScript.ShowKnightMoveInfo += ReceivePieceMoveInfo;
        PawnScript.ShowPawnMoveInfo += ReceivePieceMoveInfo;
        BishopScript.ShowBishopMoveInfo += ReceivePieceMoveInfo;
        QueenScript.ShowQueenMoveInfo += ReceivePieceMoveInfo;
        KingScript.ShowKingMoveInfo += ReceivePieceMoveInfo;
        GameAiScript.ShowAiMoveInfo += ReceiveAiMoveInfo;
    }

    private void OnDestroy()
    {
        MoveSquareScript.RaiseMoveSquareClicked -= PieceMove;
        AttackSquareScript.RaiseAttackSquareClicked -= PieceAttack;
        RookScript.ShowRookMoveInfo -= ReceivePieceMoveInfo;
        KnightScript.ShowKnightMoveInfo -= ReceivePieceMoveInfo;
        PawnScript.ShowPawnMoveInfo -= ReceivePieceMoveInfo;
        BishopScript.ShowBishopMoveInfo -= ReceivePieceMoveInfo;
        QueenScript.ShowQueenMoveInfo -= ReceivePieceMoveInfo;
        KingScript.ShowKingMoveInfo -= ReceivePieceMoveInfo;
        GameAiScript.ShowAiMoveInfo -= ReceiveAiMoveInfo;
    }
}
