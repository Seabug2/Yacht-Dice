using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
    public float targetAspectRatio = 16.0f / 9.0f; // ������ ȭ�� ���� (16:9)

    private int lastWidth;
    private int lastHeight;

    void Start()
    {
        // �ʱ� ȭ�� ũ�� ����
        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }

    void Update()
    {
        // ȭ�� ũ�Ⱑ ����Ǿ����� Ȯ��
        if (Screen.width != lastWidth || Screen.height != lastHeight)
        {
            // â ũ�� ���� ����
            SetFixedAspectRatio();

            // ���� ȭ�� ũ�� ����
            lastWidth = Screen.width;
            lastHeight = Screen.height;
        }
    }

    void SetFixedAspectRatio()
    {
        // ���� â�� �ʺ�� ���� ���
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        // �ʺ� �������� ���� ����
        if (currentAspectRatio > targetAspectRatio)
        {
            // ȭ���� �ʺ� �������� ũ�ٸ� ���̸� �÷��� ������ ���߱�
            int newHeight = Mathf.RoundToInt(Screen.width / targetAspectRatio);
            Screen.SetResolution(Screen.width, newHeight, false);
        }
        else
        {
            // ȭ���� ���̰� �������� ũ�ٸ� �ʺ� �ٿ��� ������ ���߱�
            int newWidth = Mathf.RoundToInt(Screen.height * targetAspectRatio);
            Screen.SetResolution(newWidth, Screen.height, false);
        }
    }
}
