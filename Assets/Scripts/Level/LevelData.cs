using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public Level[] levels;

}


[System.Serializable]
public class Level
{
    public int levelNo;
    public GameObject prefab;
    public ComplimentType complimentType;
}

public enum ComplimentType
{
    None,
    Good,
    Amazing,
    Welldone,
    Perfect
}