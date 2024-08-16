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
        User_info u = SQLManager.instance.info;
        Show_Userinfo.text = $"{u.User_nickname}\n\n{u.wins}�� {u.loses}��\nBest: {u.highscore}";
    }

    // �ڷ� ����(�α׾ƿ�)
    public void Back_btn()
    {
        SQLManager.instance.isLogin = false;
        LoginUI.SetActive(true);
        gameObject.SetActive(false);
    }


}
