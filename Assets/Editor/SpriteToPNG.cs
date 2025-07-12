using UnityEngine;
using UnityEditor;
using System.IO;

public class SpriteToPNG : MonoBehaviour
{
	[MenuItem("Tools/Save Sprite as PNG")]
	private static void SaveSpriteAsPNG()
	{
		// Obtén el sprite seleccionado en el editor
		Sprite sprite = Selection.activeObject as Sprite;

		if (sprite == null)
		{
			Debug.LogError("No se ha seleccionado un Sprite.");
			return;
		}

		// Convierte el sprite a una textura
		Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
		Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
												  (int)sprite.textureRect.y,
												  (int)sprite.textureRect.width,
												  (int)sprite.textureRect.height);
		texture.SetPixels(pixels);
		texture.Apply();

		// Selecciona la ruta para guardar el archivo
		string path = EditorUtility.SaveFilePanel("Guardar Sprite como PNG", "", sprite.name + ".png", "png");

		if (string.IsNullOrEmpty(path))
			return;

		// Guarda la textura como PNG
		byte[] bytes = texture.EncodeToPNG();
		File.WriteAllBytes(path, bytes);

		Debug.Log("Sprite guardado como PNG en: " + path);
	}
}
