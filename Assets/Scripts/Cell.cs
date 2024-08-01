using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cell : MonoBehaviour
{
	public Piece piece;
	[SerializeField] float speed = 0.2f;
	float realRotation;
	public CellType CellType;

	[SerializeField] Transform content;
	
	// Use this for initialization
	void Start()
	{
		piece.pos = transform.position;
	}

	void OnMouseDown()
	{
		piece.isConnected = false;
		piece.isGlowing = false;
		RotatePiece();
		GameManager.instance.UpdateConnectedPoints();
	}

	public void RotatePiece()
	{
		realRotation -= 90;

		if (realRotation == 360)
			realRotation = 0;
		content.DORotateQuaternion(Quaternion.Euler(0,0,realRotation),speed);
		piece.RotateValues();
	}

	public void UpdateConnectedCellColor(Cell source)
    {
		if (!piece.isConnected) return;
		if (!piece.isGlowing)
		{
			piece.SetColor(GameManager.instance.connectColor,true);
		}
        if (piece.connectDirection.Top)
        {
			if(piece.pos.y+1 < GameManager.instance.puzzle.height && GameManager.instance.puzzle.cells[(int)piece.pos.x, (int)piece.pos.y + 1] != source)
            {
				if (GameManager.instance.puzzle.cells[(int)piece.pos.x, (int)piece.pos.y + 1].piece.connectDirection.Bottom)
					GameManager.instance.puzzle.cells[(int)piece.pos.x, (int)piece.pos.y + 1].UpdateConnectedCellColor(this);
            }
        }
		if (piece.connectDirection.Right)
		{
			if (piece.pos.x + 1 < GameManager.instance.puzzle.width && GameManager.instance.puzzle.cells[(int)piece.pos.x + 1, (int)piece.pos.y] != source)
			{
				//if(source) Debug.Log(source.gameObject.name);
				if (GameManager.instance.puzzle.cells[(int)piece.pos.x + 1, (int)piece.pos.y].piece.connectDirection.Left)
					GameManager.instance.puzzle.cells[(int)piece.pos.x + 1, (int)piece.pos.y].UpdateConnectedCellColor(this);
			}
		}
		if (piece.connectDirection.Bottom)
		{
			if (piece.pos.y - 1 >= 0 && GameManager.instance.puzzle.cells[(int)piece.pos.x, (int)piece.pos.y - 1] != source)
			{
				if (GameManager.instance.puzzle.cells[(int)piece.pos.x, (int)piece.pos.y - 1].piece.connectDirection.Top)
					GameManager.instance.puzzle.cells[(int)piece.pos.x, (int)piece.pos.y - 1].UpdateConnectedCellColor(this);
			}
		}
		if (piece.connectDirection.Left)
		{
			if (piece.pos.x - 1 >= 0 && GameManager.instance.puzzle.cells[(int)piece.pos.x - 1, (int)piece.pos.y] !=source)
			{
				if (GameManager.instance.puzzle.cells[(int)piece.pos.x - 1, (int)piece.pos.y].piece.connectDirection.Right)
					GameManager.instance.puzzle.cells[(int)piece.pos.x - 1, (int)piece.pos.y].UpdateConnectedCellColor(this);
			}
		}
	}

	
}
