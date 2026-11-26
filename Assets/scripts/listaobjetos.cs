using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listaobjetos : MonoBehaviour
{
    public GameObject status;
    public GameObject listaedu;
    public GameObject listatec;
    public GameObject listainfra;
    public GameObject listaobj;
    bool alternar = false;
    bool alternar2 = false;
    public void AbrirStatus()
    {
        alternar = !alternar;
        status.SetActive(alternar);
        
    }
    public void AbrirListaEdu()
    {
        alternar2 = !alternar2;
        listaedu.SetActive(alternar2);
        listaobj.SetActive(!alternar2);
    }
    public void AbrirListaTec()
    {
        alternar2 = !alternar2;
        listatec.SetActive(alternar2);
        listaobj.SetActive(!alternar2);
    }
    public void AbrirListaInfra()
    {
        alternar2 = !alternar2;
        listainfra.SetActive(alternar2);
        listaobj.SetActive(!alternar2);
    }

}
