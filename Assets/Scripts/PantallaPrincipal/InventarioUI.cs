using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventarioUI : MonoBehaviour
{
	public GameObject objetoUIPrefab; 
	public Transform slotsInventario;

	GameManager gameManager;

	private void Awake()
	{
		gameManager = FindObjectOfType<GameManager>();
	}

	public void UpdateInventorioUI()
	{
		// Limpia la lista actual
		foreach (Transform slot in slotsInventario)
		{
			foreach (Transform child in slot)
			{
				Destroy(child.gameObject);
			}
		}

		// Recorre los objetos en el inventario y crea un objeto UI para cada uno
		foreach (Objeto objeto in gameManager.objetosInventario)
		{
			// Encuentra el primer slot vacío
			Transform slotVacio = null;
			foreach (Transform slot in slotsInventario)
			{
				if (slot.childCount == 0)
				{
					slotVacio = slot;
					break;
				}
			}

			GameObject objetoUI = Instantiate(objetoUIPrefab, slotVacio);

			// Busca el hijo "ImagenObjeto" y cambia su sprite
			Transform imagenTransform = objetoUI.transform.Find("ImagenObjeto");
			if (imagenTransform != null)
			{
				Image imagenObjeto = imagenTransform.GetComponent<Image>();
				if (imagenObjeto != null)
				{
					imagenObjeto.sprite = Resources.Load<Sprite>(objeto.nombreIconoObjeto);
				}
			}

			// Busca el hijo "Cantidad" y cambia su texto
			Transform cantidadTransform = objetoUI.transform.Find("Cantidad");
			if (cantidadTransform != null)
			{
				TextMeshProUGUI cantidadTexto = cantidadTransform.GetComponent<TextMeshProUGUI>();
				if (cantidadTexto != null)
				{
					cantidadTexto.text = objeto.cantidadObjeto.ToString();
				}
			}
		}
	}
}
