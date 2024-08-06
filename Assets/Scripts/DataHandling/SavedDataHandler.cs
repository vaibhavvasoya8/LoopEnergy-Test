using System;
using GamePlay;
using UnityEngine;

public class SavedDataHandler : Singleton<SavedDataHandler>
{
    [Header("Data Credentials")] public string password;

    [Header("Current Save Data")] public SaveData _saveData;

    [Header("Default Data")] public SaveData _DefaultSaveData;

    public static Action<SaveData> OnDataLoaded;

    public override void OnAwake()
    {
        base.OnAwake();

        _saveData = SaveGameData.Load(_DefaultSaveData, password);
        ApplyData();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            FetchDataFromGame();
            SaveGameData.Save(_saveData, password);
        }
        else
        {
            _saveData = SaveGameData.Load(_DefaultSaveData, password);
            ApplyData();
        }
    }

    public void SaveData()
    {
        FetchDataFromGame();
        SaveGameData.Save(_saveData, password);
    }

    public void ResetToDefault()
    {
        _saveData = SaveGameData.Clear(_DefaultSaveData, password);
        ApplyData();
    }

    void ApplyData()
    {
        // LevelManager.instance.currentLevelIndex = _saveData.levelCompleted;
    }
    public void FetchDataFromGame()
    {
        if (LevelManager.instance.currentLevelIndex >= _saveData.levelCompleted)
            _saveData.levelCompleted = LevelManager.instance.currentLevelIndex;
        _saveData.dimonds = GameManager.instance.currentDimond;
    }

    [EasyButtons.Button]
    void Delete()
    {
        SaveGameData.Delete();
    }
   
}

[Serializable]
public class SaveData
{
    public int dimonds;
    public int levelCompleted = 0;
    public bool isSFXMute;
    public bool isMusicMute;
}

