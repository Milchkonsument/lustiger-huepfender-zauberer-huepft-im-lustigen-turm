using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public string currentPlayerName;
    public int currentscore = 0;

    public string highScorePlayerName = "Irgendsoein Typ";
    public int highscore = 67;

    public override bool DestroyOnLoad => true;

    
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
