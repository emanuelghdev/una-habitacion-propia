using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EfectoBotones : MonoBehaviour
{
	// Escalas para agrandar y empequeñecer
	public Vector3 enlargedScale = new Vector3(1.2f, 1.2f, 1.2f);
	public Vector3 normalScale = Vector3.one;
	public float tweenDuration = 2f;

	private Button button;


	void Start()
	{
		button = GetComponent<Button>();
	}

	public void OnPointerEnter()
	{
		LeanTween.scale(gameObject, enlargedScale, tweenDuration).setEase(LeanTweenType.easeOutQuad);
	}

	public void OnPointerExit()
	{
		LeanTween.scale(gameObject, normalScale, tweenDuration).setEase(LeanTweenType.easeInQuad);
	}

	public void OnClick()
	{
		// Comprueba si el botón está interactuable (no deshabilitado)
		if (button != null && button.interactable)
		{
			LeanTween.scale(gameObject, enlargedScale * 1.1f, tweenDuration / 2).setEase(LeanTweenType.easeOutQuad)
				 .setOnComplete(() => LeanTween.scale(gameObject, normalScale, tweenDuration / 2).setEase(LeanTweenType.easeInQuad));
		}
	}
}