using UnityEngine;

public class CloseTab : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject pauseButton;

    public void Close()
    {
        pauseMenu.SetActive(false);
        AudioManager.Instance.sfxSource.Stop();
        AudioManager.Instance.PlayMusic("Theme");
        pauseButton.SetActive(true);
    }
}
