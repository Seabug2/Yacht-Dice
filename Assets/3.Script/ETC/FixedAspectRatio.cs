using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
    public float targetAspectRatio = 16.0f / 9.0f; // 고정할 화면 비율 (16:9)

    private int lastWidth;
    private int lastHeight;

    void Start()
    {
        // 초기 화면 크기 저장
        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }

    void Update()
    {
        // 화면 크기가 변경되었는지 확인
        if (Screen.width != lastWidth || Screen.height != lastHeight)
        {
            // 창 크기 비율 고정
            SetFixedAspectRatio();

            // 현재 화면 크기 저장
            lastWidth = Screen.width;
            lastHeight = Screen.height;
        }
    }

    void SetFixedAspectRatio()
    {
        // 현재 창의 너비와 높이 계산
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        // 너비를 기준으로 높이 조정
        if (currentAspectRatio > targetAspectRatio)
        {
            // 화면의 너비가 비율보다 크다면 높이를 늘려서 비율에 맞추기
            int newHeight = Mathf.RoundToInt(Screen.width / targetAspectRatio);
            Screen.SetResolution(Screen.width, newHeight, false);
        }
        else
        {
            // 화면의 높이가 비율보다 크다면 너비를 줄여서 비율에 맞추기
            int newWidth = Mathf.RoundToInt(Screen.height * targetAspectRatio);
            Screen.SetResolution(newWidth, Screen.height, false);
        }
    }
}
