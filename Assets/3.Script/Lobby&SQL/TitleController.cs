using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public InputField ID_input;
    public InputField PW_input;
    [SerializeField] private Text Log;
    [SerializeField] private GameObject LoginUI, RoomselectUI;

    private void Start()
    {
        // 게임 완료 후 타이틀 이동(즉, 이미 로그인 상태)일 경우
        if (SQLManager.instance.ServerFail) Log.text = "서버 연결에 실패했습니다.";
        if (SQLManager.instance.isLogin) LoginComplete();
    }

    public void Login_btn()
    {
        if (SQLManager.instance.ServerFail) return;
        // 실패 1: 빈 칸이 있을 때
        if (ID_input.text.Equals(string.Empty) || PW_input.text.Equals(string.Empty))
        {
            Log.text = "아이디와 비밀번호를 입력하세요.";
        }
        // 실패 2: 일치하는 컬럼이 없을 때(ID 없거나 PW 틀리면)
        else if (!SQLManager.instance.Login(ID_input.text, PW_input.text))
        {
            Log.text = "아이디 또는 비밀번호가 잘못되었습니다.";
        }
        // 성공 - 방 선택 화면으로
        else
        {
            // User_info info = SQLManager.instance.info;
            LoginComplete();
        }
    }
 
    public void WithoutLogin_btn()
    {
        SQLManager.instance.Guest();
        LoginComplete();
    }

    public void LoginComplete()
    {
        ID_input.text = "";
        PW_input.text = "";
        if (!SQLManager.instance.ServerFail) Log.text = "";
        RoomselectUI.SetActive(true);
        LoginUI.SetActive(false);
    }

}
