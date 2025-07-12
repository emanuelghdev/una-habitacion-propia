using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesUI : MonoBehaviour
{
	public GameObject logo;
	public GameObject inicioGrupo;
	private static bool recienIniciado = false;

	void Start()
    {
		// Ejecutamos esto solo si acabamos de ejecutar el juego
		if (!recienIniciado)
		{
			recienIniciado = true;

			inicioGrupo.SetActive(true);
			LeanTween.moveX(logo.GetComponent<RectTransform>(), 0, 4f).setDelay(3f)
					 .setEase(LeanTweenType.easeOutCirc).setOnComplete(BajarAlpha);
		}
    }

	public void BajarAlpha()
	{
		LeanTween.alpha(inicioGrupo.GetComponent<RectTransform>(), 0f, 1f).setDelay(0.5f).setOnComplete(QuitarBlockRaycats);
	}

	public void QuitarBlockRaycats()
	{
		inicioGrupo.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
}
