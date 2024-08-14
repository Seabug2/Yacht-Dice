using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class InputFieldFilter : MonoBehaviour
{
    public InputField inputField;

    void Start()
    {
        // InputField�� �ؽ�Ʈ�� ����� ������ ���� �޼��� ȣ��
        inputField.onValueChanged.AddListener(ValidateInput);
    }

    private void ValidateInput(string input)
    {
        // �Էµ� ���� ���ĺ�, ����, 1����Ʈ Ư�����ڸ� ���Եǵ��� ���Խ� ���� ����
        string pattern = @"^[a-zA-Z0-9!""#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]*$";

        // ���Խ� ������ ����Ͽ� �Է°��� ����
        if (Regex.IsMatch(input, pattern))
        {
            print(".");
            // �Է°��� ��ȿ�� ���, �ؽ�Ʈ �ʵ带 ������Ʈ���� ����
            return;
        }
        else
        {
            print("x");
            // �Է°��� ��ȿ���� ���� ���, ���ĺ�, ����, 1����Ʈ Ư�����ڸ� ���Ե� ������ ������Ʈ
            inputField.text = Regex.Replace(input, @"[^a-zA-Z0-9!""#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]", "");
        }
    }
}
