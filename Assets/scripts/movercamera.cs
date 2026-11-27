using UnityEngine;

public class movercamera : MonoBehaviour
{
    private const int QuantidadePosicoes = 4;

    private enum DirecaoCiclo
    {
        Avancar,
        Retroceder
    }

    [SerializeField] private Vector3[] posicoes = new Vector3[QuantidadePosicoes];
    [SerializeField] private Vector3[] rotacoes = new Vector3[QuantidadePosicoes];
    [SerializeField] private DirecaoCiclo direcao = DirecaoCiclo.Avancar;

    private int indiceAtual = -1;

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if (indiceAtual < 0)
            {
                indiceAtual = direcao == DirecaoCiclo.Avancar ? 0 : posicoes.Length - 1;
            }
            else
            {
                int passo = direcao == DirecaoCiclo.Avancar ? 1 : -1;
                indiceAtual = (indiceAtual + passo + posicoes.Length) % posicoes.Length;
            }

            transform.position = posicoes[indiceAtual];
            transform.rotation = Quaternion.Euler(rotacoes[indiceAtual]);
        }
    }

    private void OnValidate()
    {
        if (posicoes == null || posicoes.Length != QuantidadePosicoes)
        {
            System.Array.Resize(ref posicoes, QuantidadePosicoes);
        }

        if (rotacoes == null || rotacoes.Length != QuantidadePosicoes)
        {
            System.Array.Resize(ref rotacoes, QuantidadePosicoes);
        }
    }
}
