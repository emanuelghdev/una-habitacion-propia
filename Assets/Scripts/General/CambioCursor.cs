using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioCursor : MonoBehaviour
{
	public Texture2D spriteCursor;
	public Texture2D spriteClickCursor;
	public Vector2 puntoAnclaje = Vector2.zero;
	public CursorMode modoCursor = CursorMode.Auto;

	private bool isClicking = false;

	void Start()
	{
		CambiarCursorSprite(spriteCursor);
	}

	void Update()
	{
		// Detectar cuando se hace clic con el botón izquierdo del ratón
		if (Input.GetMouseButtonDown(0))
		{
			CambiarCursorSprite(spriteClickCursor);
			isClicking = true;
		}
		else if (Input.GetMouseButtonUp(0) && isClicking)
		{
			CambiarCursorSprite(spriteCursor);
			isClicking = false;
		}
	}

	public void CambiarCursorSprite(Texture2D nuevoCursor)
	{
		Cursor.SetCursor(nuevoCursor, puntoAnclaje, modoCursor);
	}
}
