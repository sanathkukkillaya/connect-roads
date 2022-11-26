#if UNITY_ANDROID
namespace SDK
{
    using UnityEngine;
    using System;

    using GooglePlayGames;
    using GooglePlayGames.BasicApi;

    /// <summary>
    /// Google play Sdk.
    /// </summary>
    public class GooglePlay
    {

        private const string LOG = "JTool(GooglePlay) :";

        public static bool IsReady { private set; get; }

        #region Initialize
        /// <summary>
        /// 
        /// </summary>
        public static void Init()
        {
            PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(configuration);
            PlayGamesPlatform.Activate();
            Debug.Log("Google Play Activated");
        }
        #endregion

        #region LogIn
        /// <summary>
        /// 
        /// </summary>
        public static void LogIn(Action<bool> onLogIn = null)
        {
            Social.localUser.Authenticate(success =>
            {
                if (success)
                {
                    IsReady = true;
                }

                onLogIn?.Invoke(success);
            });
        }
        #endregion

        #region LogOut
        /// <summary>
        /// 
        /// </summary>
        public static void LogOut()
        {
            if (Social.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.SignOut();
            }
            
        }
        #endregion

        #region Fetch Id
        /// <summary>
        /// 
        /// </summary>
        public static string GetUserId()
        {
            return Social.localUser.id;
        }
        #endregion

        #region Fetch Name
        /// <summary>
        /// 
        /// </summary>
        public static string GetUserName()
        {
            return Social.localUser.userName;
        }
        #endregion

        #region Leaderboard
        /// <summary>
        /// 
        /// </summary>
        public static void ShowLeaderboard()
        {
            Social.ShowLeaderboardUI();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void AddToLeaderboard(string boardId ,long score,Action<bool> onAdd = null)
        {
            PlayGamesPlatform.Instance.ReportScore(score,boardId,onAdd);
        }
        #endregion

        #region Achievement
        /// <summary>
        /// 
        /// </summary>
        public static void ShowAchievement()
        {
            Social.ShowAchievementsUI();
        }

        /// <summary>
        /// Adds progress to non-increamental achievement.
        /// </summary>
        public static void AddToAchievement(string achievemenId , double progress , Action<bool> onAdd = null)
        {
            Social.ReportProgress(achievemenId, progress, onAdd);
        }

        /// <summary>
        /// Increments Achievement.
        /// </summary>
        public static void IncrementAchievement(string achievementId , int step , Action<bool> onIncrement = null)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(achievementId, step, onIncrement);
        }
        #endregion
    }
}
#endif