using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComprarTienda : MonoBehaviour
{
	[Header("IU")]
	public ControladorTienda controladorTienda;
	public TMP_Text numObjetosInventario;
	public Button boton;

	[Header("Objeto")]
	public Objeto objeto;
	public string nombreTienda;
	public int precio;
	public int cantidad;

	void Start()
	{
		boton.onClick.AddListener(ComprarObjeto);
	}

	public void ComprarObjeto()
	{
		controladorTienda.Comprar(objeto, nombreTienda, precio, cantidad, numObjetosInventario);
	}
}
