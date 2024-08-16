using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

// ID�� ��й�ȣ���� �ѱ� �Է� ������ ��ũ��Ʈ�Դϴ�.
public class InputFieldFilter : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public InputField inputField;

    // OnSelect �޼���: InputField�� ���õǾ��� �� ȣ��˴ϴ�.
    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(DisableIMEAfterDelay());
    }

    private IEnumerator DisableIMEAfterDelay()
    {
        yield return null; // �� ������ ���
        Input.imeCompositionMode = IMECompositionMode.Off;  // IME ��带 ��Ȱ��ȭ�մϴ�.
    }



    // OnDeselect �޼���: InputField�� ������ �����Ǿ��� �� ȣ��˴ϴ�.
    public void OnDeselect(BaseEventData eventData)
    {
        Input.imeCompositionMode = IMECompositionMode.Auto;  // IME ��带 Ȱ��ȭ�մϴ�.
    }
}
