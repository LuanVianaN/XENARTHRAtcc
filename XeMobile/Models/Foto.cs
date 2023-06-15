using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace XeMobile.Models
{
    //Foto local
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
    //Teste de envio para api do site
    public class FotoT
    {
        public int ID_Usuario { get; set; }
        public int ID_Animal { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public int ID_Verificado { get; set; }
        public int Imagem { get; set; }
        public string OBS { get; set; }
    }

}
