using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int fruits = 0;
    public static int TotalCollectedFruits = 0;
    public const int TotalFruits = 64;

    [SerializeField] private Text fruitText;

    [SerializeField] private AudioSource collectionSoundEffect;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Fruit"))
        {
            collectionSoundEffect.Play();
            //Destroy(collision.gameObject);
            // Use object pooling instead of destroying the object
            collision.gameObject.SetActive(false);
            fruits++;
            TotalCollectedFruits++;
            fruitText.text = "Fruits: " + fruits;
        }
    }
}
