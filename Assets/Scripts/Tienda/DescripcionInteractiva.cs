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
			// Escalamos los valores de posición basados en la resolución actual en comparación con la resolución de referencia (1920x1080)
			float scaleX = Screen.width / 1920f;
			float scaleY = Screen.height / 1080f;

			// Ajustamos la posición del menú flotante
			Vector3 offset = new Vector3(252 * scaleX, 242 * scaleY, 0);
			menuFlotante.transform.position = Input.mousePosition + offset;
		}
	}

	// Método que se llama cuando el ratón entra en el objeto
	public void OnPointerEnter(PointerEventData eventData)
	{
		isPointerOver = true;
		textoPanel.text = textoDescripcion;
		menuFlotante.SetActive(true);
	}

	// Método que se llama cuando el ratón sale del objeto
	public void OnPointerExit(PointerEventData eventData)
	{
		isPointerOver = false;

		// Invoca una corrutina para verificar si el ratón aún está sobre el objeto
		StartCoroutine(ComprobarSiSigueEncima());
	}

	private IEnumerator ComprobarSiSigueEncima()
	{
		// Espera un brevemente para asegurarse de que el ratón no esté aún sobre el objeto
		yield return new WaitForEndOfFrame();

		// Si el ratón no está sobre el objeto, desactiva el menú
		if (!isPointerOver)
		{
			menuFlotante.SetActive(false);
		}
	}
}
