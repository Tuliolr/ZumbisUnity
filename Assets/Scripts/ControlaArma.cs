using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour {
    public GameObject Bala;
    public GameObject CanoArma;
    public AudioClip SomDoTiro;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(Bala, CanoArma.transform.position, CanoArma.transform.rotation);
            ControlaAudio.instancia.PlayOneShot(SomDoTiro);
        }
	}
}
