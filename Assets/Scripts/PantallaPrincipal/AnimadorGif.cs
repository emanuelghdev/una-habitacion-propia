using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimadorGif : MonoBehaviour
{
	public Sprite[] gifFrames;				// Arreglo de sprites que componen el GIF
	public float frameRate = 0.095f;		// Tiempo entre fotogramas

	private Image imageComponent;
	private int currentFrame;
	private float timer;

	void Start()
	{
		imageComponent = GetComponent<Image>();
		if (gifFrames.Length > 0)
		{
			imageComponent.sprite = gifFrames[0];
		}
	}

	void Update()
	{
		if (gifFrames.Length == 0) return;

		timer += Time.deltaTime;
		if (timer >= frameRate)
		{
			currentFrame = (currentFrame + 1) % gifFrames.Length;
			imageComponent.sprite = gifFrames[currentFrame];
			timer = 0f;
		}
	}
}
