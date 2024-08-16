using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    public InputField ID_input;
    public InputField PW_input;
    public InputField Nick_input;
    [SerializeField] private Text Log;
    [SerializeField] private GameObject completePanel, RoomselectUI;

    private void OnEnable()
    {
        completePanel.SetActive(false);
        ID_input.text = "";
        PW_input.text = "";
        Nick_input.text = "";
        Log.text = "";
        // 로그인 상태(수정)인 경우 - ID는 수정 불가
        if (SQLManager.instance.isLogin)
        {
            ID_input.text = SQLManager.instance.info.User_id;
            ID_input.enabled = false;
        }
    }

    public void Save_btn()
    {
        // 실패 1: 빈 칸이 있을 때
        if (ID_input.text.Equals(string.Empty) || PW_input.text.Equals(string.Empty) || Nick_input.text.Equals(string.Empty))
        {
            Log.text = "비어있는 칸이 있습니다.";
        }
        else
        {
            int ID_dup = SQLManager.instance.Isduplicate(true, ID_input.text);
            int Nick_dup = SQLManager.instance.Isduplicate(false, Nick_input.text);
            // 실패 2: ID 중복 - 수정(로그인 상태)의 경우는 판정 제외
            if (ID_dup > 0 && !SQLManager.instance.isLogin)
            {
                if (ID_dup > 1) Log.text = "오류가 발생하였습니다.";
                else Log.text = "이미 존재하는 ID입니다.";
            }
            // 실패 3: 닉네임 중복
            else if (Nick_dup > 0)
            {
                if (Nick_dup > 1) Log.text = "오류가 발생하였습니다.";
                else Log.text = "이미 존재하는 닉네임입니다.";
            }
            // 성공
            else
            {
                completePanel.SetActive(true);
                // 로그인 상태, 즉 수정인 경우
                if (SQLManager.instance.isLogin)
                {
                    Log.text = "수정 완료!";
                    SQLManager.instance.User_Edit(ID_input.text, PW_input.text, Nick_input.text);
                    // UI의 유저 정보 새로고침
                    RoomselectUI.SetActive(false);
                    RoomselectUI.SetActive(true);
                }
                // 비로그인 상태(가입)인 경우
                else
                {
                    Log.text = "가입 완료!";
                    SQLManager.instance.Register(ID_input.text, PW_input.text, Nick_input.text);
                }
            }
        }
    }

}
