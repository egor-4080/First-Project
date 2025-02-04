#if YandexGamesPlatform_yg
using System.Runtime.InteropServices;
using UnityEngine;
using YG.Insides;
using YG.Utils.LB;

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        public void SetLeaderboard(string nameLB, int score, string extraData)
        {
            SetLB_js(nameLB, score, extraData);
        }

        public void GetLeaderboard(string nameLB, int quantityTop, int quantityAround, string photoSizeLB)
        {
            if (YG2.infoYG.Leaderboards.enable)
            {
                YG2.Message("Get Leaderboard");
                GetLB_js(nameLB, quantityTop, quantityAround, photoSizeLB, YG2.player.auth);
            }
            else
            {
                YG2.onGetLeaderboard?.Invoke(YGInsides.NoLBData(nameLB));
            }
        }

        [DllImport("__Internal")]
        private static extern void SetLB_js(string nameLB, int score, string extraData);

        [DllImport("__Internal")]
        private static extern void GetLB_js(string nameLB, int quantityTop, int quantityAround, string photoSizeLB,
            bool auth);

        public class JsonLB
        {
            public int decimalOffset;
            public string entries;
            public string[] extraDataArray;
            public bool isDefault;
            public bool isInvertSortOrder;
            public string[] names;
            public string[] photos;
            public int[] ranks;
            public int[] scores;
            public string technoName;
            public string type;
            public string[] uniqueIDs;
        }
    }
}

namespace YG.Insides
{
    public partial class YGSendMessage
    {
        public void LeaderboardEntries(string data)
        {
            PlatformYG2.JsonLB jsonLB = JsonUtility.FromJson<PlatformYG2.JsonLB>(data);

            LBData lbData = new LBData()
            {
                technoName = jsonLB.technoName,
                isDefault = jsonLB.isDefault,
                isInvertSortOrder = jsonLB.isInvertSortOrder,
                decimalOffset = jsonLB.decimalOffset,
                type = jsonLB.type,
                entries = jsonLB.entries,
                players = new LBPlayerData[jsonLB.names.Length],
                currentPlayer = null
            };

            for (int i = 0; i < jsonLB.names.Length; i++)
            {
                lbData.players[i] = new LBPlayerData();
                lbData.players[i].name = jsonLB.names[i];
                lbData.players[i].rank = jsonLB.ranks[i];
                lbData.players[i].score = jsonLB.scores[i];
                lbData.players[i].photo = jsonLB.photos[i];
                lbData.players[i].uniqueID = jsonLB.uniqueIDs[i];

                if (jsonLB.extraDataArray[i] == InfoYG.NO_DATA)
                    lbData.players[i].extraData = null;
                else
                    lbData.players[i].extraData = jsonLB.extraDataArray[i];

                if (jsonLB.uniqueIDs[i] == YG2.player.id)
                {
                    lbData.currentPlayer = new LBCurrentPlayerData
                    {
                        rank = lbData.players[i].rank,
                        score = lbData.players[i].score,
                        extraData = lbData.players[i].extraData
                    };
                }
            }

            YG2.onGetLeaderboard?.Invoke(lbData);
        }
    }
}
#endif