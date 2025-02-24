using UnityEngine;

public class TutoPop : MonoBehaviour
{
    LevelManager LevelManager;

    [SerializeField] private GameObject Tuto1;
    [SerializeField] private GameObject Tuto2;
    [SerializeField] private GameObject Tuto3;

    private void Start()
    {
        Tuto1.SetActive(false);
        Tuto2.SetActive(false);
        Tuto3.SetActive(false);
    }

    void PopUp()
    {
        int currentLevel = LevelManager.Instance.CurrentLevel;

        if ( currentLevel == 1)
        {
            Tuto1.SetActive(true);
        }
        else if (currentLevel == 2)
        {
            Tuto2.SetActive(true);
        }
        else if (currentLevel == 3)
        {
            Tuto3.SetActive(true);
        }
    }

    void CloseTuto()
    {
        Tuto1.SetActive(false);
        Tuto2.SetActive(false);
        Tuto3.SetActive(false);
    }
}
