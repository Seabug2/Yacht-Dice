using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class InputFieldFilter : MonoBehaviour
{
    public InputField inputField;

    void Start()
    {
        // InputField의 텍스트가 변경될 때마다 검증 메서드 호출
        inputField.onValueChanged.AddListener(ValidateInput);
    }

    private void ValidateInput(string input)
    {
        // 입력된 값이 알파벳, 숫자, 1바이트 특수문자만 포함되도록 정규식 패턴 설정
        string pattern = @"^[a-zA-Z0-9!""#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]*$";

        // 정규식 패턴을 사용하여 입력값을 검증
        if (Regex.IsMatch(input, pattern))
        {
            print(".");
            // 입력값이 유효한 경우, 텍스트 필드를 업데이트하지 않음
            return;
        }
        else
        {
            print("x");
            // 입력값이 유효하지 않은 경우, 알파벳, 숫자, 1바이트 특수문자만 포함된 값으로 업데이트
            inputField.text = Regex.Replace(input, @"[^a-zA-Z0-9!""#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]", "");
        }
    }
}
