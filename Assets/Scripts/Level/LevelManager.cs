using UnityEngine;
using System;

namespace GamePlay
{
    public class LevelManager : Singleton<LevelManager>
    {
        public static event Action OnLoadLevel;

        [Header("Level Data SO refrence")]
        public LevelData levelData;

        public int currentLevelIndex;

        [HideInInspector]
        public LevelContainer currentLevelContainer;

        [HideInInspector]
        public bool isLevelComplete = false;

        void Start()
        {
            //if(SavedDataHandler.instance._saveData.levelCompleted != 0)
                currentLevelIndex = SavedDataHandler.instance._saveData.levelCompleted;
            if (currentLevelIndex >= levelData.levels.Length)
                currentLevelIndex = levelData.levels.Length - 1; 
            LoadLevel(currentLevelIndex); 
        }

        /// <summary>
        /// Instantiate new level.
        /// </summary>
        /// <param name="levelNo">Level index</param>
        void LoadLevel(int levelNo)
        {
            if (currentLevelContainer != null)
                Destroy(currentLevelContainer.gameObject);

            GameManager.instance.ChangeRandomTheam();

            if (levelNo < levelData.levels.Length)
            {
                currentLevelContainer = Instantiate(levelData.levels[currentLevelIndex].prefab, Vector3.zero, Quaternion.identity).GetComponent<LevelContainer>();
            }
            else
                Debug.LogError("Invalid Level Number.");

            isLevelComplete = false;
            GameManager.instance.InitLevel();
        }
        /// <summary>
        /// Load next level when current level is completed. and all level completed then load first level.
        /// </summary>
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

        /// <summary>
        /// Realod current level.
        /// </summary>
        public void ReloadLevel()
        {
            LoadLevel(currentLevelIndex);
        }
        /// <summary>
        /// Load previous level.
        /// </summary>
        public void LoadPreviousLevel()
        {
            currentLevelIndex--;
            LoadLevel(currentLevelIndex);
        }
        /// <summary>
        /// Get the wining dimond amount as per the current level.
        /// </summary>
        /// <returns>return the number of dimond to win.</returns>
        public int GetWinDiamond()
        {
            return levelData.levels[currentLevelIndex].winDimond;
        }
        /// <summary>
        /// Get the complement message as per the current level.
        /// </summary>
        /// <returns>return the string complement message</returns>
        public string GetComplementMessage()
        {
            string message = levelData.levels[currentLevelIndex].complimentType.ToString();
            message = message.Replace("_", " ");
            return message;
        }
        // for the future
        /// <summary>
        /// Fire the event when load the new level.
        /// </summary>
        public static void LoadLevel()
        {
            OnLoadLevel?.Invoke();
        }

       
    }
   
}