using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;

public class PlayGamesController : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject obj2;
    [SerializeField] private TextMeshProUGUI text;
    void Start()
    {

        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            text.text = "Connecte";
            // Continue with Play Games Services
        }
        else
        {
            text.text = "Non connecte";
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            //PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }


    #region Instance
    private static PlayGamesController _instance;

    public static PlayGamesController Instance { get => _instance; }

    public void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    // Debloquer un achievement
    public void UnlockAchievement(string achievementID)
    {
        GameObject objet =Instantiate(obj);
        objet.transform.position = new Vector3(0, 0, 0);
        Social.ReportProgress(achievementID, 100.0f, success =>
        {
            if (success)
            {
                Debug.Log("Achievement debloque !");
                GameObject objet = Instantiate(obj2);
                objet.transform.position = new Vector3(0, 0, 0);
            }
            else
            {
                GameObject objet = Instantiate(obj);
                objet.transform.position = new Vector3(1, 0, 0);
                Debug.Log("Echec du deblocage");
            }
        });
    }

    private void Connect()
    {
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

    //exemple d'utilisation

    //UnlockAchievement("CgkIj9xxxxxxEAIQAQ"); // Remplace par l’ID de l’achievement


    //exemple pour voir les succes sur un bouton

    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }
}
