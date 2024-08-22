using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomSelectController : MonoBehaviour
{
    [SerializeField] private GameObject LoginUI, EditUI;
    [SerializeField] private Text Show_Userinfo;

    private void OnEnable()
    {
        if (SQLManager.instance.isGuest)
        {
            Show_Userinfo.text = $"비로그인\n\n\n ";
        }
        else
        {
            User_info u = SQLManager.instance.info;
            Show_Userinfo.text = $"{u.User_nickname}\n\n{u.wins}승 {u.loses}패\nBest: {u.highscore}";
        }
    }

    // 뒤로 가기(로그아웃)
    public void Back_btn()
    {
        SQLManager.instance.isLogin = false;
        SQLManager.instance.isGuest = false;
        LoginUI.SetActive(true);
        gameObject.SetActive(false);
    }


}
