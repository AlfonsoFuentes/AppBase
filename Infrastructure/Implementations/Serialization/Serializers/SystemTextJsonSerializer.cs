

namespace Infrastructure.Implementations.Serialization.Serializers
{
    public class SystemTextJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerOptions _options;

        public SystemTextJsonSerializer(IOptions<SystemTextJsonOptions> options)
        {
            _options = options.Value.JsonSerializerOptions;
        }

        public T Deserialize<T>(string data)
            => System.Text.Json.JsonSerializer.Deserialize<T>(data, _options)!;

        public string Serialize<T>(T data)
            => System.Text.Json.JsonSerializer.Serialize(data, _options);
    }
}