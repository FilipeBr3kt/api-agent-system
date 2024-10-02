namespace api_sistema_agente.Domain.Entities;

public partial class Pendencie
{
    public int Id { get; set; }

    public string IdProtocol { get; set; } = null!;

    public string Document { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? IdAgent { get; set; }

    public byte[] DateRegister { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? AgentName { get; set; }
}
