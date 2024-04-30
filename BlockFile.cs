using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;


namespace PostProcessor
{
    public class BlockFile{
        public Block[] Blocks;//it is just an array of blocks with no attribute

        public BlockFile(string jsonString){
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;

            Block[] blocks = JsonSerializer.Deserialize<Block[]>(jsonString, options);

            Blocks = blocks;
        }


        ///
        /*
        var serializeOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    WriteIndented = true
};
jsonString = JsonSerializer.Serialize(weatherForecast, serializeOptions);
        */
    }
}