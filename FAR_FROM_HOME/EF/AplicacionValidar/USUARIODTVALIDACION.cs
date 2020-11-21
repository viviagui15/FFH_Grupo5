using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAR_FROM_HOME.EF
{
    [MetadataType(typeof(USUARIODTMetaData))]
    public partial class USUARIODT
    {

    }

    public class USUARIODTMetaData
    {
        [ScaffoldColumn(false)]
        public object ID_USUARIO { get; set; }
        [Required(ErrorMessage ="{0} Es obligatorio")]
        [MinLength(15,ErrorMessage ="{0} Ingrese una dirección de correo válida")]
        [Display(Name ="Email: ")]
        public object MAIL { get; set; }
        [Required]
        [Display(Name = "Nombre: ")]
        public object NOMBRE { get; set; }
        [Required]
        [Display(Name = "Apellido: ")]
        public object APELLIDO { get; set; }
        [Required(ErrorMessage = "{0} Es obligatorio")]
        [MinLength(6, ErrorMessage = "{0} Ingrese una contraseña válida, minimo 6 caracteres")]
        [Display(Name = "Contraseña: ")]
        public object CONTRASENIA { get; set; }


    }
}