using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investimentos : MonoBehaviour
{
    public int quantidadeMaxInvestimentos = 30;

    public int investimentoInfraestrutura = 0;
    public int investimentoEducacao = 0;
    public int investimentoTecnologia = 0;

    public int investimentoInfraestruturaFinal = 0;
    public int investimentoEducacaoFinal = 0;
    public int investimentoTecnologiaFinal = 0;


    // Start is called before the first frame update
    void InvestirInfraestrutura()
    {
        if(quantidadeMaxInvestimentos > 0)
        {
            investimentoInfraestrutura++;
            quantidadeMaxInvestimentos--;
        }
    }

    void InvestirEducacao()
    {
        if(quantidadeMaxInvestimentos > 0)
        {
            investimentoEducacao++;
            quantidadeMaxInvestimentos--;
        }
    }

    

    void InvestirTecnologia()
    {
        if(quantidadeMaxInvestimentos > 0)
        {
            investimentoTecnologia++;
            quantidadeMaxInvestimentos--;
        }
    }

    

    // Update is called once per frame
    void Confirmar()
    {
        investimentoInfraestruturaFinal = investimentoInfraestrutura;
        investimentoEducacaoFinal = investimentoEducacao;
        investimentoTecnologiaFinal = investimentoTecnologia;
    }

    
}
