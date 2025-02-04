using System;
using UnityEngine;
using UnityEngine.UI;
#if TMP_YG2
using TMPro;
#endif

namespace YG
{
    public class LBPlayerDataYG : MonoBehaviour
    {
        public ImageLoadYG imageLoad;
        public TextLegasy textLegasy;

        [Space(10)] public MonoBehaviour[] topPlayerActivityComponents = new MonoBehaviour[0];

        public MonoBehaviour[] currentPlayerActivityComponents = new MonoBehaviour[0];

        [HideInInspector] public Data data = new Data();

        public void UpdateEntries()
        {
            if (textLegasy.rank && data.rank != null) textLegasy.rank.text = data.rank.ToString();
            if (textLegasy.name && data.name != null) textLegasy.name.text = data.name;
            if (textLegasy.score && data.score != null) textLegasy.score.text = data.score.ToString();

#if TMP_YG2
            if (textMP.rank && data.rank != null) textMP.rank.text = data.rank.ToString();
            if (textMP.name && data.name != null) textMP.name.text = data.name;
            if (textMP.score && data.score != null) textMP.score.text = data.score.ToString();
#endif
            if (imageLoad)
            {
                if (data.photoSprite)
                {
                    imageLoad.SetTexture(data.photoSprite.texture);
                }
                else if (data.photoUrl == null)
                {
                    imageLoad.ClearTexture();
                }
                else
                {
                    imageLoad.Load(data.photoUrl);
                }
            }

            if (topPlayerActivityComponents.Length > 0)
            {
                if (data.inTop)
                {
                    ActivityMomoObjects(topPlayerActivityComponents, true);
                }
                else
                {
                    ActivityMomoObjects(topPlayerActivityComponents, false);
                }
            }

            if (currentPlayerActivityComponents.Length > 0)
            {
                if (data.currentPlayer)
                {
                    ActivityMomoObjects(currentPlayerActivityComponents, true);
                }
                else
                {
                    ActivityMomoObjects(currentPlayerActivityComponents, false);
                }
            }

            void ActivityMomoObjects(MonoBehaviour[] objects, bool activity)
            {
                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].enabled = activity;
                }
            }
        }

        [Serializable]
        public struct TextLegasy
        {
            public Text rank, name, score;
        }

        public class Data
        {
            public bool currentPlayer;
            public bool inTop;
            public string name;
            public Sprite photoSprite;
            public string photoUrl;
            public string rank;
            public string score;
        }

#if TMP_YG2
        [Serializable]
        public struct TextMP
        {
            public TextMeshProUGUI rank, name, score;
        }

        public TextMP textMP;
#endif
    }
}