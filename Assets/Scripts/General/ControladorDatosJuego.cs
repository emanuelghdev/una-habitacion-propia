using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class ControladorDatosJuego : MonoBehaviour
{
	public GameObject jugador;
	public string archivoGuardado;
	public string rutaDirectorioDiarios;
	public DatosJuego datosJuego = new DatosJuego();
	public InventarioUI inventarioIU;

	GameManager gameManager;
	

	private void Awake()
	{
		archivoGuardado = Application.persistentDataPath + "/datosJuego.json";
		rutaDirectorioDiarios = Application.persistentDataPath + "/EntradasDiario/";

		jugador = GameObject.FindGameObjectWithTag("Player");
		gameManager = FindObjectOfType<GameManager>();
	}

	public bool CargarDatos()
	{
		if(File.Exists(archivoGuardado))
		{
			string contenido = File.ReadAllText(archivoGuardado);
			datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);

			InicializarDatos();
			return true;
		}
		else{
			return false;
		}
	}

	public void GuardarDatos()
	{
		foreach (var objeto in gameManager.objetosInventario)
		{
			objeto.nombreIconoObjeto = objeto.iconoObjeto.name; // Guardamos el nombre del Sprite
		}

		DatosJuego nuevosDatos = new DatosJuego()
		{
			posicion = jugador.transform.position,
			puntosBienestar = gameManager.PB,
			objetosInventario = gameManager.objetosInventario
		};

		string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
		File.WriteAllText(archivoGuardado, cadenaJSON);
	}

	public void EliminarDatos()
	{
		// Eliminamos el archivo de guardado
		if (File.Exists(archivoGuardado))
		{
			File.Delete(archivoGuardado);
		}

		// Eliminamos los archivos de diarios
		if(Directory.Exists(rutaDirectorioDiarios))
		{
			string[] archivos = Directory.GetFiles(rutaDirectorioDiarios, "*.json");
			foreach (string archivo in archivos)
			{
				File.Delete(archivo);
			}
		}
		
		// Reestablecemos los PlayerPrefs
		PlayerPrefs.DeleteAll();
	}

	public void InicializarDatos()
	{
		jugador.GetComponent<Transform>().position = datosJuego.posicion;
		jugador.GetComponent<MovimientoTopDown>().GuardarPosicionJugador();

		gameManager.PB = datosJuego.puntosBienestar;
		gameManager.ActualizarInterfaz();


		foreach (var objeto in datosJuego.objetosInventario)
		{
			objeto.iconoObjeto = Resources.Load<Sprite>(objeto.nombreIconoObjeto); // Cargamos el Sprite desde Resources
		}

		gameManager.objetosInventario = new List<Objeto>(datosJuego.objetosInventario.Select(o => new Objeto(o)));

		gameManager.objetosInventario = datosJuego.objetosInventario;
		inventarioIU.UpdateInventorioUI();
	}
}
