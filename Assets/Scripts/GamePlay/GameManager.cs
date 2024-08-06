using UIKit;
using UnityEngine;
using GamePlay;
using System.Collections;
using UnityEngine.UI;

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
	[HideInInspector]
	public Color defaultColor;
	[HideInInspector]
	public Color connectColor;

	[Header("Theme Refrence")]
	[Tooltip("Add Background image for change random background")]
	[SerializeField] Image background;
	[Tooltip("Add all background images and color for every pieces")]
	[SerializeField] ThemeData[] themeDatas;
	
	[Tooltip("Add Camera Controller for set the camera size")]
	public CameraController cameraController;
	[Tooltip("Level Complete effect")]
	[SerializeField] GameObject fireBrustEffect;

	public int currentDimond = 0;

	private int currentThemeIndex = 0;
    public override void OnAwake()
    {
        base.OnAwake();
		currentDimond = SavedDataHandler.instance._saveData.dimonds;
	}

	/// <summary>
	/// Set all piece refrence in 2 dimensional array.
	/// Set initial random rotation, calculate connected points and update color, camera position and zoom value.
	/// </summary>
	public void InitLevel()
    {
		fireBrustEffect.SetActive(false);
		Vector2 dimensions = LevelManager.instance.currentLevelContainer.CheckDimensions();

		puzzle.width = (int)dimensions.x;
		puzzle.height = (int)dimensions.y;

		puzzle.cells = new Piece[puzzle.width, puzzle.height];

		foreach (var cell in LevelManager.instance.currentLevelContainer.pieces)
		{
			puzzle.cells[(int)cell.transform.position.x, (int)cell.transform.position.y] = cell;
		}

		puzzle.winConnection = GetWinValue();

		RandomRotate();
		UpdateConnectedPoints();
		cameraController.SetCameraPositionAndSize(puzzle.width,puzzle.height);
		Invoke("UpdateColor",0.5f);
	}
	
	/// <summary>
	/// Count all connected points and Show level complete screen when all pieces are connected.
	/// </summary>
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
		// reset all pieces color.
		LevelManager.instance.currentLevelContainer.ResetColor();
		// Update color for those piece connect to eachother with the power piece.
		UpdateColor();

		if (puzzle.winConnection == connected)
		{
			LevelManager.instance.isLevelComplete = true;
           
			if (LevelManager.instance.currentLevelIndex > SavedDataHandler.instance._saveData.levelCompleted-1)
				SavedDataHandler.instance._saveData.levelCompleted = LevelManager.instance.currentLevelIndex+1;

			cameraController.UnfocusCamera();
			fireBrustEffect.SetActive(true);
			AudioManager.instance.Play(AudioType.FireBrustSound);
			// Show level complesion screen after 0.5 seconds.
			Helper.Execute(this, () =>
			{
				UIController.instance.ShowNextScreen(ScreenType.LevelComplete);
			}, 0.5f);
		}
	}

	/// <summary>
	/// Update color for those piece connect to eachother with the power piece.
	/// </summary>
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

	/// <summary>
	/// Calculate how many points is required for the level complete.
	/// </summary>
	/// <returns></returns>
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

	/// <summary>
	/// Set a random rotation for each piece except the start point (power piece).
	/// </summary>
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

	/// <summary>
	/// Change rendom theme background image and background audio.
	/// </summary>
	public void ChangeRandomTheam()
	{
		int theamIndex;
		do
		{
			theamIndex = UnityEngine.Random.Range(0, themeDatas.Length);
		} while (currentThemeIndex == theamIndex);
		currentThemeIndex = theamIndex;
		background.sprite = themeDatas[theamIndex].background;
		defaultColor = themeDatas[theamIndex].pieceDefaultColor;
		AudioManager.instance.PlayBG(AudioType.Background);
	}
	
}

[System.Serializable]
public class ThemeData
{
	public Sprite background;
	public Color pieceDefaultColor;
}

public static class Helper
{
	public static Coroutine Execute(this MonoBehaviour monoBehaviour, System.Action action, float time)
	{
		return monoBehaviour.StartCoroutine(DelayedAction(action, time));
	}
	static IEnumerator DelayedAction(System.Action action, float time)
	{
		yield return new WaitForSeconds(time);

		action();
	}
}