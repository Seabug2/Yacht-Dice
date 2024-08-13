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
    [SerializeField] private GameObject LoginUI;

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
            // 실패 2: ID 중복
            if (ID_dup > 0)
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
                if (SQLManager.instance.isLogin)
                {
                    Log.text = "수정 완료!";
                    SQLManager.instance.User_Edit(ID_input.text, PW_input.text, Nick_input.text);
                }
                else
                {
                    Log.text = "가입 완료!";
                    SQLManager.instance.Register(ID_input.text, PW_input.text, Nick_input.text);
                }
            }
        }
    }

    public void Back_btn()
    {
        if(!SQLManager.instance.isLogin) LoginUI.SetActive(true);
        gameObject.SetActive(false);
    }

}
