using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSetup : MonoBehaviour {

	GameObject GameController;
	ParticleSystemSetup pss;
	public ParticleSystem ps;
	public ParticleSystem subEmitterPS;
	public SystemScrambler ss;
	public SystemScrambler subEmitterSS;
	public Material m;

	// Use this for initialization
	void Start () {
		ss = new SystemScrambler ();

		//Get GameController
		GameController = GameObject.FindGameObjectWithTag ("GameController");
		pss = GameController.GetComponent<ParticleSystemSetup>();

		if (ss.Enabled [7]) {
			subEmitterSS = new SystemScrambler ();

			//Make child GameObject to store new particleSystem (Unity seems to only allow one per Gameobject)
			GameObject child = new GameObject();
			child.transform.parent = gameObject.transform;

			//Make ParticleSystem Component in child
			child.AddComponent<ParticleSystem> ();
			subEmitterPS = child.GetComponent<ParticleSystem> ();

			//Set that ParticleSystem's material and trail material
			ParticleSystemRenderer psr = child.GetComponent<ParticleSystemRenderer>();
			psr.material = m;
			psr.trailMaterial = m;

			pss.setupParticleSystem (ps, ss, subEmitterPS, subEmitterSS);
		} else {
			pss.setupParticleSystem (ps, ss);
		}
	}
	
}
