
using System.ComponentModel.DataAnnotations;

namespace Programa_Trainee_VixTeam.Models
{
    public class Empresa
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public int Codigo { get; set; }
        [Required]
        [Display(Name = "Nome Fanatasia")]
        public string NomeFantasia { get; set; }    
        [Required]
        public string cpnj { get; set; }

       

        
        
    }
}
