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
using Classification;

namespace CVAssignment20221217124400
{
    public partial class Form1 : Form
    {

        private string predictionKey = "35532dc500874d7a86cf0e18b789bc5a";
        private string predictionUrl = "https://yysprediction.cognitiveservices.azure.com/customvision/v3.0/Prediction/f79b57d5-32a9-4566-949d-b144204c7ae0/detect/iterations/Iteration4/image";
        private string classificationKey = "4ad8c6f72f0b4cf9adee90cd7819cb59";
        private string classificationUrl = "https://southeastasia.api.cognitive.microsoft.com/customvision/v3.0/Prediction/a275c7ce-b51a-4e5a-9725-8a8450d408c6/classify/iterations/Iteration8/image";

        private int index = 0;
        private double objDttctProbToPass = 0.95;
        private string objDttctTargetTagName = "people";
        private double clssProbToTrig = 0.35;
        private string clssTagName = "Negative";

        private TargetAPI objdttctAPI;
        private TargetAPI classAPI;

        private bool haveResult = true;
        private string imgFileName;
        private Bitmap oriImg;
        private List<MaOwnPredModel> predictionResults = new List<MaOwnPredModel>();

        public Form1()
        {
            InitializeComponent();
            Console.WriteLine("Setting up");
            objdttctAPI = new TargetAPI(predictionKey, predictionUrl);
            classAPI = new TargetAPI(classificationKey, classificationUrl);
            PrevButton.Visible = false;
            NextButton.Visible = false;
            CroppedImgNameLabel.Visible = false;
            ObjDttctProbLabel.Visible = false;
            Console.WriteLine("Setted up");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void BrowseImgButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Reset results");
            predictionResults.Clear();

            OpenFileDialog OpenFile = new OpenFileDialog();
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine("Getting data");
                imgFileName = OpenFile.FileName;
                Console.WriteLine("Data got");
                oriImg = (Bitmap)Image.FromFile(imgFileName);

                OriImgBox.Image = oriImg;

                PrevButton.Visible = true;
                NextButton.Visible = true;
                CroppedImgNameLabel.Visible = true;
                ObjDttctProbLabel.Visible = true;
                index = 0;

                Console.WriteLine("Starting object detection");
                await GetObjectFromImg();
                Console.WriteLine("Done object detection");
                GetInfo();
            }
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (predictionResults.Count != 0)
                {
                    index--;
                    if (index < 0)
                    {
                        index = predictionResults.Count - 1;
                    }
                    GetInfo();
                }
            }
            catch (Exception)
            {

            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (predictionResults.Count != 0)
                {
                    index++;
                    if (index >= predictionResults.Count)
                    {
                        index = 0;
                    }
                    GetInfo();
                }
            }
            catch (Exception)
            {

            }
        }

        private async Task GetObjectFromImg()
        {
            List<Prediction> tmpPredictions = new List<Prediction>();

            try
            {
                Console.WriteLine("Object detection: Getting predictions");
                tmpPredictions = await objdttctAPI.GetPredictionsAsync(oriImg);
                Console.WriteLine($"Object detection: Number of predictions: {tmpPredictions.Count}");
                Console.WriteLine("Object detection: Predictions got");
            }catch (Exception)
            {

            }

            ObjDetection obj = new ObjDetection();
            Console.WriteLine("Object detection: Adding results");
            predictionResults = obj.GetMaOwnPredModel(tmpPredictions, oriImg, objDttctTargetTagName, objDttctProbToPass);
            Console.WriteLine($"Object detection: Number of results: {predictionResults.Count}");
            Console.WriteLine("Object detection: Results added");

            if (predictionResults.Count == 0)
            {
                haveResult = false;
                predictionResults.Add(new MaOwnPredModel()
                {
                    Name = "",
                    Image = new Bitmap(1, 1),
                    Probability = 0.0,
                    Triggered = false
                });
            }
            else
            {
                haveResult = true;
                Console.WriteLine("Starting classification");
                await GoClassification();
                Console.WriteLine("Done classification");
            }

            bool triged = false;
            foreach(MaOwnPredModel predModel in predictionResults)
            {
                if (predModel.Triggered == true)
                {
                    triged = true;
                }
            }
            if (triged == true)
            {
                TriggeredIndicatorLabel.Text = "Triggered!";
            }
            else
            {
                TriggeredIndicatorLabel.Text = "Not triggered";
            }
        }

        public async Task GoClassification()
        {

            foreach(MaOwnPredModel model in predictionResults)
            {
                model.ClassificationPredictions = new List<Prediction>();

                try
                {
                    Console.WriteLine("Classification: Getting predictions");
                    List<Prediction> tmpPred = await classAPI.GetPredictionsAsync(model.Image);
                    Console.WriteLine("Classification: Predictions got");
                    if(tmpPred != null)
                    {
                        Console.WriteLine($"Classification: Number of predictions: {tmpPred.Count}");
                        Console.WriteLine("Classification: Adding results");
                        model.ClassificationPredictions = tmpPred;
                        Console.WriteLine("Classification: Results added");
                    }
                }
                catch (Exception)
                {

                }

                Classfction obj = new Classfction();
                Console.WriteLine("Classification: Processing results");
                obj.GetClassificationResult(model, clssTagName, clssProbToTrig);
                Console.WriteLine("Classification: Results processed");
            }

        }

        private void GetInfo()
        {
            string output = "";

            CroppedImgNameLabel.Text = predictionResults[index].Name;
            CroppedImgBox.Image = predictionResults[index].Image;

            if (haveResult == true)
            {
                output = $"Probability: {predictionResults[index].Probability * 100}%";
            }
            else
            {
                output = "No result found";
            }

            ObjDttctProbLabel.Text = output;

        }

    }
}
