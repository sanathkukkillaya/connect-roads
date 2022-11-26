namespace JTool
{
    using UnityEngine;
    using Facebook.Unity;
    using System.Collections.Generic;
    using System;


    /// <summary>
    /// A simplified class for facebook.
    /// </summary>
    public class FaceBook
    {

        public static bool IsReady { private set; get; }

        private const string LOG = "JTool(FACEBOOK): ";

        private static UserInfo m_UserInfo;

        #region public method

        #region Initialize
        /// <summary>
        /// Initialization of facebook sdk.
        /// This function must be called before doing any fb operations.
        /// 
        /// </summary>

        public static void Init(Action onSuccess = null, Action onFailure = null)
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
                onSuccess?.Invoke();
            }
            else
            {
                FB.Init(() =>
                {
                    if (FB.IsInitialized)
                    {
                        FB.ActivateApp();
                        onSuccess?.Invoke();
                    }
                    else
                    {
                        Debug.LogError(LOG + "Error in initialization.");
                        onFailure?.Invoke();
                    }

                });
            }
        }
        #endregion

        #region LogIn
        /// <summary>
        /// Logs in to facebook with read permission.
        /// Default permission : public_profile , emial.
        /// </summary>
        public static void LogIn(List<string> permissions = null, Action onSuccess = null, Action onFailure = null)
        {
            if (permissions == null)
            {
                permissions = new List<string> { "public_profile", "email", "user_birthday", "user_gender" };
            }

            FB.LogInWithReadPermissions(permissions, result => {

                if (result.Error == null)
                {
                    Debug.Log(LOG + "Logged in");
                    GetUserInfoCall();
                    onSuccess?.Invoke();
                    
                }
                else
                {
                    Debug.Log(LOG + "Failed log in");
                    onFailure?.Invoke();
                    
                }
            });
        }
        #endregion

        #region LogOut
        /// <summary>
        /// Log out of facebook
        /// </summary>
        public static void LogOut()
        {
            if (FB.IsInitialized)
            {
                FB.LogOut();
                IsReady = false;
            }
        }

        #endregion

        #region Invite
        #endregion

        #region Share
        /// <summary>
        /// post to facebook page
        /// </summary>
        public static void PostToPage()
        {
            //TODO share on fb page.
        }
        #endregion

        #region API Querry

        public static void APIQuerry(string querry, HttpMethod httpMethod, Action<string> onSuccess = null)
        {

            if (querry == null || querry.Length <= 0)
            {
                Debug.Log(LOG + "INVALID QUERRY");
                return;
            }



            FB.API(querry, httpMethod, result =>
            {
                if (result == null)
                {
                    Debug.Log(LOG + "QUERRY ERROR");
                    return;
                }

                if (result.Error != null)
                {
                    Debug.Log(LOG + result.Error);
                    return;
                }

                onSuccess?.Invoke(result.RawResult);
            });
        }
        #endregion

        #region Getters

        public static UserInfo GetUserInfo()
        {
            if (IsReady)
            {
                if (m_UserInfo != null)
                {
                    Debug.Log(LOG + m_UserInfo.IsLoggedIn);
                    return m_UserInfo;
                }
                
                Debug.Log(LOG + "NULL");
            }
            else
            {
                Debug.Log(LOG + "NOT READY");
                
            }

            return null;
        }

        public static string GetAccessToken()
        {
            if (FB.IsInitialized)
            {
                if(AccessToken.CurrentAccessToken != null)
                {
                    return AccessToken.CurrentAccessToken.TokenString;
                }
            }

            return null;
        }

        #endregion


        #endregion

        #region Internal calls
        private static void GetUserInfoCall()
        {
            string _userInfoQuerry = "/me?fields=name,first_name,middle_name,last_name,email,birthday,gender";

            APIQuerry(_userInfoQuerry, HttpMethod.GET, result => {

                m_UserInfo = new UserInfo(result);

                if (m_UserInfo.IsLoggedIn)
                {
                    Debug.Log(LOG + "USER_INFO READY");
                    IsReady = true;
                    Debug.Log(LOG + m_UserInfo.Name + " , " + m_UserInfo.FirstName + " , " + m_UserInfo.MiddleName + " , " + m_UserInfo.LastName + " , " + m_UserInfo.UserId + " , " + m_UserInfo.Email + " , " + m_UserInfo.Birthday + " , " + m_UserInfo.Gender);
                }

            });

        }
        #endregion

        #region subclass and struct
        public class UserInfo
        {
            private string name;
            private string firstName;
            private string middleName;
            private string lastName;
            private string userId;
            private string eMail;
            private string birthday;
            private string gender;
           // private List<Friend> friends;
            private bool isLoggedIn = false;


            public string Name { get { return name;} }
            public string FirstName { get { return firstName; } }
            public string MiddleName { get { return middleName; } }
            public string LastName { get { return lastName; } }
            public string UserId { get { return userId; } }
            public string Email { get { return eMail; } }
            public string Birthday { get { return birthday; } }
            public string Gender { get { return gender; } }
            //public List<Friend> Friends { get { return friends; } }
            public bool IsLoggedIn { get { return isLoggedIn; } }


            public UserInfo(string rawData)
            {
                if(rawData!=null || rawData.Length > 0)
                {
                    Process(rawData);
                }
            }

            private void Process(string rawData)
            {

                Debug.Log(rawData);
                var data =  Facebook.MiniJSON.Json.Deserialize(rawData) as IDictionary<string,object>;

                

                if(data != null && data.Count > 0)
                {
                    if (data.ContainsKey("name"))
                        name = data["name"].ToString();
                    if (data.ContainsKey("first_name"))
                        firstName = data["first_name"].ToString();
                    if (data.ContainsKey("middle_name"))
                        middleName = data["middle_name"].ToString();
                    if (data.ContainsKey("last_name"))
                        lastName = data["last_name"].ToString();
                    if (data.ContainsKey("id"))
                        userId = data["id"].ToString();
                    if (data.ContainsKey("email"))
                        eMail = data["email"].ToString();
                    if (data.ContainsKey("birthday"))
                        birthday = data["birthday"].ToString();
                    if (data.ContainsKey("friends"))
                    {
                        
                    }
                    if (data.ContainsKey("gender"))
                        gender = data["gender"].ToString();

                    if(userId != null)
                    {
                        isLoggedIn = true;
                    }
                }
            }


        }


        //public class FriendsData
        //{
        //    public List<Friend> data;
        //    object summary;
        //}

        //public class Friend
        //{
        //    public string id;
        //    public string name;
        //}
        #endregion
    }
}
