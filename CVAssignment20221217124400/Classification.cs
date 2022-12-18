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

            foreach(Prediction prediction in predictions)
            {
                bool tmpTriged = GetClassificationResult(prediction, tagName, probToTrig);
                if (tmpTriged == true)
                {
                    triged = true;
                }
            }

            return triged;
        }

        public void GetClassificationResult(MaOwnPredModel predModel, string tagName, double probToTrig)
        {
            bool triged;

            triged = GetClassificationResult(predModel.ClassificationPredictions, tagName, probToTrig);

            if(triged == true)
            {
                predModel.Name += "Triggered!";
            }
            predModel.Triggered = triged;
        }

    }

}