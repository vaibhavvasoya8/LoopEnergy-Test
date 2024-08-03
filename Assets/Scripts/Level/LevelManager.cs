using UnityEngine;

namespace GamePlay
{
    public class LevelManager : Singleton<LevelManager>
    {
        public LevelData levelData;
        public Level currentLevel;

        public int currentLevelIndex;

        public LevelContainer currentLevelContainer;

        // Start is called before the first frame update
        void Start()
        {
            LoadLevel(currentLevelIndex);
        }

        void LoadLevel(int levelNo)
        {
            if (currentLevelContainer != null)
                Destroy(currentLevelContainer.gameObject);

            if (levelNo - 1 < levelData.levels.Length)
            {
                //currentLevelIndex = levelNo - 1;
                currentLevel = levelData.levels[currentLevelIndex];
                currentLevelContainer = Instantiate(currentLevel.prefab, Vector3.zero, Quaternion.identity).GetComponent<LevelContainer>();
            }
            else
                Debug.LogError("Invalid Level Number.");

            GameManager.instance.InitLevel();
        }

        public void LoadNextLevel()
        {
            currentLevelIndex++;
            if (currentLevelIndex < levelData.levels.Length)
                LoadLevel(currentLevelIndex);
            else
                Debug.Log("More Level Coming soon!");
        }

        public void ReloadLevel()
        {
            LoadLevel(currentLevelIndex);
        }

    }
}