using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class iniciar : MonoBehaviour
{
    public void Iniciar()
    {
        SceneManager.LoadScene("sala");
    }
}
