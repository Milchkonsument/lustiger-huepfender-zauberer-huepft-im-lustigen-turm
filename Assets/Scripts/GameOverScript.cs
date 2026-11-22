using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI ScoreField;
    public TMPro.TextMeshProUGUI HighscoreField;
    void OnEnable()
    {
        HighscoreField.SetText("Your score: " + ScoreManager.I.currentPlayerName + " " + Mathf.RoundToInt(ScoreManager.I.currentscore).ToString());
        HighscoreField.SetText("HighScore: " + ScoreManager.I.highScorePlayerName + " " + Mathf.RoundToInt(ScoreManager.I.highscore).ToString());
    }

    public void Restart()
    {
        AudioManager.I.PlaySFX("UI_Click");
        SceneManager.LoadSceneAsync(1);
    }

    public void BackToMenu()
    {
        AudioManager.I.PlaySFX("UI_Click");
        SceneManager.LoadSceneAsync(0);
    }
}
