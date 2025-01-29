using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaLibro.Modelo.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Categoria es obligatoria")]
        [Display(Name = "Categoria")]
        [StringLength(60, ErrorMessage = "Nombre debe ser maximo 60 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion es obligatoria")]
        [StringLength(200, ErrorMessage = "Nombre debe ser maximo 200 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado es obligatorio")]
        public bool Estado { get; set; }
    }
}
