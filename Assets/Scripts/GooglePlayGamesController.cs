using SDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooglePlayGamesController : MonoBehaviour
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
        m_Result.text = "LoggedIn";
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

    public string GetUserId()
    {
        string userId = GooglePlay.GetUserId();
        m_Result.text = userId;
        return userId;
    }

    public string GetUserName()
    {
        string userName = GooglePlay.GetUserName();
        m_Result.text = userName;
        return userName;
    }

    public void AddAchievement()
    {
        GooglePlay.AddToAchievement(GPGSId.achievement_achievement_2, 500, success => {


            Debug.Log("GOOGLE PLAY : " + success);


        });
    }

    public void LogOut()
    {
        GooglePlay.LogOut();
        m_Result.text = "logged out";
    }

    public void AddToBoard()
    {
        GooglePlay.AddToLeaderboard(GPGSId.leaderboard_test_leaderboard, 10, success => {


            Debug.Log("GOOGLE PLAY : " + success);

        });
        m_Result.text = "Added to board";
    }
}
