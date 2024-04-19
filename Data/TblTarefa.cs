using System;
using System.Collections.Generic;

namespace AppToDoList.Data;

public partial class TblTarefa
{
    public int IdTarefas { get; set; }

    public string? Titulo { get; set; }

    public string? Descricao { get; set; }

    public bool Status { get; set; }

    public DateTime? DataCriacao { get; set; }

    public DateTime? DataConclusao { get; set; }
}
