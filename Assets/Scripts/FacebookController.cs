using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JTool;
using UnityEngine.UI;

public class FacebookController : MonoBehaviour
{
    [SerializeField] private Text m_Result;

    private FaceBook.UserInfo userInfo;

    public void OnInitialize()
    {
        FaceBook.Init(() =>
        {
            m_Result.text = "Initialized";
            Debug.Log("FACEBOOK TEST : " + FaceBook.GetAccessToken());
        });
    }

    public void LogIn()
    {
        FaceBook.LogIn(null, () =>
        {
            StartCoroutine(WaitTillReady());
            m_Result.text = "LoggedIn";
        }, () =>
        {
            m_Result.text = "Failed to Log In";
        });
    }


    public void Load()
    {
        Debug.Log("Called Load test call");
        //FaceBook.Load();
        StartCoroutine(WaitTillReady());

        if (userInfo == null)
        {
            Debug.Log("FACEBOOK TEST NULLL");
        }
        else
        {
            Debug.Log("FACEBOOK TEST " + userInfo.UserId);
        }
    }

    public bool IsLoggedIn()
    {        
        if (FaceBook.IsReady && userInfo != null)
            return userInfo.IsLoggedIn;
        else
            return false;
    }

    public string GetUserId()
    {
        if (userInfo.IsLoggedIn)
        {
            m_Result.text = userInfo.UserId;
        }
        return userInfo.UserId;
    }

    public FaceBook.UserInfo GetUserInfo()
    {
        return userInfo;
    }

    public void LogOut()
    {
        FaceBook.LogOut();
        m_Result.text = "logged out";
    }

    public void GetEmailId()
    {
        if (userInfo.IsLoggedIn)
        {
            m_Result.text = userInfo.Email;
        }
    }

    IEnumerator WaitTillReady()
    {
        while (!FaceBook.IsReady)
        {
            m_Result.text = "Loading...";
            yield return null;
        }
        m_Result.text = "Ready";
        userInfo = FaceBook.GetUserInfo();

    }
}
