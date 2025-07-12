using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class ControladorDiario : MonoBehaviour
{
	private string rutaDirectorio;

	void Awake()
    {
		rutaDirectorio = Application.persistentDataPath + "/EntradasDiario/";
		//Debug.Log(rutaDirectorio);
		
		// Si no existe ya el directorio lo creamos
		if (!Directory.Exists(rutaDirectorio))
		{
			Directory.CreateDirectory(rutaDirectorio);
		}
	}

	public void GuardarEntradaDiario(EntradaDiario entrada)
	{
		string json = JsonUtility.ToJson(entrada);
		string rutaArchivo = Path.Combine(rutaDirectorio, entrada.id + ".json");
		File.WriteAllText(rutaArchivo, json);
	}

	public EntradaDiario CargarEntradaDiario(string id)
	{
		string rutaArchivo = Path.Combine(rutaDirectorio, id + ".json");

		// Comprobamos que exista el archivo
		if (File.Exists(rutaArchivo))
		{
			string json = File.ReadAllText(rutaArchivo);
			return JsonUtility.FromJson<EntradaDiario>(json);
		}

		return null;
	}

	public void EliminarEntrada(string id)
	{
		string rutaArchvio = Path.Combine(rutaDirectorio, id + ".json");

		// Comprobamos que la ruta del archivo existe
		if (File.Exists(rutaArchvio))
		{
			File.Delete(rutaArchvio);
		}
	}

	public List<EntradaDiario> CargarDiario()
	{
		List<EntradaDiario> entradas = new List<EntradaDiario>();

		foreach (string rutaArchivo in Directory.GetFiles(rutaDirectorio, "*.json"))
		{
			string json = File.ReadAllText(rutaArchivo);
			EntradaDiario entrada = JsonUtility.FromJson<EntradaDiario>(json);
			entradas.Add(entrada);
		}

		return entradas;
	}
}
