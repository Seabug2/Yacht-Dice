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

        // Ű �Է� ���� �� ó��
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

    // Ŭ���̾�Ʈ�� �Է��� ������ �����ϴ� Ŀ�ǵ� �޼ҵ�
    [Command]
    private void CmdHandleInput(int input)
    {
        RpcSetupResource(input);
    }

    // �������� ��� Ŭ���̾�Ʈ���� �̹����� ���带 �����϶�� ����� ����
    [ClientRpc]
    private void RpcSetupResource(int input)
    {
        anim.SetTrigger($"Trigger_{input}");  // 0���� �����ϴ� �ε����� �°� -1
    }
}