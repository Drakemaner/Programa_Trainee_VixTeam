
using Programa_Trainee_VixTeam.Data;
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

        public bool limitedeFilhos()
        {
            if(QuantidadeFilhos <= 0)
            {
                return false;

            }
            else
            {
                return true;
            }
            
        }

        public bool limiteSalario()
        {
            if(Salario < 1200 || Salario > 13000)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool emailsIguais(PessoaModel pessoa,Programa_Trainee_VixTeamContext context)
        {
           var emailsIguais = context.PessoaModel.Where(x => x.Email.Equals(pessoa.Email) && x.Codigo != pessoa.Codigo);

           if(emailsIguais.Count() > 0)
            {
                return true;
            }
           else
            {
                return false;
            }
            
        }

        public bool InativoEdicao()
        {
           
            if(Situacao == "Inativo")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }
}
