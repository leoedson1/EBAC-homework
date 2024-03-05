using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suit
{
    public class SuitItemBase : MonoBehaviour
    {
        public SuitType suitType;
        public float duration = 2f;
        public string compareTag = "Player";

        
        private void OnTriggerEnter(Collider collision)
        {
            if(collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            Debug.Log("Collect");
            var setup = SuitManager.Instance.GetSetupByType(suitType);
            Player.Instance.ChangeTexture(setup, duration);

            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}
