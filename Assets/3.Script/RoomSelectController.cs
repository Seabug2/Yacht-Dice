using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelectController : MonoBehaviour
{
    [SerializeField] private GameObject IPInputUI, LoginUI, RegisterUI;

    public void Host_btn()
    {
        // ���� ������ �̵�
    }

    public void Guest_btn()
    {
        IPInputUI.SetActive(true);
    }

    public void UserEdit_btn()
    {
        RegisterUI.SetActive(true);
    }

    public void Back_btn()
    {
        SQLManager.instance.isLogin = false;
        LoginUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void IPInput_Join_btn()
    {
        // ���� ������ �̵�
    }

    public void IPInput_Back_btn()
    {
        IPInputUI.SetActive(false);
    }

}
