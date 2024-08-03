using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToStartAnimation : MonoBehaviour {

	[SerializeField]TextScaleUpDown[] textAnim;
	IEnumerator PlayAnim;
	void OnEnable()
	{
		PlayAnimation();
	}
	void OnDisable()
	{
		if (PlayAnim != null)
			StopCoroutine(PlayAnim);
	}
	// Use this for initialization
	void PlayAnimation () {
		if(PlayAnim != null)
			StopCoroutine(PlayAnim);
		StartCoroutine(PlayAnim = StartScale());
	}
	int index;
	float t,d;
	float wait=0.2f;

	IEnumerator StartScale()
	{
		yield return new WaitForSeconds(1);
		textAnim[0].StartAnimation(0.5f);

		yield return new WaitForSeconds(wait);
		textAnim[1].StartAnimation(0.48f);

		yield return new WaitForSeconds(wait);
		textAnim[2].StartAnimation(0.46f);
			
		yield return new WaitForSeconds(wait);
		textAnim[3].StartAnimation(0.44f);

		yield return new WaitForSeconds(wait);
		textAnim[4].StartAnimation(0.42f);

		yield return new WaitForSeconds(wait);
		textAnim[5].StartAnimation(0.4f);

		yield return new WaitForSeconds(wait);
		textAnim[6].StartAnimation(0.37f);

		yield return new WaitForSeconds(wait);
		textAnim[7].StartAnimation(0.34f);

		yield return new WaitForSeconds(wait);
		textAnim[8].StartAnimation(0.30f);

		yield return new WaitForSeconds(wait);
		textAnim[9].StartAnimation(0.26f);

		StartCoroutine(PlayAnim = StartScale());
	}

	
}
