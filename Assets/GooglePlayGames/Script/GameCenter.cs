namespace SDK
{
    using UnityEngine.SocialPlatforms;
    using UnityEngine;

    #if UNITY_IOS
	public class GameCenter
    {
        #region LogIn
        public static void Intialize()
        {
            if (!Social.localUser.authenticated)
            {
                Social.localUser.Authenticate(result => { });
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
        //public static void AddToLeaderboard(string boardId, long score)
        //{
        //    PlayGamesPlatform.Instance.ReportScore(score, boardId, x => { });
        //}
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
        public static void AddToAchievement(string achievemenId, double progress)
        {
            Social.ReportProgress(achievemenId, progress, x => { });
        }

        /// <summary>
        /// Increments Achievement.
        /// </summary>
        //public static void IncrementAchievement(string achievementId, int step)
        //{
        //    PlayGamesPlatform.Instance.IncrementAchievement(achievementId, step, x => { });
        //}
        #endregion

    }
#endif
}

