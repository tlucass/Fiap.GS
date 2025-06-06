using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.GS.Model
{
    public class Manutencao
    {
        public DateTime DataHoraAgendada { get; set; }
        public string Responsavel { get; set; }
        public string TipoIntervencao { get; set; } 
    }
}
