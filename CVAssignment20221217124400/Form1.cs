using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsingAPI;
using PredictionModels;
using ObjectDetection;

namespace CVAssignment20221217124400
{
    public partial class Form1 : Form
    {

        private string predictionKey = "35532dc500874d7a86cf0e18b789bc5a";
        private string predictionUrl = "https://yysprediction.cognitiveservices.azure.com/customvision/v3.0/Prediction/f79b57d5-32a9-4566-949d-b144204c7ae0/detect/iterations/Iteration4/image";
        private string classificationKey = "";
        private string classificationUrl = "";

        private TargetAPI objdttctAPI;
        private TargetAPI classAPI;

        private string imgFileName;
        private Bitmap oriImg;
        private List<Prediction> predictions = new List<Prediction>();
        private List<Bitmap> predImgs = new List<Bitmap>();

        private int index = 0;

        public Form1()
        {
            InitializeComponent();
            objdttctAPI = new TargetAPI(predictionKey, predictionUrl);
            classAPI = new TargetAPI(classificationKey, classificationUrl);
            PrevButton.Visible = false;
            NextButton.Visible = false;
            ObjDttctProbLabel.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void BrowseImgButton_Click(object sender, EventArgs e)
        {
            predictions.Clear();
            predImgs.Clear();

            OpenFileDialog OpenFile = new OpenFileDialog();
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                imgFileName = OpenFile.FileName;
                oriImg = (Bitmap)Image.FromFile(imgFileName);

                OriImgBox.Image = oriImg;

                PrevButton.Visible = true;
                NextButton.Visible = true;
                ObjDttctProbLabel.Visible = true;

                await GetObjectFromImg();
            }
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            try
            {
                CroppedImgBox.Image = Prev();
                ObjDttctProbLabel.Text = ObjDttctProb();
            }
            catch (Exception)
            {
                Console.WriteLine("Error in function: Prev()");
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            try
            {
                CroppedImgBox.Image = Next();
                ObjDttctProbLabel.Text = ObjDttctProb();
            }
            catch (Exception)
            {
                Console.WriteLine("Error in function: Next()");
            }
        }

        private async Task GetObjectFromImg()
        {
            float probToPass = 0.95f;
            string targetTagName = "people";

            Task<List<Prediction>> Tsk = objdttctAPI.GetPredictionsAsync(oriImg);
            List<Prediction> tmpPredictions = await Tsk;

            ObjDetection obj = new ObjDetection();
            
            foreach(Prediction pred in tmpPredictions)
            {
                if (pred.probability >= probToPass)
                {
                    if (pred.tagName == targetTagName)
                    {
                        predictions.Add(pred);
                        predImgs.Add(obj.GetPredictionBitmapImg(oriImg, pred));
                    }
                }
            }

        }

        private Bitmap Prev()
        {
            index++;
            if (index >= predImgs.Count)
            {
                index = 0;
            }

            return predImgs[index];
        }

        private Bitmap Next()
        {
            index--;
            if ((index < 0) && (predImgs.Count != 0))
            {
                index = predImgs.Count - 1;
            }

            return predImgs[index];
        }

        private string ObjDttctProb()
        {
            string output = $"Probability: {predictions[index].probability * 100}%";
            return output;
        }

    }
}
