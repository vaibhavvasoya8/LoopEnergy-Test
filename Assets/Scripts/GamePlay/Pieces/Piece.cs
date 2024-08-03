using UIKit;
using UnityEngine;

namespace GamePlay
{
    [System.Serializable]
    public abstract class Piece : MonoBehaviour
    {
        public PieceType pieceType;

        public ConnectDirection connectDirection;
        public Vector2 pos;
        public bool isConnected = false;
        public bool isGlowing = false;
        public SpriteRenderer[] spriteRenderers;
        public Transform content;
        public float rotationSpeed = 0.2f;
        protected float realRotation;

        void Start()
        {
            OnStart();
            pos = transform.position;

        }
        void OnMouseDown()
        {
            if (UIController.instance.IsTapOnUI()) return;
            if (pieceType == PieceType.Start)
            {
                ApplyRotation();
            }
            else if (pieceType != PieceType.Blank)
            {
                isGlowing = false;
                ApplyRotation();
                GameManager.instance.UpdateConnectedPoints();
                Events.UpdateBulbPower();
            }
        }
        public void RotateInit()
        {
            if (pieceType == PieceType.Start) return;
            ApplyRotation();
        }
        public void RotateValues()
        {
            bool top = connectDirection.Top;
            connectDirection.Top = connectDirection.Left;
            connectDirection.Left = connectDirection.Bottom;
            connectDirection.Bottom = connectDirection.Right;
            connectDirection.Right = top;
        }

        public void SetColor(Color color, bool glow)
        {
            isGlowing = glow;
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = color;
            }
        }
        public abstract void ApplyRotation();
        public virtual void OnStart() { }

        public void UpdateConnectedCellColor(Piece source)
        {
            if (!isConnected) return;
            if (!isGlowing)
            {
                //if (CellType == CellType.End) glowEffect.Play();
                SetColor(GameManager.instance.connectColor, true);
            }
            if (connectDirection.Top)
            {
                if (pos.y + 1 < GameManager.instance.puzzle.height && GameManager.instance.puzzle.cells[(int)pos.x, (int)pos.y + 1] != source)
                {
                    if (GameManager.instance.puzzle.cells[(int)pos.x, (int)pos.y + 1].connectDirection.Bottom)
                        GameManager.instance.puzzle.cells[(int)pos.x, (int)pos.y + 1].UpdateConnectedCellColor(this);
                }
            }
            if (connectDirection.Right)
            {
                if (pos.x + 1 < GameManager.instance.puzzle.width && GameManager.instance.puzzle.cells[(int)pos.x + 1, (int)pos.y] != source)
                {
                    //if(source) Debug.Log(source.gameObject.name);
                    if (GameManager.instance.puzzle.cells[(int)pos.x + 1, (int)pos.y].connectDirection.Left)
                        GameManager.instance.puzzle.cells[(int)pos.x + 1, (int)pos.y].UpdateConnectedCellColor(this);
                }
            }
            if (connectDirection.Bottom)
            {
                if (pos.y - 1 >= 0 && GameManager.instance.puzzle.cells[(int)pos.x, (int)pos.y - 1] != source)
                {
                    if (GameManager.instance.puzzle.cells[(int)pos.x, (int)pos.y - 1].connectDirection.Top)
                        GameManager.instance.puzzle.cells[(int)pos.x, (int)pos.y - 1].UpdateConnectedCellColor(this);
                }
            }
            if (connectDirection.Left)
            {
                if (pos.x - 1 >= 0 && GameManager.instance.puzzle.cells[(int)pos.x - 1, (int)pos.y] != source)
                {
                    if (GameManager.instance.puzzle.cells[(int)pos.x - 1, (int)pos.y].connectDirection.Right)
                        GameManager.instance.puzzle.cells[(int)pos.x - 1, (int)pos.y].UpdateConnectedCellColor(this);
                }
            }
        }
    }

    [System.Serializable]
    public struct ConnectDirection
    {
        public bool Top;
        public bool Right;
        public bool Bottom;
        public bool Left;
    }

    public enum PieceType
    {
        Blank,
        Start,
        Middle,
        End
    }
}