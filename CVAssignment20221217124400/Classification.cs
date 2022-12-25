using CVAssignment20221217124400.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Classification
{

    public class Classfction
    {

        public bool GetClassificationResult(Prediction prediction, string tagName, double probToTrig)
        {
            bool triged = false;

            if (prediction.tagName == tagName)
            {
                if (prediction.probability < probToTrig)
                {
                    triged = true;
                }
            }

            return triged;
        }

        public bool GetClassificationResult(List<Prediction> predictions, string tagName, double probToTrig)
        {
            bool triged = false;

            Console.WriteLine("Classification: Processing predictions");
            foreach(Prediction prediction in predictions)
            {
                Console.WriteLine($"Classification: Processing-Tag name: {prediction.tagName}-Probability: {prediction.probability}");
                bool tmpTriged = GetClassificationResult(prediction, tagName, probToTrig);
                Console.WriteLine("Classification: Processing done");
                if (tmpTriged == true)
                {
                    triged = true;
                }
            }
            Console.WriteLine("Classification: Processing predictions done");

            return triged;
        }

        public void GetClassificationResult(MaOwnPredModel predModel, string tagName, double probToTrig)
        {
            bool triged;

            triged = GetClassificationResult(predModel.ClassificationPredictions, tagName, probToTrig);

            Console.WriteLine("Classification: Processing predictions");
            if(triged == true)
            {
                predModel.Name += "Triggered!";
            }
            predModel.Triggered = triged;
            Console.WriteLine("Classification: Processing done");
        }

    }

}