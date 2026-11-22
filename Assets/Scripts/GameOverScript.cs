using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI ScoreField;
    public TMPro.TextMeshProUGUI HighscoreField;
    void OnEnable()
    {
        ScoreField.SetText("Your score: " + Mathf.RoundToInt(ScoreManager.I.currentscore).ToString());
        HighscoreField.SetText("HighScore: " + Mathf.RoundToInt(ScoreManager.I.highscore).ToString() + "(" + ScoreManager.I.highScorePlayerName + ")");
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
