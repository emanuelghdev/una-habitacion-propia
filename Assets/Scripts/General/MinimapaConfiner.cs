using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapaConfiner : MonoBehaviour
{
	public Camera minimapaCamera;
	public Rect limiteVisible;			// Define los l�mites en el espacio del mundo

	private void Update()
	{
		AjustarLimiteCamara();
	}

	void AjustarLimiteCamara()
	{
		// Obtenemos la posici�n actual de la c�mara
		Vector3 posicionCamara = minimapaCamera.transform.position;

		// Limitamos la posici�n de la c�mara dentro de los l�mites definidos en el eje X e Y
		float clampedX = Mathf.Clamp(posicionCamara.x, limiteVisible.xMin + minimapaCamera.orthographicSize * minimapaCamera.aspect, limiteVisible.xMax - minimapaCamera.orthographicSize * minimapaCamera.aspect);
		float clampedY = Mathf.Clamp(posicionCamara.y, limiteVisible.yMin + minimapaCamera.orthographicSize, limiteVisible.yMax - minimapaCamera.orthographicSize);

		// Aplicamos la posici�n ajustada
		minimapaCamera.transform.position = new Vector3(clampedX, clampedY, posicionCamara.z);
	}
}
