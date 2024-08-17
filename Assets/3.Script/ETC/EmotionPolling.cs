using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionPolling : MonoBehaviour
{

    public GameObject prefab; 
    public Transform spawnPoint;
    public Emotion emotion; 

    public Queue<GameObject> objectQueue = new Queue<GameObject>();
    private int imageIndex = 0; 

    private void Start()
    {
        
        for (int i = 0; i < 5; i++)
        {
            SpawnNewObject();
        }
    }

    
    public void SpawnNewObject()
    {
        GameObject newObj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        objectQueue.Enqueue(newObj);

        
        if (objectQueue.Count > 5)
        {
            GameObject oldObj = objectQueue.Dequeue();
            Destroy(oldObj);
        }

       
        Emotion emotion = newObj.GetComponent<Emotion>();
        if (emotion != null)
        {
            emotion.Setup((imageIndex % 11) + 1);
            imageIndex++;
        }
    }
}
