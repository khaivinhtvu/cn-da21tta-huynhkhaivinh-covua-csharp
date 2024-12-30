using System;
using System.IO;
using UnityEngine;

public class KingScript : MonoBehaviour
{
    [SerializeField] private string pieceId;
    private string[,] matran = new string[8, 8];
    private int[] location = new int[2];
    private float positionx;
    private float positiony;
    private float positionz;
    private float[,] amove;
    private float[,] aattack;
    public delegate void MoveInfo(int[] location, float[,] amove, float x, float y, string pieceId, float[,] aattack);
    public static event MoveInfo ShowKingMoveInfo;
    public delegate void KingDead(string pieceId);
    public static event KingDead KingDestroyed;

    private void BroadMatrix()
    {
        //Đọc tập tin chessbroad.txt
        string path = Application.dataPath + "/chessbroad.txt";
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

        //Tìm vị trí hiện tại của quân cờ
        for (int i = 0; i < 8; i++)
        {
            int stop = 0;
            for (int j = 0; j < 8; j++)
            {
                if (matran[i, j] == pieceId)
                {
                    location[0] = i;
                    location[1] = j;
                    stop = 1;
                    break;
                }
            }
            if (stop == 1)
            {
                break;
            }
        }
    }

    private void MoveScan()
    {
        //Tìm đường đi hợp lệ

        amove = new float[8, 2];
        aattack = new float[8, 2];

        for (int k = 0; k < 8; k++)
        {
            amove[k, 0] = -1;
            amove[k, 1] = -1;
        }

        for (int k = 0; k < 8; k++)
        {
            aattack[k, 0] = -1;
            aattack[k, 1] = -1;
        }

        if (location[0] + 1 < 8)
        {
            if (matran[location[0] + 1, location[1]].Contains("0"))
            {
                amove[0, 0] = location[0] + 1;
                amove[0, 1] = location[1];
            }
            if (matran[location[0] + 1, location[1]].Contains("b") && pieceId.Contains("w"))
            {
                aattack[0, 0] = location[0] + 1;
                aattack[0, 1] = location[1];
            }
            if (matran[location[0] + 1, location[1]].Contains("w") && pieceId.Contains("b"))
            {
                aattack[0, 0] = location[0] + 1;
                aattack[0, 1] = location[1];
            }
        }

        if (location[1] + 1 < 8)
        {
            if (matran[location[0], location[1] + 1].Contains("0"))
            {
                amove[1, 0] = location[0];
                amove[1, 1] = location[1] + 1;
            }
            if (matran[location[0], location[1] + 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[1, 0] = location[0];
                aattack[1, 1] = location[1] + 1;
            }
            if (matran[location[0], location[1] + 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[1, 0] = location[0];
                aattack[1, 1] = location[1] + 1;
            }
        }

        if (location[0] + 1 < 8 && location[1] + 1 < 8)
        {
            if (matran[location[0] + 1, location[1] + 1].Contains("0"))
            {
                amove[2, 0] = location[0] + 1;
                amove[2, 1] = location[1] + 1;
            }
            if (matran[location[0] + 1, location[1] + 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[2, 0] = location[0] + 1;
                aattack[2, 1] = location[1] + 1;
            }
            if (matran[location[0] + 1, location[1] + 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[2, 0] = location[0] + 1;
                aattack[2, 1] = location[1] + 1;
            }
        }

        if (location[0] - 1 >= 0)
        {
            if (matran[location[0] - 1, location[1]].Contains("0"))
            {
                amove[3, 0] = location[0] - 1;
                amove[3, 1] = location[1];
            }
            if (matran[location[0] - 1, location[1]].Contains("b") && pieceId.Contains("w"))
            {
                aattack[3, 0] = location[0] - 1;
                aattack[3, 1] = location[1];
            }
            if (matran[location[0] - 1, location[1]].Contains("w") && pieceId.Contains("b"))
            {
                aattack[3, 0] = location[0] - 1;
                aattack[3, 1] = location[1];
            }
        }

        if (location[0] - 1 >= 0 && location[1] + 1 < 8)
        {
            if (matran[location[0] - 1, location[1] + 1].Contains("0"))
            {
                amove[4, 0] = location[0] - 1;
                amove[4, 1] = location[1] + 1;
            }
            if (matran[location[0] - 1, location[1] + 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[4, 0] = location[0] - 1;
                aattack[4, 1] = location[1] + 1;
            }
            if (matran[location[0] - 1, location[1] + 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[4, 0] = location[0] - 1;
                aattack[4, 1] = location[1] + 1;
            }
        }

        if (location[1] - 1 >= 0)
        {
            if (matran[location[0], location[1] - 1].Contains("0"))
            {
                amove[5, 0] = location[0];
                amove[5, 1] = location[1] - 1;
            }
            if (matran[location[0], location[1] - 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[5, 0] = location[0];
                aattack[5, 1] = location[1] - 1;
            }
            if (matran[location[0], location[1] - 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[5, 0] = location[0];
                aattack[5, 1] = location[1] - 1;
            }
        }

        if (location[0] - 1 >= 0 && location[1] - 1 >= 0)
        {
            if (matran[location[0] - 1, location[1] - 1].Contains("0"))
            {
                amove[6, 0] = location[0] - 1;
                amove[6, 1] = location[1] - 1;
            }
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
            if (matran[location[0] + 1, location[1] - 1].Contains("0"))
            {
                amove[7, 0] = location[0] + 1;
                amove[7, 1] = location[1] - 1;
            }
            if (matran[location[0] + 1, location[1] - 1].Contains("b") && pieceId.Contains("w"))
            {
                aattack[7, 0] = location[0] + 1;
                aattack[7, 1] = location[1] - 1;
            }
            if (matran[location[0] + 1, location[1] - 1].Contains("w") && pieceId.Contains("b"))
            {
                aattack[7, 0] = location[0] + 1;
                aattack[7, 1] = location[1] - 1;
            }
        }

        RaiseMoveInfo();
    }

    private void RaiseMoveInfo()
    {
        positionx = transform.position.x;
        positiony = transform.position.y;
        ShowKingMoveInfo?.Invoke(location, amove, positionx, positiony, pieceId, aattack);
    }

    private void PieceMoved(float x, float y, string newpieceId)
    {
        if (pieceId == newpieceId)
        {
            Vector3 m = new Vector3(x - transform.position.x, y - transform.position.y);

            transform.Translate(m);
        }
    }

    private void PieceAttacked(float x, float y, string newpieceId, string newattackId)
    {
        if (pieceId == newpieceId)
        {
            Vector3 m = new Vector3(x - transform.position.x, y - transform.position.y);

            transform.Translate(m);
        }
        else if (pieceId == newattackId)
        {
            UnityEngine.Object.Destroy(gameObject);
            GameScript.RaisePieceMoved -= PieceMoved;
            GameScript.RaisePieceAttacked -= PieceAttacked;
            GameScript.RaiseAiMoved -= PieceAttacked;
            KingDestroyed?.Invoke(pieceId);
            Debug.Log(pieceId + " destroyed");
        }
    }

    private void Start()
    {
        GameScript.RaisePieceMoved += PieceMoved;
        GameScript.RaisePieceAttacked += PieceAttacked;
        GameScript.RaiseAiMoved += PieceAttacked;
    }

    private void OnDestroy()
    {
        GameScript.RaisePieceMoved -= PieceMoved;
        GameScript.RaisePieceAttacked -= PieceAttacked;
        GameScript.RaiseAiMoved -= PieceAttacked;
    }

    private void OnMouseDown()
    {
        Debug.Log("king clicked");
        BroadMatrix();
        MoveScan();
    }
}
