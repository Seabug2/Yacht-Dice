using UnityEngine;
using Mirror;

public class UI_EmotionController : NetworkBehaviour
{
    public Animator anim;

    private void Start()
    {
        if (isLocalPlayer)
        {
            anim.GetComponent<RectTransform>().anchoredPosition = new Vector2(400,0);
        }
        else
        {
            anim.GetComponent<RectTransform>().anchoredPosition = new Vector2(680,0);
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        // 키 입력 감지 및 처리
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CmdHandleInput(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CmdHandleInput(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CmdHandleInput(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CmdHandleInput(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CmdHandleInput(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            CmdHandleInput(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            CmdHandleInput(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            CmdHandleInput(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            CmdHandleInput(9);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            CmdHandleInput(10);
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            CmdHandleInput(11);
        }
    }

    // 클라이언트의 입력을 서버로 전송하는 커맨드 메소드
    [Command]
    private void CmdHandleInput(int input)
    {
        RpcSetupResource(input);
    }

    // 서버에서 모든 클라이언트에게 이미지와 사운드를 설정하라는 명령을 보냄
    [ClientRpc]
    private void RpcSetupResource(int input)
    {
        anim.SetTrigger($"Trigger_{input}");  // 0부터 시작하는 인덱스에 맞게 -1
    }
}