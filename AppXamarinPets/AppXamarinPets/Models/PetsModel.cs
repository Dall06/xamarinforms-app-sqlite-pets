using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppXamarinPets.Models
{
    [Table("Pets")]
    public class PetsModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public string PictureFileBase64 { get; set; }

        public DateTime BirthDate { get; set; }

        public string Gender { get; set; }

        public string DogBreed { get; set; }

        public double Weight { get; set; }

        public string Comments { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
