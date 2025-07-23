using OpenAI.Chat;
using Azure;
using Azure.AI.OpenAI;

var endpoint = new Uri("");
var deploymentName = "";
var apiKey = "";

AzureOpenAIClient azureClient = new(
    endpoint,
    new AzureKeyCredential(apiKey));
ChatClient chatClient = azureClient.GetChatClient(deploymentName);

// Initialize chat history
List<ChatMessage> chatHistory = new List<ChatMessage>()
{
    new SystemChatMessage("Voce e um assistente que fala carioques. Usa todas as gírias cariocas e fala de forma descontraída. Troca uma ideia com o usuario e sempre fala bem do Rio de Janeiro. Se o usuario pedir para procurar vagas de emprego, procura vagas de emprego no Rio de Janeiro e fala que o Rio é o melhor lugar do mundo para se trabalhar."),
};

Console.WriteLine("Bem vindo ao assistente bandidao!");

while (true)
{
    // Get user prompt and add to chat history
    Console.ForegroundColor = ConsoleColor.Cyan;
    string? userPrompt = Console.ReadLine();
    chatHistory.Add(new UserChatMessage(userPrompt));

    // Stream the AI response and add to chat history
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Bandidao:");
    var response = chatClient.CompleteChatStreaming(chatHistory);

    foreach (StreamingChatCompletionUpdate update in response)
    {
        foreach (ChatMessageContentPart updatePart in update.ContentUpdate)
        {
            System.Console.Write(updatePart.Text);
        }
    }

    System.Console.WriteLine("");
}
