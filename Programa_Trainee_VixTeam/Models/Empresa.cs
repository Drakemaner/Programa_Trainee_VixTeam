
using System.ComponentModel.DataAnnotations;

namespace Programa_Trainee_VixTeam.Models
{
    public class EmpresaModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        [Key]
        public int Codigo { get; set; }
        [Required]
        
        public string NomeFantasia { get; set; }    
        [Required]
        public string cpnj { get; set; }

       

        
        
    }
}
