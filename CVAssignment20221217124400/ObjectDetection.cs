using PredictionModels;
using System.Drawing;
using System.Collections.Generic;
using System;

namespace ObjectDetection
{

    public class ObjDetection
    {

        public Bitmap GetPredictionBitmapImg(Bitmap oriImg, Prediction prediction)
        {
            Rectangle ruler = GetRect(oriImg, prediction.boundingBox);
            Bitmap objImg = CutImg(oriImg, ruler);

            return objImg;
        }

        public List<Bitmap> GetPredictionBitmapImg(Bitmap oriImg, List<Prediction> predictions)
        {
            List<Bitmap> objImg = new List<Bitmap>();

            foreach(Prediction prediction in predictions)
            {
                objImg.Add(GetPredictionBitmapImg(oriImg, prediction));
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