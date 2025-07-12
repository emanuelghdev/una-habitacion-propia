using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapaConfiner : MonoBehaviour
{
	public Camera minimapaCamera;
	public Rect limiteVisible;			// Define los límites en el espacio del mundo

	private void Update()
	{
		AjustarLimiteCamara();
	}

	void AjustarLimiteCamara()
	{
		// Obtenemos la posición actual de la cámara
		Vector3 posicionCamara = minimapaCamera.transform.position;

		// Limitamos la posición de la cámara dentro de los límites definidos en el eje X e Y
		float clampedX = Mathf.Clamp(posicionCamara.x, limiteVisible.xMin + minimapaCamera.orthographicSize * minimapaCamera.aspect, limiteVisible.xMax - minimapaCamera.orthographicSize * minimapaCamera.aspect);
		float clampedY = Mathf.Clamp(posicionCamara.y, limiteVisible.yMin + minimapaCamera.orthographicSize, limiteVisible.yMax - minimapaCamera.orthographicSize);

		// Aplicamos la posición ajustada
		minimapaCamera.transform.position = new Vector3(clampedX, clampedY, posicionCamara.z);
	}
}
