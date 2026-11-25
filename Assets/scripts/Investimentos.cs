using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Investimentos : MonoBehaviour
{
    public int quantidadeMaxInvestimentos = 30;

    public int investimentoInfraestrutura = 0;
    public int investimentoEducacao = 0;
    public int investimentoTecnologia = 0;

    public int investimentoInfraestruturaFinal = 0;
    public int investimentoEducacaoFinal = 0;
    public int investimentoTecnologiaFinal = 0;

    public TMP_Text quantidadeMaxInvestimentosText;
    public TMP_Text investimentoInfraestruturaText;
    public TMP_Text investimentoEducacaoText;
    public TMP_Text investimentoTecnologiaText;
    public bool investindo = true;


    // Start is called before the first frame update
    
    public void InvestirInfraestrutura()
    {
        if(investindo)
        {
            if(quantidadeMaxInvestimentos > 0)
            {
                investimentoInfraestrutura++;
                quantidadeMaxInvestimentos--;
            }
        }
        
    }
    public void InvestirInfraestrutura2()
    {
        if(investindo)
        {
            if(quantidadeMaxInvestimentos > 0 && investimentoInfraestrutura > 0)
            {
                investimentoInfraestrutura--;
                quantidadeMaxInvestimentos++;
            }
        }
        
        
        
    }

    public void InvestirEducacao()
    {
        if(investindo)
        {
            
            if(quantidadeMaxInvestimentos > 0)
            {
                investimentoEducacao++;
                quantidadeMaxInvestimentos--;
            }
        }
    }
    public void InvestirEducacao2()
    {
        if (investindo)
        {
            if(quantidadeMaxInvestimentos > 0 && investimentoEducacao > 0)
            {
                investimentoEducacao--;
                quantidadeMaxInvestimentos++;
            }
        }
        
    }


    public void InvestirTecnologia()
    {
        if(investindo)
        {
            if(quantidadeMaxInvestimentos > 0)
            {
                investimentoTecnologia++;
                quantidadeMaxInvestimentos--;
            }
        }
    }
    public void InvestirTecnologia2()
    {
        if(investindo)
        {
            if(quantidadeMaxInvestimentos > 0 && investimentoTecnologia > 0)
            {
                investimentoTecnologia--;
                quantidadeMaxInvestimentos++;
            }
        }
    }

    

    // Update is called once per frame
    public void Confirmar()
    {
        investimentoInfraestruturaFinal = investimentoInfraestrutura;
        investimentoEducacaoFinal = investimentoEducacao;
        investimentoTecnologiaFinal = investimentoTecnologia;
        
        investindo = false;
    }
    public void Update()
    {
        quantidadeMaxInvestimentosText.text = "orçamento: " + quantidadeMaxInvestimentos;
        investimentoInfraestruturaText.text =investimentoInfraestrutura + " ";
        investimentoEducacaoText.text = investimentoEducacao + " ";
        investimentoTecnologiaText.text = investimentoTecnologia + " ";
    }

    
}
