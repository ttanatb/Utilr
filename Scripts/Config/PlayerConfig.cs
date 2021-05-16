using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Text;

namespace Configr
{
    [System.Serializable]
    public class PlayerConfig
    {
        [SerializeField]
        private float m_bgmVolume;
        [System.NonSerialized]
        private UnityEvent<float> m_onBGMVolumeChanged = new UnityEvent<float>();
        public void AddOnBGMVolumeChangedListener(UnityAction<float> cb)
        {
            m_onBGMVolumeChanged.AddListener(cb);
        }
        public float BGMVolume
        {
            get { return m_bgmVolume; }
            set
            {
                m_bgmVolume = value;
                m_onBGMVolumeChanged?.Invoke(value);
            }
        }

        [SerializeField]
        private float m_sfxVolume;
        [System.NonSerialized]
        private UnityEvent<float> m_onSFXVolumeChanged = new UnityEvent<float>();
        public void AddOnSFXVolumeChangedListener(UnityAction<float> cb)
        {
            m_onSFXVolumeChanged.AddListener(cb);
        }
        public float SFXVolume
        {
            get { return m_sfxVolume; }
            set
            {
                m_sfxVolume = value;
                m_onSFXVolumeChanged?.Invoke(value);
            }
        }

        public void Copy(PlayerConfig other)
        {
            BGMVolume = other.BGMVolume;
            SFXVolume = other.SFXVolume;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("BGM Volume: {0}, SFX Volume: {1}", BGMVolume, SFXVolume);
            return sb.ToString();
        }
    }
}
