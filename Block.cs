using System.Text.Json;
using System.Text.Json.Serialization;

namespace PostProcessor
{
    public class Block{
        //[JsonPropertyName("realization_index")]
        public int RealizationIndex {get;set;}
        //[JsonPropertyName("block_index")]
        public int BlockIndex {get;set;}
        //[JsonPropertyName("block_event_count")]
        public int BlockEventCount {get;set;}
        //[JsonPropertyName("block_event_start")]
        public Int64 BlockEventStart {get;set;}
        //[JsonPropertyName("block_event_end")]
        public Int64 BlockEventEnd {get;set;}
        public Block(){

        }
        public Block(string blockstring){
            //JsonSerializerOptions options = new JsonSerializerOptions();
            //options.PropertyNameCaseInsensitive = true;
            //options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            
            Block? block = JsonSerializer.Deserialize<Block>(blockstring);//, options);
            this.RealizationIndex = block.RealizationIndex;
            this.BlockIndex = block.BlockIndex;
            this.BlockEventCount = block.BlockEventCount;
            this.BlockEventStart = block.BlockEventStart;
            this.BlockEventEnd = block.BlockEventEnd;
        }
    }
}