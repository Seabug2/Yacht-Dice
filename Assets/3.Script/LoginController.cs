using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public InputField ID_input;
    public InputField PW_input;
    [SerializeField] private Text Log;
    [SerializeField] private GameObject selectUI, RegisterPn, ExitUI;

    public void Login_btn()
    {
        // ���� 1: �� ĭ�� ���� ��
        if (ID_input.text.Equals(string.Empty) || PW_input.text.Equals(string.Empty))
        {
            Log.text = "���̵�� ��й�ȣ�� �Է��ϼ���.";
        }
        // ���� 2: ��ġ�ϴ� �÷��� ���� ��(ID ���ų� PW Ʋ����)
        else if (!SQLManager.instance.Login(ID_input.text, PW_input.text))
        {
            Log.text = "���̵� �Ǵ� ��й�ȣ�� �߸��Ǿ����ϴ�.";
        }
        // ���� - �� ���� ȭ������
        else
        {
            User_info info = SQLManager.instance.info;
            selectUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
