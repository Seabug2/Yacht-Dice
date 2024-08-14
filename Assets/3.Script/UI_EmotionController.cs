using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UI_EmotionController : NetworkBehaviour
{
    /*
    [SerializeField] private GameObject questionMark;

    [SerializeField] private GameObject bee_angry;
    [SerializeField] private GameObject bee_happy;
    [SerializeField] private GameObject bee_sad;

    [SerializeField] private GameObject cat_angry;
    [SerializeField] private GameObject cat_happy;
    [SerializeField] private GameObject cat_sad;

    [SerializeField] private GameObject poro_happy;

    [SerializeField] private GameObject rammus_good;

    [SerializeField] private GameObject teemo_good;
    [SerializeField] private GameObject teemo_surprise;
    */

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
            }else
        {
            lastInputTime = Time.time;
            //키를 눌렀을 때의 처리
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

    //
    [Command]
    void CmdTriggerAnimation(string triggerName)
    {
        RpcTriggerAnimation(triggerName);
    }

    [ClientRpc]
    void RpcTriggerAnimation(string triggerName)
    {
        emotion_ani.SetTrigger(triggerName);
    }

}
