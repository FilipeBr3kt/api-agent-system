namespace api_sistema_agente.Domain.Entities;

public partial class Pendencie
{
    public int Id { get; private set; }

    public string IdProtocol { get; private set; } = null!;

    public string Document { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public int? IdAgent { get; private set; }

    public byte[] DateRegister { get; private set; } = null!;

    public string Title { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public string ProductName { get; private set; } = null!;

    public string? AgentName { get; private set; }

    public Pendencie(string idProtocol, string document, string name, int? idAgent, string title, string description, string productName, string? agentName)
    {
        IdProtocol = idProtocol;
        Document = document;
        Name = name;
        IdAgent = idAgent;
        Title = title;
        Description = description;
        ProductName = productName;
        AgentName = agentName;
        DateRegister = BitConverter.GetBytes(DateTime.UtcNow.Ticks);
    }
}
