using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{

    
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    public ControlaInterface scriptcontrolaInterface;
    public AudioClip SomDeDano;
    private MovimentaJogador meuMovimentoJogador;
    private AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;

    // Use this for initialization
   private void Start()
    {
        
        meuMovimentoJogador = GetComponent<MovimentaJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {

        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoJogador.Movimentar(direcao.magnitude);



       
    }

    void FixedUpdate()
    {
        meuMovimentoJogador.Movimentar(direcao, statusJogador.Velocidade);

        meuMovimentoJogador.RotacaoJogador(MascaraChao);
    }



    public void TomarDano(int dano)
    {
        statusJogador.Vida -= dano;
        scriptcontrolaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);
    

        if (statusJogador.Vida <= 0)
        {
            Morrer();

        }
    }
        public void Morrer()
        {
            
        scriptcontrolaInterface.GameOver();
            
        }
        
         public void CurarVida(int quantidadeDeCura)
          {
              statusJogador.Vida += quantidadeDeCura;
           if(statusJogador.Vida > statusJogador.VidaInicial)
        {
            statusJogador.Vida = statusJogador.VidaInicial;
        }
             scriptcontrolaInterface.AtualizarSliderVidaJogador();
          }

    }



