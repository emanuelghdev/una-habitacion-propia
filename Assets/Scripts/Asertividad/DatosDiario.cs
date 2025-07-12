using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntradaDiario
{
	public string id;
	public string fecha;
	public string emocion;      // Alegre (0), Contento (1), Bien (2), Regular (3), Triste (4), Desolado (5)
	public string texto;
}