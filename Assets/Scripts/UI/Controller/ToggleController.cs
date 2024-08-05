using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour {

	[SerializeField]
	Sprite btnOnImg;
	[SerializeField]
	Sprite btnOffImg;
	[SerializeField]
	float speed;

	[SerializeField]
	Image soundBtnImg;
	[SerializeField]
	Image musicBtnImg;

	[SerializeField]Slider SoundSlider;
	[SerializeField]Slider musicSlider;

	bool isClicked;

	int changeValue;

	void Start()
	{
		changeValue = 1;
		isClicked = false;
		setValue(SoundSlider, !SavedDataHandler.instance._saveData.isSFXMute);
		setValue(musicSlider, !SavedDataHandler.instance._saveData.isMusicMute);
	}
	void setValue(Slider slider,bool isOn){
		if(isOn){
			slider.value = 1;
		}
		else{
			slider.value = 0;
		}
		SetBtnImage(slider);
	}
	public void OnToggleClicked(Slider slider){
		if(!isClicked){
			AudioManager.instance.Play(AudioType.UIButton);
			if(slider.value == 1){
				changeValue = 0;
			}
			else{
				changeValue = 1;
			}
			isClicked = true;
			StartCoroutine("SlideButton",slider);
			
		}
	}

	IEnumerator SlideButton(Slider sliderBtn)
	{
		while (sliderBtn.value != changeValue)
		{
			if(changeValue == 1){
				sliderBtn.value += Time.deltaTime * speed;
			}
			else{
				sliderBtn.value -= Time.deltaTime * speed;
			}
			yield return null;
		}
		SetBtnImage(sliderBtn);
		isClicked = false;
		
	}

	void SetBtnImage(Slider sliderBtn){
		if(sliderBtn.name == "MusicSlider"){
			if(sliderBtn.value == 1){
				musicBtnImg.sprite = btnOnImg;
				AudioManager.instance.SetMusicMute(false);
			}else{
				musicBtnImg.sprite = btnOffImg;
				AudioManager.instance.SetMusicMute(true);
			}
		}
		else if(sliderBtn.name == "SoundSlider"){
			if(sliderBtn.value == 1){
				soundBtnImg.sprite = btnOnImg;
				AudioManager.instance.SetSFXMute(false);
			}
			else
			{
				soundBtnImg.sprite = btnOffImg;
				AudioManager.instance.SetSFXMute(true);
			}
		}
	}

}
