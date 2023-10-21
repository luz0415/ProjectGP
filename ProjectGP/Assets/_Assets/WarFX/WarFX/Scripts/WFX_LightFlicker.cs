using UnityEngine;
using System.Collections;

/**
 *	Rapidly sets a light on/off.
 *	
 *	(c) 2015, Jean Moreno
**/

[RequireComponent(typeof(Light))]
public class WFX_LightFlicker : MonoBehaviour
{
	public float time = 0.05f;
	
	private float timer;
	
	void Start ()
	{
		timer = time;
	}

	public void MuzzleLight()
	{
        timer = time;
        StartCoroutine("Flicker");
    }

	IEnumerator Flicker()
	{

		GetComponent<Light>().enabled = true;

		yield return new WaitForSeconds(time);

		do
		{
			timer -= Time.deltaTime;				
			yield return null;
		}
		while(timer > 0);

        GetComponent<Light>().enabled = false;
    }
}
