using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorTienda : MonoBehaviour
{
	public GameObject dialogoBoxComprar;
	public DialogosEvento narradorRechazo;
	public TMP_Text preguntaConfirmar;
	public Button botonConfirmar;
	public Button botonCancelar;
	public Transform contenedorElementos;
	public TMP_Text puntosUI;
	public AudioClip sonidoCompra;

	private bool compraConfirmada = false;

	GameManager gameManager;

	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();

		botonConfirmar.onClick.AddListener(OnConfirmarCompra);
		botonCancelar.onClick.AddListener(OnCancelarCompra);

		InicializarObjTienda();
		puntosUI.text = gameManager.PB.ToString();
	}

	public void InicializarObjTienda()
	{
		// Recorre todos los hijos del transform padre
		for (int i = 0; i < contenedorElementos.childCount; i++)
		{
			Transform elemento = contenedorElementos.Find("Elemento" + i);
			if (elemento != null)
			{
				Transform boton = elemento.Find("Boton");
				Objeto obj = boton.GetComponent<ComprarTienda>().objeto;
				Objeto objeto = gameManager.objetosInventario.Find(i => i.idObjeto == obj.idObjeto);

				Transform checkInventario = elemento.Find("Check Inventario");
				if (checkInventario != null)
				{
					Transform numObjeto = checkInventario.Find("NumObjeto");
					if (numObjeto != null)
					{
						// Accede al componente TMP_Text y cambia el texto
						TMP_Text texto = numObjeto.GetComponent<TMP_Text>();
						if (texto != null)
						{
							if(objeto != null)
							{
								texto.text = objeto.cantidadObjeto.ToString();
								obj.cantidadObjeto = objeto.cantidadObjeto;
							}
							else
							{
								texto.text = "0";
							}
						}
					}
				}
			}
		}
	}

	public void OnCancelarCompra()
	{
		dialogoBoxComprar.SetActive(false);
	}

	public void OnConfirmarCompra()
	{
		dialogoBoxComprar.SetActive(false);
		compraConfirmada = true;
	}

	public void Comprar(Objeto objeto, string nombreTienda, int precio, int cantidad, TMP_Text textoObjInventario)
	{
		preguntaConfirmar.text = "¿Estás seguro que quieres comprar " + nombreTienda + "?";
		dialogoBoxComprar.SetActive(true);

		if(gameManager.PB >= precio)
		{
			StartCoroutine(EsperarConfirmacion(() => compraConfirmada, objeto, precio, cantidad, textoObjInventario));
		}
		else
		{
			dialogoBoxComprar.SetActive(false);
			narradorRechazo.OnEmpezarDialogo();
		}
	}

	IEnumerator EsperarConfirmacion(System.Func<bool> condicion, Objeto objeto, int precio, int cantidad, TMP_Text textoObjInventario)
	{
		// Espera hasta que la condición sea true
		yield return new WaitUntil(condicion);
		compraConfirmada = false;

		gameManager.IniciarSonido(sonidoCompra);

		int numeroObj = objeto.cantidadObjeto + cantidad;
		textoObjInventario.text = numeroObj.ToString();
		puntosUI.text = (gameManager.PB - precio).ToString();

		gameManager.ComprarObjeto(objeto, precio, cantidad);
	}
}
