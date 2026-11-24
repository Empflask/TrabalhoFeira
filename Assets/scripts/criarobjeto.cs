using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class criarobjeto : MonoBehaviour
{
    public GameObject objetoPrefab;
    public Vector3 pos = new Vector3(0, 0.5f, 0);
    public Vector3 rot = new Vector3(-90, 0, 0);
    public investimentos investimentos;
//1
    public void Criarpuff()
    {
        if(investimentos.investimentoInfraestruturaFinal > 10)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
//2
    public void Criarcadeira()
    {
        if(investimentos.investimentoInfraestruturaFinal > 5)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
//3
    public void Criarmesa()
    {
        if(investimentos.investimentoInfraestruturaFinal > 5)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
//4
    public void Criarventilador()
    {
        if(investimentos.investimentoInfraestruturaFinal > 5)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
       
    }
//5
    public void Criararcondi()
    {
        if(investimentos.investimentoInfraestruturaFinal > 10 && investimentos.investimentoTecnologiaFinal > 10)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
//6
    public void Criartablet()
    {
        if(investimentos.investimentoInfraestruturaFinal > 0)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
//7
    public void Criarlivro()
    {
        if(investimentos.investimentoInfraestruturaFinal > 0)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
//8
    public void Criarquadro()
    {
        if(investimentos.investimentoInfraestruturaFinal > 0)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
//9
    public void Criartelevisão()
    {
        if(investimentos.investimentoInfraestruturaFinal > 0)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
//10
    public void Criarmesafut()
    {
        if(investimentos.investimentoInfraestruturaFinal > 0)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
}
