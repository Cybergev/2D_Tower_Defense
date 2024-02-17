using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class SoundsAsset : ScriptableObject
{
    [SerializeField] private AudioClip[] sounds = new AudioClip[0];
    public AudioClip this[Sound s] => sounds[(int)s];

    #region
    #if UNITY_EDITOR
    [CustomEditor(typeof(SoundsAsset))]
    public class SoundsInspector : Editor
    {
        private static readonly int soundCount = Enum.GetValues(typeof(Sound)).Length;

        private new SoundsAsset target => base.target as SoundsAsset;
        public override void OnInspectorGUI()
        {

            if (target.sounds.Length < soundCount)
                Array.Resize(ref target.sounds, soundCount);
            for (int i = 0; i < target.sounds.Length; i++)
            {
                if (i == 0)
                {
                    EditorGUILayout.PrefixLabel($"{(Sound)i}");
                    target.sounds[i] = null;
                }
                else
                    target.sounds[i] = EditorGUILayout.ObjectField($"{(Sound)i}:", target.sounds[i], typeof(AudioClip), false) as AudioClip;
            }
            EditorUtility.SetDirty(target);
        }
    }
    #endif
    #endregion
}
