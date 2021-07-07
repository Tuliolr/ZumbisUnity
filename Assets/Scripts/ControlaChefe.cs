using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel {

    private Transform jogador;
    private NavMeshAgent agente;
    private Status statuChefe;
    private AnimacaoPersonagem animacaoChefe;
    private MovimentoPersonagem movimentoChefe;
    public GameObject KitMedicoPrefab;
    public Slider sliderVidaChefe;
    public Image ImageSlide;
    public Color CorDavidaMaxima, CorDaVidaMinima;
    public GameObject ParticulaSangueZumbi;

	// Use this for initialization
	void Start () {

        jogador = GameObject.FindWithTag("Jogador").transform;
        agente = GetComponent<NavMeshAgent>();
        statuChefe = GetComponent<Status>();
        agente.speed = statuChefe.Velocidade;
        animacaoChefe = GetComponent<AnimacaoPersonagem>();
        movimentoChefe = GetComponent<MovimentoPersonagem>();
        sliderVidaChefe.maxValue = statuChefe.VidaInicial;
        AtualizarInterface();
	}
	
	// Update is called once per frame
	void Update () {
        agente.SetDestination(jogador.position);
        animacaoChefe.Movimentar(agente.velocity.magnitude);

        if((!agente.pathPending && agente.hasPath))
        {

        
        bool estouPertoDoJogador = agente.remainingDistance <= agente.stoppingDistance;

        if(estouPertoDoJogador)
        {
            animacaoChefe.Atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                movimentoChefe.Rotacionar(direcao);
        }
        else
        {
            animacaoChefe.Atacar(false);
        }
		
	}
    }

    void AtacaJogador()
    {
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        statuChefe.Vida -= dano;
        AtualizarInterface();
        if(statuChefe.Vida <= 0)
        {
            Morrer();
        }
    }

    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(ParticulaSangueZumbi, posicao, rotacao);
    }

    public void Morrer()
    {
        animacaoChefe.Morrer();
        movimentoChefe.Morrer();
        this.enabled = false;
        agente.enabled = false;
        Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, 2);
    }

    void AtualizarInterface ()
    {
        sliderVidaChefe.value = statuChefe.Vida;
        float porcentagemDaVida = (float)statuChefe.Vida / statuChefe.VidaInicial;
        Color corDaVida = Color.Lerp(CorDaVidaMinima, CorDavidaMaxima, porcentagemDaVida);
        ImageSlide.color = corDaVida;
    }
}
