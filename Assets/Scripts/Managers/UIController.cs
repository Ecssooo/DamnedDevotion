using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //public Slider _musicSlider, _sfxSlider;

    private void Start()
    {
        // _musicSlider.value = 0.5f;
        // _sfxSlider.value = 0.5f;
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    // public void MusicVolume()
    // {
    //     //AudioManager.Instance.MusicVolume(_musicSlider.value);
    // }
    //
    // public void SFXVolume()
    // {
    //     AudioManager.Instance.SFXVolume(_sfxSlider.value);
    //     AudioManager.Instance.PlaySFX("slider");
    // }
}
