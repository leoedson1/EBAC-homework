using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Suit;

namespace Suit
{
    public class SuitSwapper : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;
        public Texture2D texture;

        private Texture2D _defaultTexture;

        public string shaderIDName = "_EmissionMap";

        private void Awake()
        {
            _defaultTexture = (Texture2D) mesh.sharedMaterials[0].GetTexture(shaderIDName);
        }

        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            mesh.sharedMaterials[0].SetTexture(shaderIDName, texture);
        }

        public void ChangeTexture(SuitSetup setup)
        {
            mesh.sharedMaterials[0].SetTexture(shaderIDName, setup.texture);
        }

        public void ResetTexture()
        {
            mesh.sharedMaterials[0].SetTexture(shaderIDName, _defaultTexture);
        }
    }
}
