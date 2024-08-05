using UnityEngine;
using System;

namespace GamePlay
{
    public class LevelManager : Singleton<LevelManager>
    {
        public static event Action OnLoadLevel;

        [Header("Level Data & Prefabs")]
        public LevelData levelData;

        public int currentLevelIndex;

        public LevelContainer currentLevelContainer;
        public bool isLevelComplete = false;

       

        // Start is called before the first frame update
        void Start()
        {
            currentLevelIndex = SavedDataHandler.instance._saveData.levelCompleted;
            if (currentLevelIndex >= levelData.levels.Length)
                currentLevelIndex = levelData.levels.Length - 1; 
            LoadLevel(currentLevelIndex); 
        }

        void LoadLevel(int levelNo)
        {
            if (currentLevelContainer != null)
                Destroy(currentLevelContainer.gameObject);

            GameManager.instance.ChangeRandomTheam();

            if (levelNo - 1 < levelData.levels.Length)
            {
                currentLevelContainer = Instantiate(levelData.levels[currentLevelIndex].prefab, Vector3.zero, Quaternion.identity).GetComponent<LevelContainer>();
            }
            else
                Debug.LogError("Invalid Level Number.");

            isLevelComplete = false;
            GameManager.instance.InitLevel();
        }

        public void LoadNextLevel()
        {
            currentLevelIndex++;
            if (currentLevelIndex < levelData.levels.Length)
                LoadLevel(currentLevelIndex);
            else
            {
                Debug.Log("More Level Coming soon!");
                currentLevelIndex = 0;
                LoadLevel(currentLevelIndex);
            }
        }

        public void ReloadLevel()
        {
            LoadLevel(currentLevelIndex);
        }

        public void LoadPreviousLevel()
        {
            currentLevelIndex--;
            LoadLevel(currentLevelIndex);
        }
        
        public static void LoadLevel()
        {
            OnLoadLevel?.Invoke();
        }

       
    }
   
}