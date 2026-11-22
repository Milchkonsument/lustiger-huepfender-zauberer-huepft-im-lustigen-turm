using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScript : MonoBehaviour
{
   public Slider MusicVolumeSlider;
   public Slider SFXVolumeSlider;


   public void StartSceneByID(int ID)
   {
      AudioManager.I.PlaySFX("UI_Click");
      SceneManager.LoadSceneAsync(ID);
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
