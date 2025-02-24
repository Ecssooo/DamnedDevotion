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
                Debug.Log("Connect� � Google Play Games");
            }
            else
            {
                Debug.Log("�chec de la connexion");
            }
        });
    }


    // D�bloquer un achievement
    public void UnlockAchievement(string achievementID)
    {
        Social.ReportProgress(achievementID, 100.0f, success => {
            if (success)
            {
                Debug.Log("Achievement d�bloqu� !");
            }
            else
            {
                Debug.Log("�chec du d�blocage");
            }
        });
    }

    //exemple d'utilisation

    //UnlockAchievement("CgkIj9xxxxxxEAIQAQ"); // Remplace par l�ID de l�achievement


    //exemple pour voir les succ�s sur un bouton

    //public void ShowAchievements() {
    //Social.ShowAchievementsUI();
}
