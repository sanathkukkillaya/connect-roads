using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_IOS

using SDK;
public class GameCenterHandler : MonoBehaviour
{
     [SerializeField] private Text m_Result;



    public void LogIn()
    {
        GameCenter.Intialize();
        m_Result.text = "LogedIn";
        
    }

    public void ShowLeaderBoard()
    {
        GameCenter.ShowLeaderboard();
        m_Result.text = "Leaderboard";
    }

    public void ShowAchievement()
    {
        GameCenter.ShowAchievement();

    }

    public void GetUserId()
    {
        m_Result.text = GameCenter.GetUserId();
    }

    //public void AddAchievement()
    //{
    //    a.AddToAchievement(GPGSId.achievement_achievement_1, 10);
    //}

    //public void LogOut()
    //{
    //    a.LogOut();
    //    m_Result.text = "loged out";
    //}

    //public void AddToBoard()
    //{
    //    GooglePlay.AddToLeaderboard(GPGSId.leaderboard_test_leaderboard, 10);
    //    m_Result.text = "Added to board";
    //} 

}
#endif
