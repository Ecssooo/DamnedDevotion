using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayGamesController : MonoBehaviour
{
    void Start()
    {
        PlayGamesPlatform.Activate(); // Active Google Play Games
        Social.localUser.Authenticate(success => {
            if (success)
            {
                Debug.Log("Connecte à Google Play Games");
            }
            else
            {
                Debug.Log("Echec de la connexion");
            }
        });
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

    // Debloquer un achievement
    public void UnlockAchievement(string achievementID)
    {
        Social.ReportProgress(achievementID, 100.0f, success => {
            if (success)
            {
                Debug.Log("Achievement debloque !");
            }
            else
            {
                Debug.Log("Echec du deblocage");
            }
        });
    }

    //exemple d'utilisation

    //UnlockAchievement("CgkIj9xxxxxxEAIQAQ"); // Remplace par l’ID de l’achievement


    //exemple pour voir les succes sur un bouton

    //public void ShowAchievements() {
    //Social.ShowAchievementsUI();
}
