using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class CollectableItemBase : MonoBehaviour
    {
        public ItemType itemType;

        public string compareTag = "Player";
        public ParticleSystem iParticleSystem;
        public float timeToHide = 3;
        public GameObject itemGraphic;
        public Collider[] iColliders;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
            iColliders = GetComponentsInChildren<Collider>();
            //if(coinParticleSystem != null) coinParticleSystem.transform.SetParent(null);
        }
        
        private void OnTriggerEnter(Collider collision)
        {
            if(collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        protected virtual void Collect()
        {
            if(iColliders != null)
            {
                for(int i= 0; i < iColliders.Length; ++i)
                {
                iColliders[i].enabled = false;
                }
            } 
            if(itemGraphic != null) itemGraphic.SetActive(false);
            Invoke("HideObject", timeToHide);
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect() 
        {
            if(iParticleSystem != null) iParticleSystem.Play();
            if(audioSource != null) audioSource.Play();
            ItemManager.Instance.AddByType(itemType);
        }
    }
}
