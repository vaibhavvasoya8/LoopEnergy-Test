using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class LevelContainer : MonoBehaviour
    {
        public Level Level;
        public Piece[] pieces;
        public Piece[] startPoint;
        [SerializeField] int NumberOfStartPoints;
        private void Awake()
        {
            if (startPoint.Length == 0)
                FindStartPoints();
        }
        private void FindStartPoints()
        {
            startPoint = new Piece[NumberOfStartPoints];
            int count = 0;
            foreach (var cell in pieces)
            {
                if (cell.pieceType == PieceType.Start)
                {
                    startPoint[count] = cell;
                    count++;
                }
            }
        }

        public void UpdateConnectedCellColor()
        {
            for (int i = 0; i < startPoint.Length; i++)
            {
                startPoint[i].UpdateConnectedCellColor(null);
            }
        }

        public void ResetColor()
        {
            Color defaultColor = GameManager.instance.defalutColor;
            foreach (var cell in pieces)
            {
                cell.SetColor(defaultColor, false);
            }
        }

        public Vector2 CheckDimensions()
        {
            Vector2 dimension = Vector2.zero;
            foreach (var cell in pieces)
            {
                if (cell.transform.position.x > dimension.x)
                    dimension.x = cell.transform.position.x;

                if (cell.transform.position.y > dimension.y)
                    dimension.y = cell.transform.position.y;
            }
            dimension.x++;
            dimension.y++;

            return dimension;
        }
        [EasyButtons.Button]
        void SetPiecesRefrance()
        {
            int count = transform.childCount;
            pieces = new Piece[count];
            startPoint = new Piece[NumberOfStartPoints];
            int startCount = 0;
            for (int i = 0; i < count; i++)
            {
                pieces[i] = transform.GetChild(i).GetComponent<Piece>();
                if (pieces[i].pieceType == PieceType.Start)
                {
                    startPoint[startCount] = pieces[i];
                    startCount++;
                }
            }
        }
    }
}