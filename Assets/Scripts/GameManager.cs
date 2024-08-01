using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[System.Serializable]
	public class Puzzle
	{
		public int winConnection;
		public int currentConnection;
		
		public int width;
		public int height;
		public Cell[,] cells;
	}

	public Puzzle puzzle;

	[SerializeField] Cell[] cells;
	[SerializeField] Cell powerCell = null;

	public Color defalutColor;
	public Color connectColor;
	private void Awake()
    {
		instance = this;
    }
    // Use this for initialization
    void Start()
	{
		Vector2 dimensions = CheckDimensions();

		puzzle.width = (int)dimensions.x;
		puzzle.height = (int)dimensions.y;

		puzzle.cells = new Cell[puzzle.width, puzzle.height];

		foreach (var cell in cells)
		{
			puzzle.cells[(int)cell.transform.position.x, (int)cell.transform.position.y] = cell;
		}

		foreach (var item in puzzle.cells)
		{
			Debug.Log(item.name);
		}

		puzzle.winConnection = GetWinValue();

		RandomRotate();
		UpdateConnectedPoints();
	}
	
	public void UpdateConnectedPoints()
	{
		int connected = 0;
		for (int h = 0; h < puzzle.height; h++)
		{
			for (int w = 0; w < puzzle.width; w++)
			{
				//compares top
				if (h != puzzle.height - 1) {
					if (puzzle.cells[w, h].piece.connectDirection.Top && puzzle.cells[w, h + 1].piece.connectDirection.Bottom)
					{
						puzzle.cells[w, h + 1].piece.isConnected = true;
						connected++;
					}
				}
				//compare right
				if (w != puzzle.width - 1)
				{
					if (puzzle.cells[w, h].piece.connectDirection.Right && puzzle.cells[w + 1, h].piece.connectDirection.Left)
					{
						puzzle.cells[w + 1, h].piece.isConnected = true;
						connected++;
					}
				}
				//compares bottom
				if (h != 0)
				{
					if (puzzle.cells[w, h].piece.connectDirection.Bottom && puzzle.cells[w, h - 1].piece.connectDirection.Top)
					{
						puzzle.cells[w, h - 1].piece.isConnected = true;
						//connected++;
					}
				}
				//compare left
				if (w != 0)
				{
					if (puzzle.cells[w, h].piece.connectDirection.Left && puzzle.cells[w - 1, h].piece.connectDirection.Right)
					{
						puzzle.cells[w - 1, h].piece.isConnected = true;
						//connected++;
					}
				}
			}
		}
		puzzle.currentConnection = connected;
		ResetColor();
		UpdateColor();
	}
	void UpdateColor()
	{
		if (powerCell == null)
		{
			foreach (var cell in cells)
			{
				if (cell.CellType == CellType.Start)
				{
					powerCell = cell;
					break;
				}
			}
		}
		if (powerCell == null)
		{
			Debug.LogError("Please add power(Start) point");
			return;
		}
		powerCell.piece.isConnected = true;
		powerCell.UpdateConnectedCellColor(null);

	}
	public void ResetColor()
	{
		foreach (var cell in cells)
		{
			cell.piece.SetColor(defalutColor, false);
		}
	}
	Vector2 CheckDimensions()
	{
		Vector2 dimension = Vector2.zero;
		foreach (var cell in cells)
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
	int GetWinValue()
	{
		int winValue = 0;
		foreach (var cell in puzzle.cells)
		{
			if (cell.piece.connectDirection.Top) winValue++;
			if (cell.piece.connectDirection.Right) winValue++;
			if (cell.piece.connectDirection.Bottom) winValue++;
			if (cell.piece.connectDirection.Left) winValue++;
		}
		winValue /= 2;

		return winValue;
	}

	void RandomRotate()
	{
		foreach (var cell in puzzle.cells)
		{
			int k = Random.Range(0, 4);

			for (int i = 0; i < k; i++)
			{
				cell.RotatePiece();
			}
		}
	}

}
