using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CVAssignment20221217124400.Models
{
    public class MaOwnPredModel
    {
        public Bitmap Image { get; set; }
        public double Probability { get; set; }
        public MyPredictionModel ClassificationPrediction { get; set; }
    }
}
