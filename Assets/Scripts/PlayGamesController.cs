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
                Debug.Log("Connecté à Google Play Games");
            }
            else
            {
                Debug.Log("Échec de la connexion");
            }
        });
    }


    // Débloquer un achievement
    public void UnlockAchievement(string achievementID)
    {
        Social.ReportProgress(achievementID, 100.0f, success => {
            if (success)
            {
                Debug.Log("Achievement débloqué !");
            }
            else
            {
                Debug.Log("Échec du déblocage");
            }
        });
    }

    //exemple d'utilisation

    //UnlockAchievement("CgkIj9xxxxxxEAIQAQ"); // Remplace par l’ID de l’achievement


    //exemple pour voir les succès sur un bouton

    //public void ShowAchievements() {
    //Social.ShowAchievementsUI();
}
