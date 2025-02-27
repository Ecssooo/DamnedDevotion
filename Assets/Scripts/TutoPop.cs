using UnityEngine;

public class TutoPop : MonoBehaviour
{
    [SerializeField] private GameObject Tuto1;
    [SerializeField] private GameObject Tuto2;
    [SerializeField] private GameObject Tuto3;

    private void Start()
    {
        Tuto1.SetActive(false);
        Tuto2.SetActive(false);
        Tuto3.SetActive(false);
    }

    public void PopUp()
    {
        int currentLevel = LevelManager.Instance.CurrentLevel;

        if ( currentLevel == 0) // Premier niveau avec déplacement
        {
            Tuto1.SetActive(true);
        }
        else if (currentLevel == 15) // Premier niveau avec swap
        {
            Tuto2.SetActive(true);
        }
        else if (currentLevel == 24) // Premier niveau avec invocation
        {
            Tuto3.SetActive(true);
        }
    }

    public void CloseTuto()
    {
        Tuto1.SetActive(false);
        Tuto2.SetActive(false);
        Tuto3.SetActive(false);
    }
}
