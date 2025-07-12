using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaEdificio : MonoBehaviour
{
	[SerializeField] private string escenaSig;
	[SerializeField] private int numMenu;							// 1: serenidad, 2: asertividad, 3: resiliencia, 4: tienda
	[SerializeField] private TransicionEscena transicionEscena;
	[SerializeField] private AudioClip newMusicClip;
	[SerializeField] private float duracionFade;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			// Antes de cambiar de escena guardamos la posicion del personaje indicando el menu al que entra
			PlayerPrefs.SetFloat("EntrandoEscena", numMenu);

			StartCoroutine(transicionEscena.CambiarEscena(escenaSig, newMusicClip, duracionFade));
		}
	}
}
