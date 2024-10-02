using System;
using System.Collections.Generic;

namespace api_sistema_agente.Domain.Entities;

public partial class Auth
{
    public int Id { get; set; }
    public string? Login { get; set; }
    public string? Mail { get; set; }
    public string? Password { get; set; }
}
