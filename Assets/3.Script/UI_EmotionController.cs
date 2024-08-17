using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UI_EmotionController : NetworkBehaviour
{

    /*


    private Animator emotion_ani;
    private bool isCanInput = true;

    float lastInputTime = 0;
    [SerializeField]
    float delayTime = 1;

    private void Awake()
    {
        transform.SetParent(GameObject.Find("Canvas_Emotion").transform);
    }
    private void Start()
    {
        if(!isLocalPlayer)
        {
            Destroy(this);
        }
        emotion_ani = GetComponent<Animator>();
        lastInputTime = 0;
    }
    private void Update()
    {
        if (Input.anyKeyDown){
            if((Time.time - lastInputTime) < delayTime)
            {
                return;
            }
            else
            {
                lastInputTime = Time.time;
                //Ű�� ������ ���� ó��
                Emotion();
            }
        }
        
    }

    public void Emotion()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CmdTriggerAnimation("IsQuestion");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CmdTriggerAnimation("IsBeeAngry");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CmdTriggerAnimation("IsBeeHappy");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CmdTriggerAnimation("IsBeeSad");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CmdTriggerAnimation("IsCatAngry");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            CmdTriggerAnimation("IsCatHappy");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            CmdTriggerAnimation("IsCatSad");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            CmdTriggerAnimation("IsPoroHappy");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            CmdTriggerAnimation("IsRammusGood");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            CmdTriggerAnimation("IsTeemoGood");
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            CmdTriggerAnimation("IsTeemoSurprise");
        }
    }

    private IEnumerator BlockInput(float time)
    {
        isCanInput = false;
        yield return new WaitForSeconds(time);
        isCanInput = true;
    }
    


    [Command]
    void CmdTriggerAnimation(string triggerName)
    {
        //RpcTriggerAnimation(triggerName);
    }

    [ClientRpc]
    void RpcTriggerAnimation(string triggerName)
    {
        //emotion_ani.SetTrigger(triggerName);
    }
    */
    public class PlayerController : NetworkBehaviour
    {
        public Emotion emotion;

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
            emotion.SetForm(input - 1);  // 0���� �����ϴ� �ε����� �°� -1
        }
    }
}
