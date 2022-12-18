using CVAssignment20221217124400.Models;
using System.Drawing;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ObjectDetection
{

    public class ObjDetection
    {

        public void GetMaOwnPredModel(List<MaOwnPredModel> predModel, List<Prediction> predictions, Bitmap oriImg, string wantedTagName, double probToPass = 0.0)
        {
            int index = 0;
            foreach (Prediction pred in predictions)
            {
                if (pred.tagName == wantedTagName)
                {
                    if (pred.probability >= probToPass)
                    {
                        predModel.Add(new MaOwnPredModel()
                        {
                            Name = $"{wantedTagName}NO:{index}",
                            Image = GetCroppedPredictionBitmapImg(oriImg, pred),
                            Probability = pred.probability
                        });
                        index++;
                    }
                }
            }
        }

        public List<MaOwnPredModel> GetMaOwnPredModel(List<Prediction> predictions, Bitmap oriImg, string wantedTagName, double probToPass = 0.0)
        {
            List<MaOwnPredModel> result = new List<MaOwnPredModel>();
            GetMaOwnPredModel(result, predictions, oriImg, wantedTagName, probToPass);

            return result;
        }

        public Bitmap GetCroppedPredictionBitmapImg(Bitmap oriImg, Prediction prediction)
        {
            Rectangle ruler = GetRect(oriImg, prediction.boundingBox);
            Bitmap objImg = CutImg(oriImg, ruler);

            return objImg;
        }

        public List<Bitmap> GetCroppedPredictionBitmapImg(Bitmap oriImg, List<Prediction> predictions)
        {
            List<Bitmap> objImg = new List<Bitmap>();

            foreach(Prediction prediction in predictions)
            {
                objImg.Add(GetCroppedPredictionBitmapImg(oriImg, prediction));
            }

            return objImg;
        }

        public Rectangle GetRect(Bitmap img, BoundingBox bdBox)
        {
            int calcedLeft = Convert.ToInt32(img.Width * bdBox.left);
            int calcedTop = Convert.ToInt32(img.Height * bdBox.top);
            int calcedWidth = Convert.ToInt32(img.Width * bdBox.width);
            int calcedHeight = Convert.ToInt32(img.Height * bdBox.height);

            Rectangle rect = new Rectangle(calcedLeft, calcedTop, calcedWidth, calcedHeight);

            return rect;
        }

        public Bitmap CutImg(Bitmap img, Rectangle ruler)
        {
            Bitmap cuttedImg = img.Clone(ruler, img.PixelFormat);

            return cuttedImg;
        }

    }

}