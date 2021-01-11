using System;
using Newtonsoft.Json;

namespace TrueOrFalse
{
    [Serializable]
    public class Question
    {
        [JsonProperty("category")]
        public string category { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("difficulty")]
        public string difficulty { get; set; }

        [JsonProperty("question")]
        public string question { get; set; }

        [JsonProperty("correct_answer")]
        public string correct_answer { get; set; }

        [JsonProperty("incorrect_answers")]
        public string[] incorrect_answers { get; set; }
    }
}
