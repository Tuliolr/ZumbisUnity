using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaDeGeracao = 3;
    private float DistanciaDoJogadorParaGeracao = 20;
    private GameObject jogador;
    private int quantidadeMaximaDeZumbisVivos = 3;
    private int quantidadeDeZumbisVivos;
    private float tempoProximoAumentoDeDificuldade = 5;
    private float contadorDeAumentarDificuldade;


    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        contadorDeAumentarDificuldade = tempoProximoAumentoDeDificuldade;
        for(int i =0; i < quantidadeMaximaDeZumbisVivos; i++)
        {
            StartCoroutine(GerarNovoZumbi());
        }
    }



    // Update is called once per frame
    void Update()
    {
        bool possoGerarZumbisPelaDistancia = Vector3.Distance(transform.position,
            jogador.transform.position) > DistanciaDoJogadorParaGeracao;

        if (possoGerarZumbisPelaDistancia == true && 
            quantidadeDeZumbisVivos < quantidadeMaximaDeZumbisVivos)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }

        if(Time.timeSinceLevelLoad > contadorDeAumentarDificuldade)
        {
            quantidadeMaximaDeZumbisVivos++;
            contadorDeAumentarDificuldade = Time.timeSinceLevelLoad + 
                tempoProximoAumentoDeDificuldade;
        }
    
        
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    IEnumerator GerarNovoZumbi ()
        {
        Vector3 posicaoDeCriacao = AleatorizazPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);//Para testa onde tem colisao

        while(colisores.Length > 0)
        {
             posicaoDeCriacao = AleatorizazPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }
           ControlaInimigo zumbi = Instantiate(Zumbi, posicaoDeCriacao, transform.rotation)
            .GetComponent<ControlaInimigo>(); //Para Gerar Zumbis
        zumbi.meuGerador = this;
        quantidadeDeZumbisVivos++;
            
        }
        
       Vector3 AleatorizazPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    public void DiminuirQuantidadeDeZumbisVivos()
    {
        quantidadeMaximaDeZumbisVivos--;
    }
	}

