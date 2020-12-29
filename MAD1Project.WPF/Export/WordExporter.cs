using MAD1Project.WPF.Model;
using NPOI.XWPF.UserModel;
using System.IO;

namespace MAD1Project.WPF.Export
{
    public class WordExporter
    {
        public void Export(string path, AnalysisExport analysisExport)
        {
            XWPFDocument doc = new XWPFDocument();

            SetHeading(doc);
            SetBasicInfo(doc, analysisExport);
            SetAnalysisContent(doc, analysisExport);


            using (FileStream sw = File.Create(path))
            {
                doc.Write(sw);
            }

        }

        private void SetHeading(XWPFDocument document)
        {
            var heading = document.CreateParagraph();
            heading.Alignment = ParagraphAlignment.CENTER;
            var headingRun = heading.CreateRun();
            headingRun.FontSize = 25;
            headingRun.IsBold = true;
            headingRun.SetText("Analýza sítě - Model Small world");

        }

        private void SetBasicInfo(XWPFDocument document, AnalysisExport analysisExport)
        {
            var basicInfoParagraphHeading = document.CreateParagraph();
            basicInfoParagraphHeading.Alignment = ParagraphAlignment.CENTER;

            var basicInfoParagraphHeadingRun = basicInfoParagraphHeading.CreateRun();
            basicInfoParagraphHeadingRun.FontSize = 20;
            basicInfoParagraphHeadingRun.SetText("Vygenerovaný graf");

            var basicInfoParagraphGeneral = document.CreateParagraph();
            basicInfoParagraphGeneral.Alignment = ParagraphAlignment.BOTH;

            var basicInfoParagraphGeneralRun = basicInfoParagraphGeneral.CreateRun();
            basicInfoParagraphGeneralRun.FontSize = 13;
            basicInfoParagraphGeneralRun.AppendText($"Pro následnou analýzu byl vygenerován model grafu typu Small world pomocí algoritmu Watts-Strogatz. Graf byl generován na základě následujících parametrů.");
            basicInfoParagraphGeneralRun.AddCarriageReturn();

            var basicInfoParagraphContent = document.CreateParagraph();

            var basicInfoParagraphContentRun = basicInfoParagraphContent.CreateRun();
            basicInfoParagraphContentRun.FontSize = 13;
            basicInfoParagraphContentRun.AppendText($"Počet vrcholů: {analysisExport.NodeCount}");
            basicInfoParagraphContentRun.AddCarriageReturn();
            basicInfoParagraphContentRun.AppendText($"Parametr P: {analysisExport.ParameterP}");
        }

        private void SetAnalysisContent(XWPFDocument document, AnalysisExport analysisExport)
        {
            var analysisParagraphHeading = document.CreateParagraph();
            analysisParagraphHeading.Alignment = ParagraphAlignment.CENTER;

            var analysisParagraphHeadingRun = analysisParagraphHeading.CreateRun();
            analysisParagraphHeadingRun.FontSize = 20;
            analysisParagraphHeadingRun.SetText("Analýza grafu");

            var analysisParagraphContent = document.CreateParagraph();

            var analysisParagraphContentRun = analysisParagraphContent.CreateRun();
            analysisParagraphContentRun.FontSize = 13;
            analysisParagraphContentRun.AppendText($"Průměr grafu: {analysisExport.GraphMean}");
            analysisParagraphContentRun.AddCarriageReturn();
            analysisParagraphContentRun.AppendText($"Průměrný shlukovací koeficient: {analysisExport.AverageClusteringCieficient}");


            var degreeDistributionGraphParagraphContent = document.CreateParagraph();

            var widthEmus = (int)(600.0 * 9525);
            var heightEmus = (int)(400.0 * 9525);

            var degreeDistributionGraphParagraphContentRun = degreeDistributionGraphParagraphContent.CreateRun();
            degreeDistributionGraphParagraphContentRun.AddPicture(analysisExport.DegreeDistributionGraph, (int)PictureType.PNG, "degreeDistributionGraph", widthEmus, heightEmus);


            var ClusteringEffectDistributionGraphParagraphContent = document.CreateParagraph();

            var ClusteringEffectDistributionGraphParagraphContentRun = ClusteringEffectDistributionGraphParagraphContent.CreateRun();
            ClusteringEffectDistributionGraphParagraphContentRun.AddPicture(analysisExport.ClusteringEffectDistributionGraph, (int)PictureType.PNG, "degreeDistributionGraph", widthEmus, heightEmus);

        }
    }
}
