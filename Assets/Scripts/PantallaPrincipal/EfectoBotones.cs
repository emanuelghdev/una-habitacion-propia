using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EfectoBotones : MonoBehaviour
{
	// Escalas para agrandar y empequeñecer
	public Vector3 enlargedScale = new Vector3(1.2f, 1.2f, 1.2f);
	public Vector3 normalScale = Vector3.one;
	public float tweenDuration = 2f;

	// Efectos de sonido
	public AudioClip hoverClip;
	public AudioClip clickClip;
	private AudioSource audioSource;

	private Button button;

	private void Awake()
	{
		// Nos aseguramos de tener un AudioSource
		audioSource = GetComponent<AudioSource>();
	}


	void Start()
	{
		button = GetComponent<Button>();
	}

	public void OnPointerEnter()
	{
		if (hoverClip != null)
			audioSource.PlayOneShot(hoverClip);

		LeanTween.scale(gameObject, enlargedScale, tweenDuration).setEase(LeanTweenType.easeOutQuad);
	}

	public void OnPointerExit()
	{
		LeanTween.scale(gameObject, normalScale, tweenDuration).setEase(LeanTweenType.easeInQuad);
	}

	public void OnClick()
	{
		// Comprobamos si el botón está interactuable (no deshabilitado)
		if (button != null && button.interactable)
		{
			if (clickClip != null)
				audioSource.PlayOneShot(clickClip);

			LeanTween.scale(gameObject, enlargedScale * 1.1f, tweenDuration / 2).setEase(LeanTweenType.easeOutQuad)
				 .setOnComplete(() => LeanTween.scale(gameObject, normalScale, tweenDuration / 2).setEase(LeanTweenType.easeInQuad));
		}
	}
}