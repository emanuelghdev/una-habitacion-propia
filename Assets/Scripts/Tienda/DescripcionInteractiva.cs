using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DescripcionInteractiva : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public GameObject menuFlotante;
	public TMP_Text textoPanel;
	[TextArea(4, 6)] public string textoDescripcion;

	private bool isPointerOver = false;

	private void Update()
	{
		if(isPointerOver)
		{
			// Escalamos los valores de posici�n basados en la resoluci�n actual en comparaci�n con la resoluci�n de referencia (1920x1080)
			float scaleX = Screen.width / 1920f;
			float scaleY = Screen.height / 1080f;

			// Ajustamos la posici�n del men� flotante
			Vector3 offset = new Vector3(252 * scaleX, 242 * scaleY, 0);
			menuFlotante.transform.position = Input.mousePosition + offset;
		}
	}

	// M�todo que se llama cuando el rat�n entra en el objeto
	public void OnPointerEnter(PointerEventData eventData)
	{
		isPointerOver = true;
		textoPanel.text = textoDescripcion;
		menuFlotante.SetActive(true);
	}

	// M�todo que se llama cuando el rat�n sale del objeto
	public void OnPointerExit(PointerEventData eventData)
	{
		isPointerOver = false;

		// Invoca una corrutina para verificar si el rat�n a�n est� sobre el objeto
		StartCoroutine(ComprobarSiSigueEncima());
	}

	private IEnumerator ComprobarSiSigueEncima()
	{
		// Espera un brevemente para asegurarse de que el rat�n no est� a�n sobre el objeto
		yield return new WaitForEndOfFrame();

		// Si el rat�n no est� sobre el objeto, desactiva el men�
		if (!isPointerOver)
		{
			menuFlotante.SetActive(false);
		}
	}
}
