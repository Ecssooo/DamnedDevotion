using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlaySFX("Test");
        this.gameObject.SetActive(false);
    }

}
