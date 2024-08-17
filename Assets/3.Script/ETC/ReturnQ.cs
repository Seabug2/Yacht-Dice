using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnQ : MonoBehaviour
{
    EmotionPolling emotionPolling;

    private void Awake()
    {
        emotionPolling = GetComponent<EmotionPolling>();
    }
    private void OnDisable()
    {
        emotionPolling.objectQueue.Dequeue();
    }
}
