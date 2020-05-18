using System;
using Newtonsoft.Json;

namespace Assets.Scripts.Entities
{
    [Serializable]
    [JsonObject]
    public class Score
    {
        public long ScoreAmount { get; set; }

    }
}