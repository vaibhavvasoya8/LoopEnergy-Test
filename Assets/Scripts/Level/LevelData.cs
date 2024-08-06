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
    public int winDimond;
    public ComplimentType complimentType;
}

public enum ComplimentType
{
    None,
    Good,
    Amazing,
    Welldone,
    Perfect,
    Excellent,
    Fantastic,
    Superb,
    Brilliant,
    Outstanding,
    Awesome,
    Tremendous,
    Impressive,
    Splendid,
    Marvelous,
    Outstanding_Work,
    Extraordinary,
    Magnificent,
    You_Did_It,
    Great_Job,
    Top_Notch
}