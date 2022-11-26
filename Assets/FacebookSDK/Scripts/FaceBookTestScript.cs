using UnityEngine;
using JTool;
using UnityEngine.UI;
using System.Collections;

public class FaceBookTestScript : MonoBehaviour
{
    [SerializeField] private Text m_Result;

    private FaceBook.UserInfo userInfo;

    public void OnInitialize()
    {
       FaceBook.Init(()=> {
           m_Result.text = "Initialized";
           Debug.Log("FACEBOOK TEST : "+FaceBook.GetAccessToken());
       });
    }

    public void LogIn()
    {
        FaceBook.LogIn(null, () => {
            m_Result.text = "LogedIn";
        },() => {
            m_Result.text = "Failed to Log In";
        });
    }


    public void Load()
    {
        Debug.Log("Called Load test call");
        //FaceBook.Load();
        StartCoroutine(WaitTillReady());
       
        if(userInfo == null)
        {
            Debug.Log("FACEBOOK TEST NULLL");
        }
        else
        {
            Debug.Log("FACEBOOK TEST " + userInfo.UserId);
        }
    }

    public void GetUserId()
    {
        if (userInfo.IsLoggedIn)
        {
            m_Result.text = userInfo.UserId;
        }
    }

    public void LogOut()
    {
        FaceBook.LogOut();
        m_Result.text = "loged out";
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
