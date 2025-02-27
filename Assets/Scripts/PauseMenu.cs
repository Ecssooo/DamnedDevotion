using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    [SerializeField] private GameObject _canva;
    [SerializeField] private GameObject _board;
    [SerializeField] private GameObject _defeatScreen;
    [SerializeField] private GameObject _winScreen;

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlaySFX("Test");
        this.gameObject.SetActive(false);
    }

    public void ReturnMenu()
    {
        LevelManager.Instance.LoadMenu();
    }
}
