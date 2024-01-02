using System;
using Reuse.CSV;
using Reuse.UI;
using Reuse.Utils;
using SaveGame;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class UIOptionSetting : MonoBehaviour
    {
        [Serializable]
        private class SettingLimits
        {
            public AnimationCurve animationValues;
            public AnimationCurve animationValuesReverseAxis;
            public float defaultValue = 0.8f;
            public float GetLimit(float value)
            {
                return animationValues.Evaluate(value);
            }
            
            public float GetPercentage(float value)
            {
                return animationValuesReverseAxis.Evaluate(value);
            }
        }
        
        [SerializeField] private AudioMixer mainMixer; 
        
        [Space][Header("SETTING COMPONENTS")] [Space]
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider cameraMovementSensibility;
        [SerializeField] private LanguageDropdown languageDropdown;
        
        [Space][Header("SETTING LIMITS COMPONENTS")] [Space]
        [SerializeField] private SettingLimits musicLimits;
        [SerializeField] private SettingLimits sfxLimits;
        [SerializeField] private SettingLimits cameraMovementLimits;
        
        [Space] [Header("SETTING VISUAL COMPONENT")] [Space] 
        [SerializeField] private UISettingVisualComponent musicComponent;
        [SerializeField] private UISettingVisualComponent soundComponent;
        [SerializeField] private UISettingVisualComponent cameraControlSensibilityComponent;
        
        [Space] [Header("BUTTON")] [Space]
        [SerializeField] private Button saveButton;
        [SerializeField] private Button exitButton;

        [Space] [Header("SETTING MAIN CONTENT")] [Space]
        [SerializeField] private GameObject visual;
        private void Awake()
        {
            saveButton.onClick.AddListener(SaveValues);
            exitButton.onClick.AddListener(SetSaveValues);

            musicSlider.onValueChanged.AddListener(MusicSliderValueChanged);
            sfxSlider.onValueChanged.AddListener(SoundSliderValueChanged);
            cameraMovementSensibility.onValueChanged.AddListener(CameraControlSensibilitySliderValueChanged);

        }
        
        //Weird but necessary but fix because of the AudioMixer
        private void Start()
        {
            SetSaveValues();
        }

        //This is the easiest way definitively, but also not the best
        public void Enable()
        {
            musicComponent.SetComponent(musicSlider.value = UtilCardSave.LoadMusic(musicLimits.defaultValue));
            soundComponent.SetComponent(sfxSlider.value = UtilCardSave.LoadSfx(sfxLimits.defaultValue));
            cameraControlSensibilityComponent.SetComponent(cameraMovementSensibility.value = 
                cameraMovementLimits.GetPercentage(UtilCardSave.LoadSensibility(cameraMovementLimits.defaultValue)));
            
            visual.SetActive(true);
            UtilPause.PauseGame();
        }

        private void SaveValues()
        {
            UtilCardSave.SaveMusic(musicSlider.value);
            UtilCardSave.SaveSfx(sfxSlider.value);
            
            //Easier to save the value
            UtilCardSave.SaveSensibility(cameraMovementLimits.GetLimit(cameraMovementSensibility.value));
            UtilCardSave.SaveLanguage(languageDropdown.GetDropdownValue());

            Disable();
        }
        
        //Also the easiest way, but not the best
        public void SetSaveValues()
        {
            mainMixer.SetFloat("BGM", musicLimits.GetLimit(UtilCardSave.LoadMusic(musicLimits.defaultValue)));
            mainMixer.SetFloat("SFX", sfxLimits.GetLimit(UtilCardSave.LoadSfx(sfxLimits.defaultValue)));
            GameVersatileTextsController.ChangeActualLanguage(UtilCardSave.LoadLanguage());

            Disable();
        }

        private void Disable()
        {
            visual.SetActive(false);
            UtilPause.ResumeGame();
        }

        private void MusicSliderValueChanged(float value)
        {
            musicComponent.SetComponent(value);
            mainMixer.SetFloat("BGM", musicLimits.GetLimit(musicSlider.value));
        }
        private void SoundSliderValueChanged(float value)
        {
            soundComponent.SetComponent(value);
            mainMixer.SetFloat("SFX", sfxLimits.GetLimit(sfxSlider.value));
        }
        private void CameraControlSensibilitySliderValueChanged(float value)
        {
            cameraControlSensibilityComponent.SetComponent(value);
        }
    }
}
