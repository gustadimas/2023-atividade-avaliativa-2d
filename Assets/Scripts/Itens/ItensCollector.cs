using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItensCollector : MonoBehaviour
{
    private int coins = 0;

    [SerializeField] Text coinsText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coins"))
        {
            Destroy(collision.gameObject);
            coins++;
            coinsText.text = "Coins: " + coins;
            AudioManager.instance.Play("Itens");

            if (coins == 4)
            {
                SceneManager.LoadScene("Traps");
            }
        }
    }
}
