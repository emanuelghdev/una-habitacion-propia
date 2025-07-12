using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Objeto
{
	public string idObjeto;
	public string nombreIconoObjeto;
	public int cantidadObjeto;

	[System.NonSerialized]
	public Sprite iconoObjeto;

	public Objeto(string id, string nombreIcono, int cantidad = 0)
	{
		idObjeto = id;
		nombreIconoObjeto = nombreIcono;
		cantidadObjeto = cantidad;
		iconoObjeto = Resources.Load<Sprite>(nombreIcono);
	}

	// Constructor de copia profunda
	public Objeto(Objeto otro)
	{
		idObjeto = otro.idObjeto;
		nombreIconoObjeto = otro.nombreIconoObjeto;
		cantidadObjeto = otro.cantidadObjeto;
		iconoObjeto = Resources.Load<Sprite>(otro.nombreIconoObjeto);
	}
}
