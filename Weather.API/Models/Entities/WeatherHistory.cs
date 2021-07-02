using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weather.API.Models.Entities
{
    public class WeatherHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int CityID { get; set; }

        public int Humidity { get; set; }

        public double Temperature { get; set; }
    }
}
