using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

public class GameAiScript : MonoBehaviour
{
    [SerializeField] private bool active = false; 
    private string pieceId;
    private string[,] matran = new string[8, 8];
    private int[] location = new int[2];
    private MoveScan.MoveInfo moveInfo = new MoveScan.MoveInfo();
    private float[,] aattack;
    public delegate void AiMoveInfo(string pieceId, float[,] move);
    public static event AiMoveInfo ShowAiMoveInfo;

    private void BroadMatrix()
    {
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
    }

    private void RaiseMoveInfo()
    {
        //Astar();
        BroadMatrix();
        BFS.BFSALg bfs = new BFS.BFSALg();
        bfs.Matran = this.matran;
        moveInfo = bfs.BFS_Search();
        ShowAiMoveInfo?.Invoke(moveInfo.PieceId, moveInfo.Moves);
    }

    private void Start()
    {
        if (active == true)
        {
            GameScript.CallAiMove += RaiseMoveInfo;
        }
    }
}
