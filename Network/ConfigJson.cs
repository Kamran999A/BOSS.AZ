using Newtonsoft.Json;

namespace Bossaz.Network
{
    public struct ConfigJson
    {
        [JsonProperty("mail")]
        public string Mail { get; private set; }

        [JsonProperty("password")]
        public string Password { get; private set; }
    }
}