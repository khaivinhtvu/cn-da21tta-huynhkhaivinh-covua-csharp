using System;
using UnityEngine;

public class BroadState
{
    internal class State
    {
        private string[,] matran = new string[8, 8];
        private float[,] amove;
        private int depth;
        private string pieceId;
        private int curId;
        private int preId;
        private int g;
        private double f;

        public State()
        {
            CurId = -1;
            PreId = -1;
            g = 0;
            f = -1;
            depth = 0;
        }

        public State(string[,] matran, int cp, double f, float[,] amove, int depth, string pieceId, int curId, int preId)
        {
            this.Matran = matran;
            this.Amove = amove;
            this.Depth = depth;
            this.PieceId = pieceId;
            this.CurId = curId;
            this.PreId = preId;
            this.G = cp;
            this.F = f;
        }

        public int G
        {
            get { return g; }
            set { g = value; }
        }

        public double F
        {
            get { return f; }
            set { f = value; }
        }

        public string[,] Matran { get => matran; set => matran = value; }
        public float[,] Amove { get => amove; set => amove = value; }
        public string PieceId { get => pieceId; set => pieceId = value; }
        public int CurId { get => curId; set => curId = value; }
        public int PreId { get => preId; set => preId = value; }
        public int Depth { get => depth; set => depth = value; }
    }
}
