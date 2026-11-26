using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class criarobjeto : MonoBehaviour
{
    public GameObject objetoPrefab;
    public Vector3 pos = new Vector3(0, 0.5f, 0);
    public Vector3 rot = new Vector3(-90, 0, 0);
    public Investimentos investimentos;
//1
    public void Criarpuff()
    {
        if(investimentos.investimentoInfraestruturaFinal >= 10)
        {
            
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
//2
    public void Criarcadeira()
    {
        if(investimentos.investimentoInfraestruturaFinal >= 5)
        {
            rot.x = 0;
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
            rot.x = -90;
        }
        
    }
//3
    public void Criarmesa()
    {
        if(investimentos.investimentoInfraestruturaFinal >= 5 && investimentos.investimentoEducacaoFinal >= 5)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
//4
    public void Criarventilador()
    {
        if(investimentos.investimentoInfraestruturaFinal >= 5 && investimentos.investimentoTecnologiaFinal >=5)
        {
            pos.y = 2;
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
            pos.y = 0;
        }
       
    }
//5
    public void Criararcondi()
    {
        if(investimentos.investimentoInfraestruturaFinal >= 10 && investimentos.investimentoTecnologiaFinal >= 10)
        {
            pos.y = 1.5f;
            rot.x = 0;
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
            rot.x = -90;
            pos.y = 0;
        }
        
    }
//6
    public void Criartablet()
    {
        if(investimentos.investimentoTecnologiaFinal >= 10)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
//7
    public void Criarlivro()
    {
        if(investimentos.investimentoEducacaoFinal >= 5)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
//8
    public void Criarquadro()
    {
        if(investimentos.investimentoInfraestruturaFinal >= 10 && investimentos.investimentoEducacaoFinal >= 10)
        {
            pos.y = 1.5f;
            rot.x = 0;
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
            rot.x = -90;
            pos.y = 0;
        }
        
    }
//9
    public void Criartelevisão()
    {
        if(investimentos.investimentoInfraestruturaFinal >= 10 && investimentos.investimentoTecnologiaFinal >= 15)
        {
            pos.y = 1.5f;
            rot.x = 0;
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
            rot.x = -90;
            pos.y = 0;
        }
        
    }
//10
    public void Criarmesafut()
    {
        if(investimentos.investimentoInfraestruturaFinal >= 10 && investimentos.investimentoEducacaoFinal >= 10)
        {
            Instantiate(objetoPrefab, pos, Quaternion.Euler(rot));
        }
        
    }
}
