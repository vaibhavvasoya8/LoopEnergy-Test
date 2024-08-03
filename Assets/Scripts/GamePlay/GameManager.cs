using UIKit;
using UnityEngine;
using GamePlay;

public class GameManager : Singleton<GameManager>
{

	[System.Serializable]
	public class Puzzle
	{
		public int winConnection;
		public int currentConnection;
		
		public int width;
		public int height;
		public Piece[,] cells;
	}

	public Puzzle puzzle;

	public Color defalutColor;
	public Color connectColor;

	public CameraController cameraController;

	// Use this for initialization
	void Start()
	{
		
	}
	public void InitLevel()
    {
		Vector2 dimensions = LevelManager.instance.currentLevelContainer.CheckDimensions();

		puzzle.width = (int)dimensions.x;
		puzzle.height = (int)dimensions.y;

		puzzle.cells = new Piece[puzzle.width, puzzle.height];

		foreach (var cell in LevelManager.instance.currentLevelContainer.pieces)
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
		cameraController.SetCameraPositionAndSize(puzzle.width,puzzle.height);
		Invoke("UpdateColor",0.5f);
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
					if (puzzle.cells[w, h].connectDirection.Top && puzzle.cells[w, h + 1].connectDirection.Bottom)
					{
						puzzle.cells[w, h + 1].isConnected = true;
						connected++;
					}
				}
				//compare right
				if (w != puzzle.width - 1)
				{
					if (puzzle.cells[w, h].connectDirection.Right && puzzle.cells[w + 1, h].connectDirection.Left)
					{
						puzzle.cells[w + 1, h].isConnected = true;
						connected++;
					}
				}
				//compares bottom
				if (h != 0)
				{
					if (puzzle.cells[w, h].connectDirection.Bottom && puzzle.cells[w, h - 1].connectDirection.Top)
					{
						puzzle.cells[w, h - 1].isConnected = true;
						//connected++;
					}
				}
				//compare left
				if (w != 0)
				{
					if (puzzle.cells[w, h].connectDirection.Left && puzzle.cells[w - 1, h].connectDirection.Right)
					{
						puzzle.cells[w - 1, h].isConnected = true;
						//connected++;
					}
				}
			}
		}
		puzzle.currentConnection = connected;
		LevelManager.instance.currentLevelContainer.ResetColor();
		UpdateColor();

		if(puzzle.winConnection == connected)
        {
			UIController.instance.ShowNextScreen(ScreenType.LevelComplete);
		}
	}
	void UpdateColor()
	{
		if (LevelManager.instance.currentLevelContainer.startPoint == null)
		{
			Debug.LogError("Please add power(Start) point");
			return;
		}
		//powerCell.isConnected = true;
		LevelManager.instance.currentLevelContainer.UpdateConnectedCellColor();

	}
	

	int GetWinValue()
	{
		int winValue = 0;
		foreach (var cell in puzzle.cells)
		{
			if (cell.connectDirection.Top) winValue++;
			if (cell.connectDirection.Right) winValue++;
			if (cell.connectDirection.Bottom) winValue++;
			if (cell.connectDirection.Left) winValue++;
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
				cell.RotateInit();
			}
		}
	}

}
