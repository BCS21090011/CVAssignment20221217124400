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
using CVAssignment20221217124400.Models;
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
        private List<MaOwnPredModel> predictionResults = new List<MaOwnPredModel>();

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
            predictionResults.Clear();

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

            }
        }

        private async Task GetObjectFromImg()
        {
            double probToPass = 0.95;
            string targetTagName = "people";

            MyPredictionModel predModel = await objdttctAPI.GetPredictionsAsync(oriImg);
            List<Prediction> tmpPredictions = predModel.predictions;

            ObjDetection obj = new ObjDetection();
            
            foreach(Prediction pred in tmpPredictions)
            {
                if (pred.probability >= probToPass)
                {
                    if (pred.tagName == targetTagName)
                    {
                        predictionResults.Add(new MaOwnPredModel()
                        {
                            Image = obj.GetPredictionBitmapImg(oriImg, pred),
                            Probability = pred.probability
                        });
                    }
                }
            }

        }

        private Bitmap Prev()
        {
            index++;
            if (index >= predictionResults.Count)
            {
                index = 0;
            }

            return predictionResults[index].Image;
        }

        private Bitmap Next()
        {
            index--;
            if ((index < 0) && (predictionResults.Count != 0))
            {
                index = predictionResults.Count - 1;
            }

            return predictionResults[index].Image;
        }

        private string ObjDttctProb()
        {
            string output = $"Probability: {predictionResults[index].Probability * 100}%";
            return output;
        }

    }
}
