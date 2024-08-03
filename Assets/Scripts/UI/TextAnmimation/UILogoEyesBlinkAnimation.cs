using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILogoEyesBlinkAnimation : MonoBehaviour {

	[SerializeField]Transform[] eyes;
	float elapsed,duration;
	IEnumerator blink;

	void OnEnable()
	{
		StratBlink();
	}
	void OnDisable()
	{
		if (blink != null)
			StopCoroutine(blink);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	void StratBlink(){
		if(blink != null)
			StopCoroutine(blink);
		StartCoroutine(blink = Blink());
	}

	IEnumerator Blink()
	{
		elapsed = 0;
		duration = 0.15f;
		Vector2 value;
		while (elapsed < duration)
		{
			value = Vector2.Lerp(new Vector2(1, 1), new Vector2(1, 0), (elapsed / duration));
            for (int i = 0; i < eyes.Length; i++)
            {
				eyes[i].localScale = value;
            }
			elapsed += Time.deltaTime;
			yield return null;
		}
		elapsed=0;
		while (elapsed < duration)
		{
			value = Vector2.Lerp(new Vector2(1,0),new Vector2(1,1),(elapsed / duration));
			for (int i = 0; i < eyes.Length; i++)
			{
				eyes[i].localScale = value;
			}
			elapsed += Time.deltaTime;
			yield return null;
		}
		for (int i = 0; i < eyes.Length; i++)
		{
			eyes[i].localScale = Vector3.one;
		}
		yield return new WaitForSeconds(Random.Range(2,3.5f));
		StratBlink();
	}
}
