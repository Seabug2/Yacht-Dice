using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

// ID와 비밀번호에서 한글 입력 방지용 스크립트입니다.
public class InputFieldFilter : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public InputField inputField;

    // OnSelect 메서드: InputField가 선택되었을 때 호출됩니다.
    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(DisableIMEAfterDelay());
    }

    private IEnumerator DisableIMEAfterDelay()
    {
        yield return null; // 한 프레임 대기
        Input.imeCompositionMode = IMECompositionMode.Off;  // IME 모드를 비활성화합니다.
    }



    // OnDeselect 메서드: InputField의 선택이 해제되었을 때 호출됩니다.
    public void OnDeselect(BaseEventData eventData)
    {
        Input.imeCompositionMode = IMECompositionMode.Auto;  // IME 모드를 활성화합니다.
    }
}
