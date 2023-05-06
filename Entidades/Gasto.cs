﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class Gasto
    {
        public Gasto(string idGasto, double costo, string materiaPrima, DateTime fecha, int unidades, float mililitros, float gramos, string descripcion)
        {
            this.idGasto = idGasto;
            this.costo = costo;
            this.materiaPrima = materiaPrima;
            this.fecha = fecha;
            this.unidades = unidades;
            this.mililitros = mililitros;
            this.gramos = gramos;
            this.descripcion = descripcion;
        }

        public String idGasto { get; set; }
        public double costo { get; set; }
        public string materiaPrima { get; set; }
        public DateTime fecha { get; set; }
        public int unidades { get; set; }
        public float mililitros { get; set; }
        public float gramos { get; set; }
        String descripcion { get; set; }
    }
}