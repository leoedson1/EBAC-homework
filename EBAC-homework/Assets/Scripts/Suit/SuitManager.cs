using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

namespace Suit
{
    public enum SuitType
    {
        SPEED,
        POWER
    }

    public class SuitManager : Singleton<SuitManager>
    {
        public List<SuitSetup> suitSetups;
        public SuitSetup GetSetupByType(SuitType suit)
        {
            return suitSetups.Find(i => i.suitType == suit);
        }
    }

    [System.Serializable]
    public class SuitSetup
    {
        public SuitType suitType;
        public Texture2D texture;
    }
}
