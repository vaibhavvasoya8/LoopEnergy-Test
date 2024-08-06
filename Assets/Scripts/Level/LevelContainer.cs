using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class LevelContainer : MonoBehaviour
    {
        [Header("Pieces refrence.")]
        [Tooltip("Add all pieces refrence.(Start, Middle, End, Wifi and Empty.)")]
        public Piece[] pieces;
        [Tooltip("Add start piece refrence(Power Points)")]
        public Piece[] startPoint;
        [Tooltip("Set the Number of Powwer point count for get auto/runtime refrance.")]
        [SerializeField] int NumberOfStartPoints;

        private void Awake()
        {
            if (startPoint.Length == 0)
                FindStartPoints();
        }
        /// <summary>
        /// Set the start(Power) piece refrence.
        /// </summary>
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
        /// <summary>
        /// Start color updating from start piece to all connected piece.
        /// </summary>
        public void UpdateConnectedCellColor()
        {
            for (int i = 0; i < startPoint.Length; i++)
            {
                startPoint[i].UpdateConnectedCellColor(null);
            }
        }
        /// <summary>
        /// Reset all pieces color to defaultColor
        /// </summary>
        public void ResetColor()
        {
            Color defaultColor = GameManager.instance.defaultColor;
            foreach (var cell in pieces)
            {
                cell.SetColor(defaultColor, false);
            }
        }
        /// <summary>
        /// Calculate grid width and height based on cell
        /// </summary>
        /// <returns>return width and height</returns>
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

        //For the level designer. Get all piece refrance and set to the pieces ans startPoints array.
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