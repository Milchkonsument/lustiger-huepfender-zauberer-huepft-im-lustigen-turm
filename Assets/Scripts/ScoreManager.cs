using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public string currentPlayerName;
    public int currentscore = 67;

    public string highScorePlayerName = "THe guy she told you not to worry about";
    public int highscore = 67;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void SetName(string newName)
    {
        currentPlayerName = newName;
    }

    public void SetScore(int score)
    {
        currentscore = score;

        if (currentscore > highscore)
        {
            highscore = score;
            highScorePlayerName = currentPlayerName;
        }
    }
}
