using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Fecha
    {
        public int id_fecha {  get; set; }
        public int id_empresa { get; set; }
        public int id_lugar_origen { get; set; }
        public int id_lugar_destino { get; set; }
        public int id_transporte { get; set; }
        public DateTime fecha_ida {  get; set; }
        public DateTime fecha_vuelta { get; set; }
        public string categoria_tipo { get; set; }


        public Fecha()
        {

        }


        public Fecha(int id_fecha, int id_empresa, int id_origen, int id_destino, int id_trans, DateTime ida, DateTime vuelta, string tip)
        {
            this.id_fecha = id_fecha;
            this.id_empresa = id_empresa;
            this.id_lugar_origen = id_origen;
            this.id_lugar_destino = id_destino;
            this.id_transporte = id_trans;
            this.fecha_ida = ida;
            this.fecha_vuelta = vuelta;
            this.categoria_tipo = tip;
        }

        public Fecha(int id_empresa, int id_origen, int id_destino, int id_trans, DateTime ida, DateTime vuelta, string tip)
        {
             this.id_empresa = id_empresa;
            this.id_lugar_origen = id_origen;
            this.id_lugar_destino = id_destino;
            this.id_transporte = id_trans;
            this.fecha_ida = ida;
            this.fecha_vuelta = vuelta;
            this.categoria_tipo = tip;
        }
    }
}
