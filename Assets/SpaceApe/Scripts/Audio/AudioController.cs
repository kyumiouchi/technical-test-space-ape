using System;
using System.Collections;
using UnityEngine;

namespace DodgyBoxes
{
    /// <summary>
    /// AudioController is singleton responsible for manager the audio
    /// </summary>
    public class AudioController : MonoBehaviourSingleton<AudioController>
    {
        /// <summary>
        /// Audio actions
        /// </summary>
        public enum AudioAction
        {
            /// <summary>
            /// Play clip
            /// </summary>
            START,
            /// <summary>
            /// Stop clip
            /// </summary>
            STOP,
            /// <summary>
            /// Restart clip
            /// </summary>
            RESTART
        }
        
        /// <summary>
        /// Array of all audioClip and audioSource
        /// </summary>
        [SerializeField] private AudioTrack[] tracks;
        
        /// <summary>
        /// Hashtable to access all audioClip
        /// </summary>
        private Hashtable _audioTable;
        
        /// <summary>
        /// AudioObject relates audioType and audioClip
        /// </summary>
        [Serializable]
        public class AudioObject
        {
            public AudioType type;
            public AudioClip clip;
        } 
        
        /// <summary>
        /// AudioObject relates audioSource and all clips
        /// </summary>
        [Serializable]
        public class AudioTrack
        {
            public AudioSource source;
            public AudioObject[] audio;
        }
        
        private void Start()
        {
            _audioTable = new Hashtable();
            GenerateAudioTable();
        }

        /// <summary>
        /// GenerateAudioTable fill the audios to access
        /// </summary>
        private void GenerateAudioTable()
        {
            foreach (AudioTrack _track in tracks)
            {
                foreach (AudioObject _audio in _track.audio)
                {
                    if (!_audioTable.ContainsKey(_audio.type))
                    {
                        _audioTable.Add(_audio.type, _track);
                    }
                }
            }
        }

        /// <summary>
        /// Get audio clip of list of audios
        /// </summary>
        /// <param name="type">type of the audio</param>
        /// <param name="track">list of audios</param>
        /// <returns></returns>
        public AudioClip GetAudioClipFromAudioTrack(AudioType type, AudioTrack track)
        {
            foreach (AudioObject audio in track.audio)
            {
                if (audio.type == type)
                {
                    return audio.clip;
                }
            }

            return null;
        }
    /// <summary>
    /// RunAction does one action, as Play.
    /// </summary>
    /// <param name="type">type of the audio</param>
    /// <param name="action">audio execution action</param>
        public void RunAudio(AudioType type, AudioAction action = AudioAction.START)
        {
            AudioTrack track = (AudioTrack)_audioTable[type];
            track.source.clip = GetAudioClipFromAudioTrack(type, track);

            switch (action)
            {
                case AudioAction.START:
                    track.source.Play();
                    break;
                case AudioAction.STOP:
                    track.source.Stop();
                    break;
                case AudioAction.RESTART:
                    track.source.Play();
                    track.source.Stop();
                    break;
            }
        }
    }
}