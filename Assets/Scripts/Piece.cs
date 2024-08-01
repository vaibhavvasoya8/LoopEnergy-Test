using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Piece 
{
    public ConnectDirection connectDirection;
    public Vector2 pos;
    public bool isConnected = false;
    public bool isGlowing = false;
    public SpriteRenderer[] spriteRenderers;
    public void RotateValues()
    {
        bool top = connectDirection.Top;
        connectDirection.Top = connectDirection.Left;
        connectDirection.Left = connectDirection.Bottom;
        connectDirection.Bottom = connectDirection.Right;
        connectDirection.Right = top;
    }

    public void SetColor(Color color,bool glow)
    {
        isGlowing = glow;
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = color;
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

public enum CellType
{
    Blank,
    Start,
    Middle,
    End
}