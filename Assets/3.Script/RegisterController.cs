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
        // ���� 1: �� ĭ�� ���� ��
        if (ID_input.text.Equals(string.Empty) || PW_input.text.Equals(string.Empty) || Nick_input.text.Equals(string.Empty))
        {
            Log.text = "����ִ� ĭ�� �ֽ��ϴ�.";
        }
        else
        {
            int ID_dup = SQLManager.instance.Isduplicate(true, ID_input.text);
            int Nick_dup = SQLManager.instance.Isduplicate(false, Nick_input.text);
            // ���� 2: ID �ߺ�
            if (ID_dup > 0)
            {
                if (ID_dup > 1) Log.text = "������ �߻��Ͽ����ϴ�.";
                else Log.text = "�̹� �����ϴ� ID�Դϴ�.";
            }
            // ���� 3: �г��� �ߺ�
            else if (Nick_dup > 0)
            {
                if (Nick_dup > 1) Log.text = "������ �߻��Ͽ����ϴ�.";
                else Log.text = "�̹� �����ϴ� �г����Դϴ�.";
            }
            // ����
            else
            {
                if (SQLManager.instance.isLogin)
                {
                    Log.text = "���� �Ϸ�!";
                    SQLManager.instance.User_Edit(ID_input.text, PW_input.text, Nick_input.text);
                }
                else
                {
                    Log.text = "���� �Ϸ�!";
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
