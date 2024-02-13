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
        public Collider iCollider;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
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
            if(iCollider != null) iCollider.enabled = false;
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
