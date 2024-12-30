using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class PawnScript : MonoBehaviour
{
    [SerializeField] private string pieceId;
    private string[,] matran = new string[8, 8];
    private int[] location = new int[2];
    private float positionx;
    private float positiony;
    private float positionz;
    private bool hasmoved = false;
    private float[,] amove;
    private float[,] aattack;
    public delegate void MoveInfo(int[] location, float[,] amove, float x, float y, string pieceId, float[,] aattack);
    public static event MoveInfo ShowPawnMoveInfo;

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
                    Debug.Log("found pawn location");
                    Debug.Log("pawn location: " + i + ", " + j);
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
        if (hasmoved == false && pieceId.Contains("w"))
        {
            amove = new float[2, 2];
            amove[0, 0] = -1;
            amove[0, 1] = -1;
            amove[1, 0] = -1;
            amove[1, 1] = -1;
            if (matran[location[0] - 1, location[1]] == "0")
            {
                amove[0, 0] = location[0] - 1;
                amove[0, 1] = location[1];
            }
            if (matran[location[0] - 2, location[1]] == "0")
            {
                amove[1, 0] = location[0] - 2;
                amove[1, 1] = location[1];
            }
        }

        if (hasmoved == false && pieceId.Contains("b"))
        {
            amove = new float[2, 2];
            amove[0, 0] = -1;
            amove[0, 1] = -1;
            amove[1, 0] = -1;
            amove[1, 1] = -1;
            if (matran[location[0] + 1, location[1]] == "0")
            {
                amove[0, 0] = location[0] + 1;
                amove[0, 1] = location[1];
            }
            if (matran[location[0] + 2, location[1]] == "0")
            {
                amove[1, 0] = location[0] + 2;
                amove[1, 1] = location[1];
            }
        }

        if (hasmoved == true && pieceId.Contains("b"))
        {
            amove = new float[1, 2];
            amove[0, 0] = -1;
            amove[0, 1] = -1;
            if (location[0] + 1 < 8 && (matran[location[0] + 1, location[1]] == "0"))
            {
                amove[0, 0] = location[0] + 1;
                amove[0, 1] = location[1];
            }
        }

        if (hasmoved == true && pieceId.Contains("w"))
        {
            amove = new float[1, 2];
            amove[0, 0] = -1;
            amove[0, 1] = -1;
            if (location[0] - 1 >= 0 && matran[location[0] - 1, location[1]] == "0")
            {
                amove[0, 0] = location[0] - 1;
                amove[0, 1] = location[1];
            }
        }

        if (pieceId.Contains("w"))
        {
            aattack = new float[2, 2];
            aattack[0, 0] = -1;
            aattack[0, 1] = -1;
            aattack[1, 0] = -1;
            aattack[1, 1] = -1;

            if (location[0] - 1 >= 0 && location[1] - 1 >= 0 && matran[location[0] - 1, location[1] - 1].Contains("b"))
            {
                aattack[0, 0] = location[0] - 1;
                aattack[0, 1] = location[1] - 1;
            }

            if (location[0] - 1 >= 0 && location[1] + 1 < 8 && matran[location[0] - 1, location[1] + 1].Contains("b"))
            {
                aattack[1, 0] = location[0] - 1;
                aattack[1, 1] = location[1] + 1;
            }

            RaiseMoveInfo();
        }

        if (pieceId.Contains("b"))
        {
            aattack = new float[2, 2];
            aattack[0, 0] = -1;
            aattack[0, 1] = -1;
            aattack[1, 0] = -1;
            aattack[1, 1] = -1;

            if (location[0] + 1 < 8 && location[1] - 1 >= 0 && matran[location[0] + 1, location[1] - 1].Contains("w"))
            {
                aattack[0, 0] = location[0] + 1;
                aattack[0, 1] = location[1] - 1;
            }

            if (location[0] + 1 < 8 && location[1] + 1 < 8 && matran[location[0] + 1, location[1] + 1].Contains("w"))
            {
                aattack[1, 0] = location[0] + 1;
                aattack[1, 1] = location[1] + 1;
            }

            RaiseMoveInfo();
        }
    }

    private void RaiseMoveInfo()
    {
        positionx = transform.position.x;
        positiony = transform.position.y;
        ShowPawnMoveInfo?.Invoke(location, amove, positionx, positiony, pieceId, aattack);
    }

    private void PieceMoved(float x, float y, string newpieceId)
    {
        if (pieceId == newpieceId)
        {
            hasmoved = true;
            Vector3 m = new Vector3(x - transform.position.x, y - transform.position.y);

            transform.Translate(m);
        }
    }

    private void PieceAttacked(float x, float y, string newpieceId, string newattackId)
    {
        if (pieceId == newpieceId)
        {
            hasmoved = true;
            Vector3 m = new Vector3(x - transform.position.x, y - transform.position.y);

            transform.Translate(m);
        }
        else if (pieceId == newattackId) 
        {
            UnityEngine.Object.Destroy(gameObject);
            GameScript.RaisePieceMoved -= PieceMoved;
            GameScript.RaisePieceAttacked -= PieceAttacked;
            GameScript.RaiseAiMoved -= PieceAttacked;
            Debug.Log(pieceId + "destroyed");
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
        Debug.Log("pawn clicked");
        BroadMatrix();
        MoveScan();
    }
}
