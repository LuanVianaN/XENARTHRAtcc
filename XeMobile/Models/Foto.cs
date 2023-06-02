using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace XeMobile.Models
{
    public class Foto
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string Comentario { get; set; }
        public DateTime Data{ get; set; }
        public string FotoPath { get; set; }
        public double Latitude { get; set; } = 0;
        public double Longitude { get; set; } = 0;
    }
}
