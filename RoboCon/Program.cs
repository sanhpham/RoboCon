
using Microsoft.Extensions.Configuration;
using Robot.AI;
using Robot.Core.Abstractions;
using Robot.Core.Agents;
using Robot.Memory;
using System.Text;


var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

//var apiKey = config["OpenAI:ApiKey"]!;
//var model = config["OpenAI:Model"]!;

var hfKey = config["HuggingFace:ApiKey"]!;
var model = config["HuggingFace:Model"]!;

//var ai = new HuggingFaceService(hfKey, model);
var ai = new OllamaAIService("llama3.1:8b");
var shortMemory = new ConversationMemory();
var longMemory = new LongTermMemory();
var kb = new TextKnowledgeBase();

// 🔹 LOAD FILE
IDocumentLoader loader =
    new TxtDocumentLoader();
// hoặc:
// IDocumentLoader loader = new PdfDocumentLoader();

var text = loader.Load("C:\\Users\\SP\\Desktop\\test\\AIStory.txt");

// 🔹 CHUNK
var chunks = TextChunker.Chunk(text);

// 🔹 INGEST (TRAIN)
kb.Ingest(chunks);

var agent = new ChatAgent(ai, shortMemory, longMemory,kb);

Console.WriteLine("🤖 Robot is alive 22222 sssssss. Type something:");;
Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;
while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();
    if (input == "exit") break;

    var reply = await agent.AskAsync(input!);
    Console.WriteLine($"🤖 {reply}");
}
