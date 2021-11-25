using System.ComponentModel.DataAnnotations;

namespace Programa_Trainee_VixTeam.Models
{
    public class PessoaModel
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime DatadeNascimento { get; set; }

        public int QuantidadeFilhos { get; set; }

        public decimal Salario { get; set; }

        public string Situacao { get; set; } = "Ativo";
    }
}
