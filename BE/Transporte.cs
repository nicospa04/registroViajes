using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Transporte
    {
        public int id_transporte {  get; set; }
        public string modelo { get; set; }
        public decimal porcentaje_extra { get; set; }

        public int id_tipo_transporte { get; set; }

        public int id_empresa { get; set; }

        public Transporte(int id_transporte, string modelo, decimal porcentaje_extra, int id_tipo_transporte, int id_empresa)
        {
            this.id_transporte = id_transporte;
            this.modelo = modelo;
            this.porcentaje_extra = porcentaje_extra;
            this.id_tipo_transporte = id_tipo_transporte;
            this.id_empresa = id_empresa;
        }
    }
}
