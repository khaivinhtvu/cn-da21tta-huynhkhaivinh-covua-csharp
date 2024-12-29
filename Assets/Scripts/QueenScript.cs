using System;
using System.IO;
using UnityEngine;

public class QueenScript : MonoBehaviour
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
    public static event MoveInfo ShowQueenMoveInfo;

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
                    Debug.Log("found queen location");
                    Debug.Log("queen location: " + i + ", " + j);
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

        amove = new float[28, 2];
        aattack = new float[8, 2];

        for (int k = 0; k < 28; k++)
        {
            amove[k, 0] = -1;
            amove[k, 1] = -1;
        }

        for (int k = 0; k < 8; k++)
        {
            aattack[k, 0] = -1;
            aattack[k, 1] = -1;
        }

        int count = 0;

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
            if (matran[i, location[1]] == "0")
            {
                amove[count, 0] = (float)i;
                amove[count, 1] = (float)location[1];
                count++;
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
            if (matran[location[0], i] == "0")
            {
                amove[count, 0] = (float)location[0];
                amove[count, 1] = (float)i;
                count++;
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
            if (matran[location[0], i] == "0")
            {
                amove[count, 0] = (float)location[0];
                amove[count, 1] = (float)i;
                count++;
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
            if (matran[i, location[1]] == "0")
            {
                amove[count, 0] = (float)i;
                amove[count, 1] = (float)location[1];
                count++;
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
            if (matran[location[0] + i, location[1] + i] == "0")
            {
                amove[count, 0] = (float)location[0] + i;
                amove[count, 1] = (float)location[1] + i;
                count++;
            }
            if (pieceId.Contains("w") && matran[location[0] + i, location[1] + i].Contains("b"))
            {
                aattack[4, 0] = (float)location[0] + i;
                aattack[4, 1] = (float)location[1] + i;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] + i, location[1] + i].Contains("w"))
            {
                aattack[4, 0] = (float)location[0] + i;
                aattack[4, 1] = (float)location[1] + i;
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
            if (matran[location[0] + i, location[1] - i] == "0")
            {
                amove[count, 0] = (float)location[0] + i;
                amove[count, 1] = (float)location[1] - i;
                count++;
            }
            if (pieceId.Contains("w") && matran[location[0] + i, location[1] - i].Contains("b"))
            {
                aattack[5, 0] = (float)location[0] + i;
                aattack[5, 1] = (float)location[1] - i;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] + i, location[1] - i].Contains("w"))
            {
                aattack[5, 0] = (float)location[0] + i;
                aattack[5, 1] = (float)location[1] - i;
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
            if (matran[location[0] - i, location[1] + i] == "0")
            {
                amove[count, 0] = (float)location[0] - i;
                amove[count, 1] = (float)location[1] + i;
                count++;
            }
            if (pieceId.Contains("w") && matran[location[0] - i, location[1] + i].Contains("b"))
            {
                aattack[6, 0] = (float)location[0] - i;
                aattack[6, 1] = (float)location[1] + i;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] - i, location[1] + i].Contains("w"))
            {
                aattack[6, 0] = (float)location[0] - i;
                aattack[6, 1] = (float)location[1] + i;
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
            if (matran[location[0] - i, location[1] - i] == "0")
            {
                amove[count, 0] = (float)location[0] - i;
                amove[count, 1] = (float)location[1] - i;
                count++;
            }
            if (pieceId.Contains("w") && matran[location[0] - i, location[1] - i].Contains("b"))
            {
                aattack[7, 0] = (float)location[0] - i;
                aattack[7, 1] = (float)location[1] - i;
                break;
            }
            if (pieceId.Contains("b") && matran[location[0] - i, location[1] - i].Contains("w"))
            {
                aattack[7, 0] = (float)location[0] - i;
                aattack[7, 1] = (float)location[1] - i;
                break;
            }
        }

        RaiseMoveInfo();
    }

    private void RaiseMoveInfo()
    {
        positionx = transform.position.x;
        positiony = transform.position.y;
        ShowQueenMoveInfo?.Invoke(location, amove, positionx, positiony, pieceId, aattack);
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
            Debug.Log(pieceId + "destroyed");
        }
    }

    private void Start()
    {
        GameScript.RaisePieceMoved += PieceMoved;
        GameScript.RaisePieceAttacked += PieceAttacked;
        GameScript.RaiseAiMoved += PieceAttacked;
    }

    private void OnMouseDown()
    {
        Debug.Log("queen clicked");
        BroadMatrix();
        MoveScan();
    }
}
