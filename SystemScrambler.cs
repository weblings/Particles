using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemScrambler{

	public List<float> Floats;

	/* __Collision__
	 * 0. bounce
	 * 1. bounceMultiplier
	 * 2. dampen
	 * 3. dampenMultiplier
	 * 4. lifetimeLoss
	 * 5. lifetimeLossMultiplier
	 * 6. maxKillSpeed
	 * 7. minKillSpeed
	 * 
	 * 8-19. colorBySpeed (Gradient)
	 * 
	 * 20-31. colorOverLifetime (Gradient)
	 * 
	 * __Emission__
	 * 32. rateOverTime
	 * 
	 * __Trails__
	 * 33-44. colorOverLifetime (Gradient)
	 * 45-56. colorOverTrail (Gradient)
	 * 57. lifetime
	 * 58. lifetimeMultiplier
	 * 59. minVertexDistance
	 * 60. ratio
	 * 61. widthOverTrail
	 * 62. widthOverTrailMultiplier
	 * 
	 * __SizeBySpeed__
	 * 63. range (min)
	 * 64. range (max)
	 * 65. size
	 * 66. sizeMultiplier
	 * 67. x
	 * 68. xMultiplier
	 * 69. y
	 * 70. yMultiplier
	 * 71. z
	 * 72. zMultiplier
	 * 
	 * __Shape__
	 * 73. Randomize Direction
	 * 74. Spherize Direction
	 * 
	 * __RotationOverLifetime__
	 * 75. x 
	 * 76. xMultiplier
	 * 77. y
	 * 78. yMultiplier 
	 * 79. z
	 * 80. zMultiplier
	 * 
	 * __Noise__
	 * 81. Frequency
	 * 82. x
	 * 83. xMultiplier
	 * 84. y
	 * 85. yMultiplier
	 * 86. z
	 * 87. zMultiplier
     */

	public List<bool> Enabled;

	/* _enabled_
	 * 0. Collision 
	 * 1. colorBySpeed
	 * 2. colorOverLifetime
	 * 3. Trails
	 * 4. SizeBySpeed
	 * 5. RotationOverLifetime
	 * 6. Noise
	 * 7. SubEmitter
	 */ 

	public List<bool> Bools;

	/* __Trails__
	* 0 dieWithParticles
	* 1. inheritParticleColor
	* 
	* __SizeBySpeed__
	* 2. separateAxes
	* 
	* __RotationOverLifetime__
	* 3. separateAxes
	* 
	* __Noise__
	* 4. separateAxes
	*/

	public List<int> Bursts;

	public int shape; //Wasn't worth hassle of trying to convert into a float and back

	//Constructor
	public SystemScrambler(){
		Floats = new List<float> ();
		Enabled = new List<bool> ();
		Bools = new List<bool> ();
		Bursts = new List<int> ();

		//Floats ----------------------------------------

		//Collision
		for (int i = 0; i < 8; i++) {
			Floats.Add (Random.Range(0f,1f));
		}
		AddGradient (); //colorBySpeed
		AddGradient (); //colorByLifetime
		Floats.Add(Random.Range(0f,10f)); //Emission

		//Trails
		AddGradient(); //colorOverLifetime
		AddGradient(); //colorOverTrail
		for (int i = 0; i < 24; i++) { //also SizeBySpeed and Shape and RotationOverLifetime
			Floats.Add (Random.Range(0f,1f));
		}

		//Shape
		shape = Random.Range(0,9);

		//Noise
		for (int i = 0; i < 7; i++) {
			Floats.Add (Random.Range (0.2f, 0.7f));
		}

		//Bursts------------------------------------------
		Bursts.Add (Random.Range (1, 5)); //numBursts
		for (int i = 0; i < Bursts[0]; i++) {
			Bursts.Add (i);
			for (int j = 0; j < 2; j++) {
				Bursts.Add (Random.Range (3, 15));
			}
		}

		//Enabled------------------------------------------
		for (int i = 0; i < 8; i++) {
			if (Random.Range (0f, 1f) > .5) {
				Enabled.Add (true);
			} else {
				Enabled.Add (false);
			}
		}

		//Bools--------------------------------------------
		for (int i = 0; i < 9; i++) {
			if (Random.Range (0f, 1f) > .5) {
				Bools.Add (true);
			} else {
				Bools.Add (false);
			}
		}
	}

	//Helper functions -------------------------------------
	void AddGradient(){

		/*__Gradient numbers__
		i.    color1's red
		i+1.  color1's blue
		i+2.  color1's green
		i+3.  color2's red
		i+4.  color2's blue
		i+5.  color2's green
		i+6.  color1's timecode
		i+7.  color2's timecode
		i+8.  color1's alpha
		i+9. color1's alpha timecode
		i+10. color2's alpha 
		i+11. color2's alpha timecode*/

		for (int i = 0; i < 12; i++) {
			float num;
			if (i >= 7){ //|| i == 11) {
				num = Random.Range (0.4f, 1f);
			} else {
				num = Random.Range (0f, 1f);
			}
			Floats.Add (num);
		}

	}
}

