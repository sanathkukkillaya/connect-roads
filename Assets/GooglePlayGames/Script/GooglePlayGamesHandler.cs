
using SDK;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_ANDROID
public class GooglePlayGamesHandler : MonoBehaviour
{

    [SerializeField] private Text m_Result;


    public void OnInitialize()
    {
        GooglePlay.Init();
        m_Result.text = "Initialized";

    }

    public void LogIn()
    {
        GooglePlay.LogIn(success => {

            Debug.Log("Google : " + success);

        });
        m_Result.text = "LogedIn";
    }

    public void ShowLeaderBoard()
    {
        GooglePlay.ShowLeaderboard();
        m_Result.text = "Leaderboard";
    }

    public void ShowAchievement()
    {
        GooglePlay.ShowAchievement();
        
    }

    public void GetUserId()
    {
        m_Result.text = GooglePlay.GetUserId();
    }

    public void AddAchievement()
    {
        GooglePlay.AddToAchievement(GPGSId.achievement_achievement_2, 500,success=> {


            Debug.Log("GOOGLE PLAY : " + success);


        });
    }

    public void LogOut()
    {
        GooglePlay.LogOut();
        m_Result.text = "loged out";
    }

    public void AddToBoard()
    {
        GooglePlay.AddToLeaderboard(GPGSId.leaderboard_test_leaderboard, 10 ,success => {


            Debug.Log("GOOGLE PLAY : " + success);

        });
        m_Result.text = "Added to board";
    }

}
#endif