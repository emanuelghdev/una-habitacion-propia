using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermitirDialogos : MonoBehaviour
{

	public GameObject controladorDialogosNPC;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			controladorDialogosNPC.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			controladorDialogosNPC.SetActive(false);
		}
	}
}
