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
        // ���� �Ϸ� �� Ÿ��Ʋ �̵�(��, �̹� �α��� ����)�� ���
        if (SQLManager.instance.ServerFail) Log.text = "���� ���ῡ �����߽��ϴ�.";
        if (SQLManager.instance.isLogin) LoginComplete();
    }

    public void Login_btn()
    {
        if (SQLManager.instance.ServerFail) return;
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
