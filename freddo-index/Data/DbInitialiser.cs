using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using freddo_index.Models;

namespace freddo_index.Data
{
    class ChangePointsTransistion
    {
        public List<PriceChangePoint> data { get; set; }
    }
    public class DbInitialiser
    {
        public static void Initialise(FreddoIndexContext context)
        {
            if (context.PriceChangePoints.Any())
                return; // Database has been seeded 
            string rawJson = File.ReadAllText("./DefaultData/change-points.json");
            List<PriceChangePoint> data = JsonSerializer.Deserialize<ChangePointsTransistion>(rawJson).data;
            context.PriceChangePoints.AddRange(data);
            context.SaveChanges();
        }
    }
}
