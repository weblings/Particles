using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemSetup : MonoBehaviour{

	// Use this for initialization without a subEmitter
	public void setupParticleSystem(ParticleSystem ps, SystemScrambler ss) {
		setupCollision (ps, ss);
		setupColorBySpeed (ps, ss);
		setupColorOverLifetime (ps, ss);
		setupEmission (ps, ss);
		setupTrails (ps, ss);
		setupSizeBySpeed (ps, ss);
		setupShape (ps, ss);
		setupRotationOverLifetime (ps, ss);
		setupNoise (ps, ss);
	}

	//With a subEmitter
	public void setupParticleSystem(ParticleSystem ps, SystemScrambler ss, ParticleSystem ps2, SystemScrambler ss2) {
		setupCollision (ps,ss);
		setupColorBySpeed (ps, ss);
		setupColorOverLifetime (ps, ss);
		setupEmission (ps, ss);
		setupTrails (ps, ss);
		setupSizeBySpeed (ps, ss);
		setupShape (ps, ss);
		setupRotationOverLifetime (ps, ss);
		setupNoise (ps, ss);
		setupSubEmitter (ps, ss, ps2, ss2);
	}

	//ParticleSystem setup functions--------------------------------------------------------------------------------------------------------

	void setupCollision(ParticleSystem ps, SystemScrambler ss){
		ParticleSystem.CollisionModule col = ps.collision;
		col.enabled = ss.Enabled [0];
		if (!ss.Enabled [0]) return;
		col.bounce = ss.Floats [0];
		col.bounceMultiplier = ss.Floats [1];
		col.dampen = ss.Floats [2];
		col.dampenMultiplier = ss.Floats [3];
		col.lifetimeLoss = ss.Floats [4];
		col.lifetimeLossMultiplier = ss.Floats [5];
		col.maxKillSpeed = ss.Floats [6];
		col.minKillSpeed = ss.Floats [7];
	}

	void setupColorBySpeed(ParticleSystem ps, SystemScrambler ss){
		ParticleSystem.ColorBySpeedModule cbs = ps.colorBySpeed;
		cbs.enabled = ss.Enabled [1];
		if (!ss.Enabled [1]) return;
		cbs.color = makeGradient(ss, 8);
	}

	void setupColorOverLifetime(ParticleSystem ps, SystemScrambler ss){
		ParticleSystem.ColorOverLifetimeModule col = ps.colorOverLifetime;
		col.enabled = ss.Enabled [2];
		if (!ss.Enabled [2]) return;
		col.color = makeGradient(ss, 20);
	}

	void setupEmission(ParticleSystem ps, SystemScrambler ss){
		ParticleSystem.EmissionModule em = ps.emission;
		if (!ss.Enabled [3]) return; //Need emission always enabled
		em.rateOverTime = ss.Floats[32];

		//Bursts
		ParticleSystem.Burst[] bArray = new ParticleSystem.Burst[ss.Bursts [0]];
		for (int i = 0; i < ss.Bursts [0]; i++) {
			short v1 = Convert.ToInt16( ss.Bursts [(i * 3) + 2]);
			short v2 = Convert.ToInt16(ss.Bursts [(i * 3) + 3]);
			ParticleSystem.Burst b = new ParticleSystem.Burst (
				ss.Bursts [(i*3) + 1], v1, v2);
			bArray [i] = b;
		}
		em.SetBursts (bArray);
	}

	void setupTrails(ParticleSystem ps, SystemScrambler ss){
		ParticleSystem.TrailModule t = ps.trails;

		//Booleans
		t.enabled = ss.Enabled [4];
		if (!ss.Enabled [4]) return;
		t.dieWithParticles = ss.Bools [5];
		t.inheritParticleColor = ss.Bools [6];

		//Floats
		if (!ss.Bools [6]) {
			t.colorOverLifetime = makeGradient (ss, 33);
			t.colorOverTrail = makeGradient (ss, 45);
		}
		t.lifetime = ss.Floats [57];
		t.lifetimeMultiplier = ss.Floats [58];
		t.minVertexDistance = ss.Floats [59];
		t.ratio = ss.Floats [60];
		t.widthOverTrail = ss.Floats [61];
		t.widthOverTrailMultiplier = ss.Floats [62];
	}

	void setupSizeBySpeed(ParticleSystem ps, SystemScrambler ss){
		ParticleSystem.SizeBySpeedModule sbs = ps.sizeBySpeed;

		//Booleans
		sbs.enabled = ss.Enabled [4];
		if (!ss.Enabled [4]) return;
		sbs.separateAxes = ss.Bools [2];

		//Floats
		sbs.range = new Vector2(ss.Floats[63],ss.Floats[63]+ss.Floats[64]);
		sbs.size = ss.Floats [65];
		sbs.sizeMultiplier = ss.Floats [66];

		if (ss.Bools [2]) {
			sbs.x = ss.Floats [67];
			sbs.xMultiplier = ss.Floats[68];
			sbs.y = ss.Floats [69];
			sbs.yMultiplier = ss.Floats[70];
			sbs.z = ss.Floats [71];
			sbs.zMultiplier = ss.Floats[72];
		}
	}

	void setupShape(ParticleSystem ps, SystemScrambler ss){
		ParticleSystem.ShapeModule shape = ps.shape;
		int num = ss.shape;//(int) ss.Floats [73] * 10;
		if (num == 0) {
			shape.shapeType = ParticleSystemShapeType.Sphere;
		} else if (num == 1) {
			shape.shapeType = ParticleSystemShapeType.Hemisphere;
		} else if (num == 2) {
			shape.shapeType = ParticleSystemShapeType.Cone;
		} else if (num == 3) {
			shape.shapeType = ParticleSystemShapeType.Box;
		} else if (num == 4) {
			shape.shapeType = ParticleSystemShapeType.ConeVolume;
		} else if (num == 5) {
			shape.shapeType = ParticleSystemShapeType.Circle;
		} else if (num == 6) {
			shape.shapeType = ParticleSystemShapeType.SingleSidedEdge;
		} else if (num == 7) {
			shape.shapeType = ParticleSystemShapeType.BoxShell;
		} else if (num == 8) {
			shape.shapeType = ParticleSystemShapeType.BoxEdge;
		}/* else if (num == 11) {
			shape.shapeType = ParticleSystemShapeType.Donut;
		} //Doesn't appear to be in the version of Unity I'm currently in*/

		shape.randomDirectionAmount = ss.Floats [73];
		shape.sphericalDirectionAmount = ss.Floats [74];
	}

	void setupRotationOverLifetime(ParticleSystem ps, SystemScrambler ss){
		ParticleSystem.RotationOverLifetimeModule rol = ps.rotationOverLifetime;

		rol.enabled = ss.Enabled [5];
		if (!ss.Enabled [5]) return;
		rol.separateAxes = ss.Bools [3];

		if (rol.separateAxes) {
			rol.x = ss.Floats [75];
			rol.xMultiplier = ss.Floats [76];
			rol.y = ss.Floats [77];
			rol.yMultiplier = ss.Floats [78];
			rol.z = ss.Floats [79];
			rol.zMultiplier = ss.Floats [80];
		}

	}

	void setupNoise(ParticleSystem ps, SystemScrambler ss){
		ParticleSystem.NoiseModule n = ps.noise;

		n.enabled = ss.Enabled [6];
		if (!ss.Enabled [6]) return;
		n.separateAxes = ss.Bools [4];

		n.frequency = ss.Floats [81];

		if (!n.separateAxes) {
			n.strength = ss.Floats [82];
		} else {
			n.strengthX = ss.Floats [82];
			n.strengthXMultiplier = ss.Floats [83];
			n.strengthY = ss.Floats [84];
			n.strengthYMultiplier = ss.Floats [85];
			n.strengthZ = ss.Floats [86];
			n.strengthZMultiplier = ss.Floats [87];
		}
	}

	void setupSubEmitter(ParticleSystem ps, SystemScrambler ss, ParticleSystem ps2, SystemScrambler ss2){
		ParticleSystem.SubEmittersModule se = ps.subEmitters;

		se.enabled = ss.Enabled [7];
		if (!ss.Enabled [7]) return;

		setupParticleSystem (ps2,ss2);
		se.AddSubEmitter (ps2, ParticleSystemSubEmitterType.Birth, ParticleSystemSubEmitterProperties.InheritNothing);
	}

	//Helper functions-----------------------------------------------------------------------------------------------------------------------

	//Returns a gradient pulling data from the Floats array from a starting index
	Gradient makeGradient(SystemScrambler ss, int index){
		Gradient grad = new Gradient ();
		Color c1 = new Color (ss.Floats [index], ss.Floats [index + 1], ss.Floats [index + 2]);
		Color c2 = new Color (ss.Floats [index + 3], ss.Floats [index + 4], ss.Floats [index + 5]);
		grad.SetKeys( new GradientColorKey[] {
			new GradientColorKey(c1, ss.Floats[index + 6]), new GradientColorKey(c2, ss.Floats[index + 7])},
			new GradientAlphaKey[] { new GradientAlphaKey(ss.Floats[index + 8], ss.Floats[index + 9]),
				new GradientAlphaKey(ss.Floats[index + 10], ss.Floats[index + 11])}
		);
		return grad;
	}
}
