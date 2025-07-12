using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class DatosJuego
{
	public Vector3 posicion;
	public int puntosBienestar;
	public List<Objeto> objetosInventario = new List<Objeto>();
}
