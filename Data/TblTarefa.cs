using System.ComponentModel.DataAnnotations;

namespace AppToDoList.Data;

public partial class TblTarefa
{
    public int IdTarefas { get; set; }

    [Required(ErrorMessage = "O campo Título é obrigatório.")]
    public string? Titulo { get; set; }

    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public string? Descricao { get; set; }

    public bool Status { get; set; }

    public DateTime? DataCriacao { get; set; }

    public DateTime? DataConclusao { get; set; }
}
