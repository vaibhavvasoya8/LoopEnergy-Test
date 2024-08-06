using UIKit;
using UnityEngine;

namespace GamePlay
{
    [System.Serializable]
    public abstract class Piece : MonoBehaviour
    {
        [Tooltip("Piece type")]
        public PieceType pieceType;

        [Header("Connection direction points")]
        public ConnectDirection connectDirection;
        [Tooltip("Piece position in grid.")]
        public Vector2 pos;
        [Tooltip("It's TRUE when any point is connected to other piece point.")]
        public bool isConnected = false;
        [Tooltip("It's TRUE when this piece is linked to the power piece(start piece).")]
        public bool isGlowing = false;
        [Header("All sprite refrence those allow to change collore when piece is linked to power piece")]
        public SpriteRenderer[] spriteRenderers;
        public Transform content;
        [Tooltip("Piece rotation speed.")]
        public float rotationSpeed = 0.2f;
        
        protected float realRotation;

        public abstract void ApplyRotation();
        public virtual void OnStart() { }

        void Start()
        {
            OnStart();
            pos = transform.position;

        }
        void OnMouseDown()
        {
            if (UIController.instance.IsTapOnUI() || LevelManager.instance.isLevelComplete) return;
            if (pieceType == PieceType.Start)
            {
                ApplyRotation();
                AudioManager.instance.Play(AudioType.TapRestrict);
            }
            else if (pieceType != PieceType.Blank)
            {
                isGlowing = false;
                ApplyRotation();
                GameManager.instance.UpdateConnectedPoints();
                Events.UpdateBulbPower();
                AudioManager.instance.Play(AudioType.TapOnPiece);
            }
        }
        /// <summary>
        /// Change initial roation except start(power) piece.
        /// </summary>
        public void RotateInit()
        {
            if (pieceType == PieceType.Start) return;
            ApplyRotation();
        }
        /// <summary>
        /// Change connection direction when piece is rotate.
        /// </summary>
        public void RotateValues()
        {
            bool top = connectDirection.Top;
            connectDirection.Top = connectDirection.Left;
            connectDirection.Left = connectDirection.Bottom;
            connectDirection.Bottom = connectDirection.Right;
            connectDirection.Right = top;
        }
        /// <summary>
        /// Set piece color and set is glowing or not.(Means is linked on power piece or not)
        /// </summary>
        /// <param name="color">Set defalut or glow color</param>
        /// <param name="glow">is linked on power piece or not</param>
        public void SetColor(Color color, bool glow)
        {
            isGlowing = glow;
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = color;
            }
        }

        /// <summary>
        /// When connected to a power piece it updates the color of the piece and also updates the color of the piece connected to itself.
        /// </summary>
        /// <param name="source">pass the piece refrence those piece is call this method</param>
        public void UpdateConnectedCellColor(Piece source)
        {
            if (!isConnected) return;
            if (!isGlowing)
            {
                //if (CellType == CellType.End) glowEffect.Play();
                SetColor(GameManager.instance.connectColor, true);
                if(GetComponent<WifiPiece>() != null)
                {
                    Events.WifiConectionUpdate(GetComponent<Piece>());
                }
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
        End,
        Wifi
    }
}