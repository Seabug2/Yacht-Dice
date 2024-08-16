using UnityEngine;

public class QuitApp : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ������ ���¿��� �÷��� �ߴ�
#else
        Application.Quit(); // ����� ���ӿ����� ���� ����
#endif
    }
}
