using app_mascota.Models;
using System.Collections.Generic;

namespace app_mascota.ViewModel
{
    public class MascotaViewModel
    {
        public Mascota? FormularioMascota { get; set; }
        public List<Mascota>? ListaMascota { get; set; }
    }
}