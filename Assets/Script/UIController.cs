using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider musicSli, sfxSli;
    // Start is called before the first frame update
    
    public void ChangeMusicVolume(){
        AudioManager.Instance.MusicVolumeControl(musicSli.value);
    } 

    public void ChangeSfxVolume(){
        AudioManager.Instance.SFXVolumeControl(sfxSli.value);
    }  

    // Guarda las preferencias de sonido en PlayerPrefs
    public void SaveOptions()
    {
        //AudioManager.Instance.SaveSoundPreferences(musicSli.value, sfxSli.value);
    }

    // Carga las preferencias de sonido desde PlayerPrefs y las aplica a los controles de la UI
    public void LoadOptions()
    {
        //AudioManager.Instance.LoadSoundPreferences(); // Carga los valores guardados en AudioManager
        //musicSli.value = PlayerPrefs.GetFloat(AudioManager.Instance.musicSavedValue); // Actualiza el slider de música
        //sfxSli.value = PlayerPrefs.GetFloat(AudioManager.Instance.sfxSavedValue); // Actualiza el slider de efectos de sonido
        //muteChecker.isOn = AudioManager.Instance.isMute; // Actualiza el toggle de silencio
    }          
}
