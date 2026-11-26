using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Feedb : MonoBehaviour
{
    public GameObject feedbackImage;
    public GameObject Puff;
    public GameObject Cadeira;
    public GameObject Mesa;
    public GameObject Ventilador;
    public GameObject ArCondicionado;
    public GameObject Tablet;
    public GameObject Livro;
    public GameObject Quadro;
    public GameObject Televisao;
    public GameObject Mesafut;

     public TMP_Text PuffText;
    public TMP_Text CadeiraText;
    public TMP_Text MesaText;
    public TMP_Text VentiladorText;
    public TMP_Text ArCondicionadoText;
    public TMP_Text TabletText;
    public TMP_Text LivroText;
    public TMP_Text QuadroText;
    public TMP_Text TelevisaoText;
    public TMP_Text MesafutText;

    public Armazenarboolobjeto armazenarboolobjeto;

    void Start()
    {
        PuffText.text = "A presença de pufes na sala de aula pode proporcionar um ambiente mais confortável.";
        CadeiraText.text = "Cadeiras confortáveis e ergonômicas são essenciais para o bem-estar dos alunos.";
        MesaText.text = "Mesas adequadas e espaçosas contribuem para a organização e produtividade.";
        VentiladorText.text = "Ventiladores ajudam a manter a sala de aula fresca e confortável.";
        ArCondicionadoText.text = "O ar-condicionado proporciona um ambiente agradável, especialmente em dias quentes.";
        TabletText.text = "O uso de tablets pode enriquecer o aprendizado e facilitar o acesso a recursos educacionais.";
        LivroText.text = "Livros são fundamentais para o desenvolvimento do conhecimento e da imaginação."; 
        QuadroText.text = "Quadros são ferramentas importantes para a comunicação e a apresentação de informações.";
        TelevisaoText.text = "A televisão pode ser utilizada como recurso de ensino para mostrar conteúdos visuais.";
        MesafutText.text = "Mesas futuristas podem adicionar um toque moderno e inspirador ao ambiente de aprendizado.";
    }

    
    public void Feedback()
    {
        feedbackImage.SetActive(true);
        
        if(armazenarboolobjeto.puffCriado)
        {
            feedbackImage.SetActive(true);
            Puff.SetActive(true);
        }
        if(armazenarboolobjeto.cadeiraCriado)
        {
            feedbackImage.SetActive(true);
            Cadeira.SetActive(true);
        }
        if(armazenarboolobjeto.mesaCriado)
        {
            feedbackImage.SetActive(true);
            Mesa.SetActive(true);
        }
        if(armazenarboolobjeto.ventiladorCriado)
        {
            feedbackImage.SetActive(true);
            Ventilador.SetActive(true);
        }
        if(armazenarboolobjeto.arcondicionadoCriado)
        {
            feedbackImage.SetActive(true);
            ArCondicionado.SetActive(true);
        }
        if(armazenarboolobjeto.tabletCriado)
        {
            feedbackImage.SetActive(true);
            Tablet.SetActive(true);
        }
        if(armazenarboolobjeto.livroCriado)
        {
            feedbackImage.SetActive(true);
            Livro.SetActive(true);
        }
        if(armazenarboolobjeto.quadroCriado)
        {
            feedbackImage.SetActive(true);
            Quadro.SetActive(true);
        }
        if(armazenarboolobjeto.televisaoCriado)
        {
            feedbackImage.SetActive(true);
            Televisao.SetActive(true);
        }
        if(armazenarboolobjeto.mesafutCriado)
        {
            feedbackImage.SetActive(true);
            Mesafut.SetActive(true);
        }
    }
}
