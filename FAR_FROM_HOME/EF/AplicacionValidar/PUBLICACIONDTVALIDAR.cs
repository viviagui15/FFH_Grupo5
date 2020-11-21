using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAR_FROM_HOME.EF
{
    [MetadataType(typeof(PUBLICACIONDTMetaData))]
    public partial class PUBLICACIONDT
    {

}

public class PUBLICACIONDTMetaData
    {
        [ScaffoldColumn(false)]
        public int ID_PUBLICACION { get; set; }

        public Nullable<int> ID_USUARIO { get; set; }
        [Required(ErrorMessage = "{0} Es obligatorio")]
        [Display(Name = "Ubicación")]
        public string UBICACION { get; set; }
        [Display(Name = "Raza de mascota")]
        public string RAZA { get; set; }
        [Display(Name = "Salud de mascota")]
        public string ESTADO_SALUD { get; set; }
        [Required(ErrorMessage = "{0} Es obligatorio")]
        [Display(Name = "Tipo de mascota")]
        public string TIPO_MASCOTA { get; set; }
        [Display(Name = "Color de mascota")]
        public string COLOR_MASCOTA { get; set; }
        [Display(Name = "Tamaño de mascota")]
        public string TAMAÑO { get; set; }
        [Display(Name = "Edad de mascota")]
        public string EDAD { get; set; }
        [Display(Name = "Redes Sociales")]
        public string REDES { get; set; }
        [MaxLength(1, ErrorMessage = "{0} Estado no válido. M:Macho / H:Hembra")]
        [Display(Name = "Sexo de mascota")]
        public string SEXO { get; set; }
        [Display(Name = "Se busca tránsito? ")]
        public string TRANSITO { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio (E:Encontrado / P:Perdido)")]
        [MaxLength(1, ErrorMessage = "{0} no válido. E:Encontrado / P:Perdido")]
        [Display(Name = "Estado de publicacion: ")]
        public string EST_ENCPERD { get; set; }
        [Required(ErrorMessage = "{0} Ingrese una fecha válida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] //formato en el q visualizamos el formato y aply establece el valor a traves de VF
        [Display(Name = "Fecha de publicación")]
        public Nullable<System.DateTime> F_PUBLICACION { get; set; }
        

        public byte[] IMAGEN { get; set; }
        [Display(Name = "Descripción de la publucación")]
        public string DESCRIPCION { get; set; }


        public virtual USUARIODT USUARIODT { get; set; }
    }

}