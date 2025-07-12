using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoTopDown : MonoBehaviour
{
	[Header("Movimiento")]
	[SerializeField] private GameObject jugador;
	[SerializeField] private float velocidadCaminar;
	[SerializeField] private float velocidadCorrer;
	[SerializeField] private float velocidad;
	[SerializeField] private Vector2 direccion;
	private Rigidbody2D rbd2D;

	[Header("Animacion")]
	private float movimientoX;
	private float movimientoY;
	private Animator animator;

	private void Start()
	{
		rbd2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		// En caso de que a la escena principal vuelva despues de haber entrado a otra actualizamos su posicion
		if (PlayerPrefs.HasKey("EntrandoEscena"))
		{
			float x = -1.47f;
			float y = 1.83f;

			switch(PlayerPrefs.GetFloat("EntrandoEscena"))
			{
				case 1:
					// Posicion a las puertas de la Cabaña de la Serenidad (Menu 1)
					x = 1f;
					y = -20.5f;
				break;
				case 2:
					// Posicion a las puertas de la Biblioteca de la Asertividad (Menu 2)
					x = 26.5f;
					y = 14.5f;
				break;
				case 3:
					// Posicion a las puertas de la Casa de la Resiliencia (Menu 3)
					x = 33f;
					y = -11.2f;
				break;
				case 4:
					// Posicion a las puertas de la Tienda (Menu 4)
					x = -36.45f;
					y = -3.16f;
				break;
			}
			rbd2D.position = new Vector2(x, y);
			PlayerPrefs.DeleteKey("EntrandoEscena");
		}
		
		if(PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))	
		{	
			// Posicion igual a la que tenía antes, para cuando simplemente se recargue la escena
			float x = PlayerPrefs.GetFloat("PlayerX");
			float y = PlayerPrefs.GetFloat("PlayerY");

			rbd2D.position = new Vector2(x, y);
			PlayerPrefs.DeleteKey("PlayerX");
			PlayerPrefs.DeleteKey("PlayerY");
		}
	}

	private void Update()
	{
		movimientoX = Input.GetAxisRaw("Horizontal");
		movimientoY = Input.GetAxisRaw("Vertical");

		// Comprueba si la tecla Shift está presionada
		bool isRunning = Input.GetKey(KeyCode.LeftShift);

		velocidad = isRunning ? velocidadCorrer : velocidadCaminar;

		animator.SetFloat("MovimientoX", movimientoX);
		animator.SetFloat("MovimientoY", movimientoY);
		animator.SetBool("isRunning", isRunning);

		if (movimientoX != 0 || movimientoY != 0){
			animator.SetFloat("UltimoX", movimientoX);
			animator.SetFloat("UltimoY", movimientoY);
		}

		direccion = new Vector2(movimientoX, movimientoY).normalized;

		//Debug.Log(rbd2D.position);
	}

	private void FixedUpdate()
	{
		rbd2D.MovePosition(rbd2D.position + direccion * velocidad * Time.fixedDeltaTime);
	}

	public void GuardarPosicionJugador()
	{
		PlayerPrefs.SetFloat("PlayerX", jugador.GetComponent<Transform>().position.x);
		PlayerPrefs.SetFloat("PlayerY", jugador.GetComponent<Transform>().position.y);
	}
}
