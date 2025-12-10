namespace SentimentAnalysis
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            pnlMain = new Panel();
            lblInputTitle = new Label();
            txtInput = new TextBox();
            btnAnalyze = new Button();
            btnClear = new Button();
            progressBar = new ProgressBar();
            pnlResult = new Panel();
            lblResultIcon = new Label();
            lblResult = new Label();
            lblConfidence = new Label();
            pnlMetrics = new Panel();
            lblMetricsTitle = new Label();
            lblMetrics = new Label();
            pnlHeader.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlResult.SuspendLayout();
            pnlMetrics.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(52, 73, 94);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(3, 4, 3, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(800, 127);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Emoji", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(23, 19);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(275, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Duygu Analizi";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 11F);
            lblSubtitle.ForeColor = Color.FromArgb(189, 195, 199);
            lblSubtitle.Location = new Point(27, 82);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(316, 20);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "ML.NET ile Turkce Metin Duygu Analizi Sistemi";
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(236, 240, 241);
            pnlMain.Controls.Add(lblInputTitle);
            pnlMain.Controls.Add(txtInput);
            pnlMain.Controls.Add(btnAnalyze);
            pnlMain.Controls.Add(btnClear);
            pnlMain.Controls.Add(progressBar);
            pnlMain.Dock = DockStyle.Top;
            pnlMain.Location = new Point(0, 127);
            pnlMain.Margin = new Padding(3, 4, 3, 4);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(23, 25, 23, 25);
            pnlMain.Size = new Size(800, 279);
            pnlMain.TabIndex = 1;
            // 
            // lblInputTitle
            // 
            lblInputTitle.AutoSize = true;
            lblInputTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblInputTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblInputTitle.Location = new Point(27, 25);
            lblInputTitle.Name = "lblInputTitle";
            lblInputTitle.Size = new Size(195, 21);
            lblInputTitle.TabIndex = 0;
            lblInputTitle.Text = "Analiz Edilecek Metin:";
            // 
            // txtInput
            // 
            txtInput.BackColor = Color.White;
            txtInput.BorderStyle = BorderStyle.FixedSingle;
            txtInput.Font = new Font("Segoe UI", 12F);
            txtInput.ForeColor = Color.FromArgb(44, 62, 80);
            txtInput.Location = new Point(27, 63);
            txtInput.Margin = new Padding(3, 4, 3, 4);
            txtInput.Multiline = true;
            txtInput.Name = "txtInput";
            txtInput.PlaceholderText = "Buraya analiz etmek istediginiz metni yazin...";
            txtInput.ScrollBars = ScrollBars.Vertical;
            txtInput.Size = new Size(745, 126);
            txtInput.TabIndex = 1;
            // 
            // btnAnalyze
            // 
            btnAnalyze.BackColor = Color.FromArgb(46, 204, 113);
            btnAnalyze.Cursor = Cursors.Hand;
            btnAnalyze.FlatAppearance.BorderSize = 0;
            btnAnalyze.FlatStyle = FlatStyle.Flat;
            btnAnalyze.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            btnAnalyze.ForeColor = Color.White;
            btnAnalyze.Location = new Point(27, 209);
            btnAnalyze.Margin = new Padding(3, 4, 3, 4);
            btnAnalyze.Name = "btnAnalyze";
            btnAnalyze.Size = new Size(206, 57);
            btnAnalyze.TabIndex = 2;
            btnAnalyze.Text = "Analiz Et";
            btnAnalyze.UseVisualStyleBackColor = false;
            btnAnalyze.Click += btnAnalyze_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(231, 76, 60);
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(251, 209);
            btnClear.Margin = new Padding(3, 4, 3, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(160, 57);
            btnClear.TabIndex = 3;
            btnClear.Text = "Temizle";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(434, 222);
            progressBar.Margin = new Padding(3, 4, 3, 4);
            progressBar.MarqueeAnimationSpeed = 30;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(338, 32);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 4;
            progressBar.Visible = false;
            // 
            // pnlResult
            // 
            pnlResult.BackColor = Color.White;
            pnlResult.Controls.Add(lblResultIcon);
            pnlResult.Controls.Add(lblResult);
            pnlResult.Controls.Add(lblConfidence);
            pnlResult.Dock = DockStyle.Top;
            pnlResult.Location = new Point(0, 406);
            pnlResult.Margin = new Padding(3, 4, 3, 4);
            pnlResult.Name = "pnlResult";
            pnlResult.Padding = new Padding(23, 25, 23, 25);
            pnlResult.Size = new Size(800, 165);
            pnlResult.TabIndex = 2;
            // 
            // lblResultIcon
            // 
            lblResultIcon.Font = new Font("Segoe UI", 48F);
            lblResultIcon.Location = new Point(27, 25);
            lblResultIcon.Name = "lblResultIcon";
            lblResultIcon.Size = new Size(114, 114);
            lblResultIcon.TabIndex = 0;
            lblResultIcon.Text = "...";
            lblResultIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblResult.ForeColor = Color.FromArgb(44, 62, 80);
            lblResult.Location = new Point(149, 38);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(219, 32);
            lblResult.TabIndex = 1;
            lblResult.Text = "Model Yukleniyor...";
            // 
            // lblConfidence
            // 
            lblConfidence.AutoSize = true;
            lblConfidence.Font = new Font("Segoe UI", 12F);
            lblConfidence.ForeColor = Color.FromArgb(127, 140, 141);
            lblConfidence.Location = new Point(151, 89);
            lblConfidence.Name = "lblConfidence";
            lblConfidence.Size = new Size(260, 21);
            lblConfidence.TabIndex = 2;
            lblConfidence.Text = "Lutfen model yuklenmesini bekleyin";
            // 
            // pnlMetrics
            // 
            pnlMetrics.BackColor = Color.FromArgb(44, 62, 80);
            pnlMetrics.Controls.Add(lblMetricsTitle);
            pnlMetrics.Controls.Add(lblMetrics);
            pnlMetrics.Dock = DockStyle.Fill;
            pnlMetrics.Location = new Point(0, 571);
            pnlMetrics.Margin = new Padding(3, 4, 3, 4);
            pnlMetrics.Name = "pnlMetrics";
            pnlMetrics.Padding = new Padding(23, 25, 23, 25);
            pnlMetrics.Size = new Size(800, 126);
            pnlMetrics.TabIndex = 3;
            // 
            // lblMetricsTitle
            // 
            lblMetricsTitle.AutoSize = true;
            lblMetricsTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblMetricsTitle.ForeColor = Color.FromArgb(46, 204, 113);
            lblMetricsTitle.Location = new Point(27, 19);
            lblMetricsTitle.Name = "lblMetricsTitle";
            lblMetricsTitle.Size = new Size(165, 20);
            lblMetricsTitle.TabIndex = 0;
            lblMetricsTitle.Text = "Model Performansi";
            // 
            // lblMetrics
            // 
            lblMetrics.AutoSize = true;
            lblMetrics.Font = new Font("Consolas", 10F);
            lblMetrics.ForeColor = Color.FromArgb(189, 195, 199);
            lblMetrics.Location = new Point(27, 57);
            lblMetrics.Name = "lblMetrics";
            lblMetrics.Size = new Size(112, 17);
            lblMetrics.TabIndex = 1;
            lblMetrics.Text = "Yukleniyor...";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            ClientSize = new Size(800, 697);
            Controls.Add(pnlMetrics);
            Controls.Add(pnlResult);
            Controls.Add(pnlMain);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Duygu Analizi - ML.NET";
            Load += Form1_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            pnlResult.ResumeLayout(false);
            pnlResult.PerformLayout();
            pnlMetrics.ResumeLayout(false);
            pnlMetrics.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlMain;
        private Label lblInputTitle;
        private TextBox txtInput;
        private Button btnAnalyze;
        private Button btnClear;
        private ProgressBar progressBar;
        private Panel pnlResult;
        private Label lblResultIcon;
        private Label lblResult;
        private Label lblConfidence;
        private Panel pnlMetrics;
        private Label lblMetricsTitle;
        private Label lblMetrics;
    }
}
