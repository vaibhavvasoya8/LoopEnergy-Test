using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScaleUpDown : MonoBehaviour {

	Text text;
	float t,d;

	void OnDisable()
	{
		transform.localScale = new Vector3(1.1f,1.1f,1.1f);
		text.color = new Color(text.color.r,text.color.g,text.color.b,1);
	}
    private void Start()
    {
		text = GetComponent<Text>();
    }
    public void StartAnimation(float duration){
		StartCoroutine(ScaleUp(this.transform,duration));
	}
	
	IEnumerator ScaleUp(Transform obj,float duration)
	{
		t=0;
		d=duration;
		while (t<d)
		{
			t+=Time.deltaTime;
			obj.localScale = Vector2.Lerp(new Vector2(1.1f,1.1f),new Vector2(1.35f,1.35f),(t/d));
			text.color = new Color(text.color.r,text.color.g,text.color.b,Mathf.Lerp(1,0.5f,(t/d)));
		yield return null;
		}
		StartCoroutine(ScaleDown(obj,duration));
	}

	IEnumerator ScaleDown(Transform obj,float duration)
	{
		t=0;
		d= duration - 0.1f;
		while (t<d)
		{
			t+=Time.deltaTime;
			obj.localScale = Vector2.Lerp(new Vector2(1.35f,1.35f),new Vector2(1.1f,1.1f),(t/d));
			text.color = new Color(text.color.r,text.color.g,text.color.b,Mathf.Lerp(0.5f,1,(t/d)));
		yield return null;
		}
	}
}
