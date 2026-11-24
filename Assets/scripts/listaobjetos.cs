using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listaobjetos : MonoBehaviour
{
    public GameObject lista;
    bool alternar = false;
    public void AbrirLista()
    {
        alternar = !alternar;
        lista.SetActive(alternar);
    }
}
