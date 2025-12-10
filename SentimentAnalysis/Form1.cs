using SentimentAnalysis.Entities;
using SentimentAnalysis.Services;

namespace SentimentAnalysis
{
    public partial class Form1 : Form
    {
        private SentimentService? _sentimentService;

        // Renk paleti
        private readonly Color PositiveColor = Color.FromArgb(46, 204, 113);   // Yesil
        private readonly Color NegativeColor = Color.FromArgb(231, 76, 60);    // Kirmizi
        private readonly Color NeutralColor = Color.FromArgb(52, 152, 219);    // Mavi
        private readonly Color DarkText = Color.FromArgb(44, 62, 80);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Model yuklenirken kullaniciya bilgi ver
            SetLoadingState(true);

            // Model egitimini arka planda yap
            Task.Run(() =>
            {
                _sentimentService = new SentimentService();

                // UI thread'e geri dön
                this.Invoke(() =>
                {
                    SetLoadingState(false);
                    ShowModelReady();
                });
            });
        }

        private void SetLoadingState(bool isLoading)
        {
            btnAnalyze.Enabled = !isLoading;
            progressBar.Visible = isLoading;

            if (isLoading)
            {
                lblResultIcon.Text = "...";
                lblResult.Text = "Model Yukleniyor...";
                lblResult.ForeColor = NeutralColor;
                lblConfidence.Text = "ML.NET modeli egitiliyor, lutfen bekleyin...";
                lblMetrics.Text = "Yukleniyor...";
            }
        }

        private void ShowModelReady()
        {
            lblResultIcon.Text = "OK";
            lblResult.Text = "Model Hazir!";
            lblResult.ForeColor = PositiveColor;
            lblConfidence.Text = "Metin girerek analiz yapabilirsiniz";

            if (_sentimentService != null)
            {
                lblMetrics.Text = $"Dogruluk: {_sentimentService.Accuracy:P1}  |  " +
                                 $"F1 Skoru: {_sentimentService.F1Score:P1}  |  " +
                                 $"Kesinlik: {_sentimentService.Precision:P1}  |  " +
                                 $"Duyarlilik: {_sentimentService.Recall:P1}";
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            var text = txtInput.Text;

            if (string.IsNullOrWhiteSpace(text))
            {
                ShowWarning("Lutfen analiz edilecek bir metin girin.");
                return;
            }

            if (_sentimentService == null)
            {
                ShowWarning("Model henuz yuklenmedi, lutfen bekleyin.");
                return;
            }

            // Detayli analiz yap
            var result = _sentimentService.AnalyzeDetailed(text);

            // Sonucu göster
            ShowResult(result);
        }

        private void ShowResult(AnalysisResult result)
        {
            if (result.Sentiment == SentimentType.Positive)
            {
                lblResultIcon.Text = ":)";
                lblResult.Text = "Olumlu Duygu";
                lblResult.ForeColor = PositiveColor;
                pnlResult.BackColor = Color.FromArgb(232, 245, 233); // Acik yesil
            }
            else
            {
                lblResultIcon.Text = ":(";
                lblResult.Text = "Olumsuz Duygu";
                lblResult.ForeColor = NegativeColor;
                pnlResult.BackColor = Color.FromArgb(255, 235, 238); // Acik kirmizi
            }

            // Guven skorunu yuzde olarak goster (0-1 arasi degeri yuzdeye cevir)
            double confidencePercent = Math.Min(result.Confidence * 100, 100);
            string confidenceLevel = confidencePercent switch
            {
                >= 70 => "Yuksek",
                >= 50 => "Orta",
                >= 30 => "Dusuk",
                _ => "Belirsiz"
            };

            lblConfidence.Text = $"Guven: {confidenceLevel} ({confidencePercent:F0}%)";
        }

        private void ShowWarning(string message)
        {
            lblResultIcon.Text = "!";
            lblResult.Text = "Uyari";
            lblResult.ForeColor = Color.FromArgb(243, 156, 18); // Turuncu
            lblConfidence.Text = message;
            pnlResult.BackColor = Color.FromArgb(255, 248, 225); // Acik sari
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInput.Clear();
            txtInput.Focus();

            // Sonuc panelini sifirla
            lblResultIcon.Text = "OK";
            lblResult.Text = "Model Hazir!";
            lblResult.ForeColor = PositiveColor;
            lblConfidence.Text = "Metin girerek analiz yapabilirsiniz";
            pnlResult.BackColor = Color.White;
        }
    }
}


