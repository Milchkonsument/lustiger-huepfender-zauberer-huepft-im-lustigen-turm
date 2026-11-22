using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScript : MonoBehaviour
{
   public Slider MusicVolumeSlider;
   public Slider SFXVolumeSlider;
   public TMPro.TMP_InputField NameInput;

   public TMPro.TextMeshProUGUI HighscoreField;

    public void Start()
    {
      HighscoreField.SetText("HighScore: " + ScoreManager.I.highScorePlayerName +" "+ Mathf.RoundToInt(ScoreManager.I.highscore).ToString());
    }
    public void StartGame()
   {
      AudioManager.I.PlaySFX("UI_Click");
      SceneManager.LoadSceneAsync(1);
      ScoreManager.I.SetName(NameInput.text);
   }

   public void QuitGame()
   {
      AudioManager.I.PlaySFX("UI_Click");
      Application.Quit();
   }

   public void HandleSFXVolumeSlider()
   {
      AudioManager.I.SetSFXVolume(SFXVolumeSlider.value);
   }
   public void HandleMusicVolumeSlider()
   {
      AudioManager.I.SetMusicVolume(MusicVolumeSlider.value);
   }
}
