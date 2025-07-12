using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartel : MonoBehaviour
{
	[SerializeField, TextArea(4,6)] public string text = "";
	GameManager gameManager;

    void Start()
    {
		gameManager = FindObjectOfType<GameManager>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")){
			gameManager.MostrarTexto(text);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			gameManager.OcultarTexto();
		}
	}
}
