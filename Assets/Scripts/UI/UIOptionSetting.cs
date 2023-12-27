using System;
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
            public float minValue;
            public float maxValue;

            public float GetLimit(float value)
            {
                return Mathf.Lerp(minValue, maxValue, value);
            }
        }
        
        [SerializeField] private AudioMixer mainMixer; 
        
        [Space][Header("SETTING COMPONENTS")] [Space]
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider cameraMovementSensibility;
        
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

            SetSaveValues();
        }
        
        //This is the easiest way definitively, but also not the best
        public void Enable()
        {
            musicComponent.SetComponent(musicSlider.value = UtilCardSave.LoadMusic());
            soundComponent.SetComponent(sfxSlider.value = UtilCardSave.LoadSfx());
            cameraControlSensibilityComponent.SetComponent(cameraMovementSensibility.value = UtilCardSave.LoadSensibility());
            
            visual.SetActive(true);
        }

        private void SaveValues()
        {
            UtilCardSave.SaveMusic(musicSlider.value);
            UtilCardSave.SaveSfx(sfxSlider.value);
            UtilCardSave.SaveSensibility(cameraMovementSensibility.value);

            visual.SetActive(false);
        }
        
        //Also the easiest way, but not the best
        private void SetSaveValues()
        {
            mainMixer.SetFloat("BGM", musicLimits.GetLimit(UtilCardSave.LoadMusic()));
            mainMixer.SetFloat("SFX", sfxLimits.GetLimit(UtilCardSave.LoadSfx()));
            
            visual.SetActive(false);
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
