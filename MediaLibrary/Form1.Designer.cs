namespace MediaLibrary
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GroupBook = new System.Windows.Forms.GroupBox();
            this.BoxBookPages = new System.Windows.Forms.TextBox();
            this.BoxBookTitle = new System.Windows.Forms.TextBox();
            this.LblBookPages = new System.Windows.Forms.Label();
            this.lblBookTitle = new System.Windows.Forms.Label();
            this.BtnRegisterBook = new System.Windows.Forms.Button();
            this.GroupSoundtrack = new System.Windows.Forms.GroupBox();
            this.BoxSoundTime = new System.Windows.Forms.TextBox();
            this.LblSoundTime = new System.Windows.Forms.Label();
            this.LblSoundTitle = new System.Windows.Forms.Label();
            this.BoxSoundTitle = new System.Windows.Forms.TextBox();
            this.BtnRegisterSound = new System.Windows.Forms.Button();
            this.GroupMovie = new System.Windows.Forms.GroupBox();
            this.BoxMovieResolution = new System.Windows.Forms.TextBox();
            this.LblMovieResolution = new System.Windows.Forms.Label();
            this.BoxMovieTime = new System.Windows.Forms.TextBox();
            this.LblMovieTime = new System.Windows.Forms.Label();
            this.LblMovieTitle = new System.Windows.Forms.Label();
            this.BoxMovieTitle = new System.Windows.Forms.TextBox();
            this.BtnRegisterMovie = new System.Windows.Forms.Button();
            this.TextResult = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GroupBook.SuspendLayout();
            this.GroupSoundtrack.SuspendLayout();
            this.GroupMovie.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBook
            // 
            this.GroupBook.Controls.Add(this.BoxBookPages);
            this.GroupBook.Controls.Add(this.BoxBookTitle);
            this.GroupBook.Controls.Add(this.LblBookPages);
            this.GroupBook.Controls.Add(this.lblBookTitle);
            this.GroupBook.Controls.Add(this.BtnRegisterBook);
            this.GroupBook.Location = new System.Drawing.Point(16, 15);
            this.GroupBook.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBook.Name = "GroupBook";
            this.GroupBook.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBook.Size = new System.Drawing.Size(285, 179);
            this.GroupBook.TabIndex = 0;
            this.GroupBook.TabStop = false;
            this.GroupBook.Text = "Bok";
            // 
            // BoxBookPages
            // 
            this.BoxBookPages.Location = new System.Drawing.Point(99, 65);
            this.BoxBookPages.Margin = new System.Windows.Forms.Padding(4);
            this.BoxBookPages.Name = "BoxBookPages";
            this.BoxBookPages.Size = new System.Drawing.Size(163, 22);
            this.BoxBookPages.TabIndex = 4;
            // 
            // BoxBookTitle
            // 
            this.BoxBookTitle.Location = new System.Drawing.Point(99, 32);
            this.BoxBookTitle.Margin = new System.Windows.Forms.Padding(4);
            this.BoxBookTitle.Name = "BoxBookTitle";
            this.BoxBookTitle.Size = new System.Drawing.Size(163, 22);
            this.BoxBookTitle.TabIndex = 3;
            // 
            // LblBookPages
            // 
            this.LblBookPages.AutoSize = true;
            this.LblBookPages.Location = new System.Drawing.Point(8, 71);
            this.LblBookPages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblBookPages.Name = "LblBookPages";
            this.LblBookPages.Size = new System.Drawing.Size(72, 16);
            this.LblBookPages.TabIndex = 2;
            this.LblBookPages.Text = "Antal Sidor";
            // 
            // lblBookTitle
            // 
            this.lblBookTitle.AutoSize = true;
            this.lblBookTitle.Location = new System.Drawing.Point(8, 38);
            this.lblBookTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBookTitle.Name = "lblBookTitle";
            this.lblBookTitle.Size = new System.Drawing.Size(33, 16);
            this.lblBookTitle.TabIndex = 1;
            this.lblBookTitle.Text = "Titel";
            // 
            // BtnRegisterBook
            // 
            this.BtnRegisterBook.Location = new System.Drawing.Point(99, 131);
            this.BtnRegisterBook.Margin = new System.Windows.Forms.Padding(4);
            this.BtnRegisterBook.Name = "BtnRegisterBook";
            this.BtnRegisterBook.Size = new System.Drawing.Size(163, 37);
            this.BtnRegisterBook.TabIndex = 0;
            this.BtnRegisterBook.Text = "Registrera bok";
            this.BtnRegisterBook.UseVisualStyleBackColor = true;
            this.BtnRegisterBook.Click += new System.EventHandler(this.BtnRegisterBook_Click);
            // 
            // GroupSoundtrack
            // 
            this.GroupSoundtrack.Controls.Add(this.BoxSoundTime);
            this.GroupSoundtrack.Controls.Add(this.LblSoundTime);
            this.GroupSoundtrack.Controls.Add(this.LblSoundTitle);
            this.GroupSoundtrack.Controls.Add(this.BoxSoundTitle);
            this.GroupSoundtrack.Controls.Add(this.BtnRegisterSound);
            this.GroupSoundtrack.Location = new System.Drawing.Point(309, 15);
            this.GroupSoundtrack.Margin = new System.Windows.Forms.Padding(4);
            this.GroupSoundtrack.Name = "GroupSoundtrack";
            this.GroupSoundtrack.Padding = new System.Windows.Forms.Padding(4);
            this.GroupSoundtrack.Size = new System.Drawing.Size(253, 179);
            this.GroupSoundtrack.TabIndex = 1;
            this.GroupSoundtrack.TabStop = false;
            this.GroupSoundtrack.Text = "Ljudspår";
            // 
            // BoxSoundTime
            // 
            this.BoxSoundTime.Location = new System.Drawing.Point(72, 65);
            this.BoxSoundTime.Margin = new System.Windows.Forms.Padding(4);
            this.BoxSoundTime.Name = "BoxSoundTime";
            this.BoxSoundTime.Size = new System.Drawing.Size(159, 22);
            this.BoxSoundTime.TabIndex = 4;
            // 
            // LblSoundTime
            // 
            this.LblSoundTime.AutoSize = true;
            this.LblSoundTime.Location = new System.Drawing.Point(8, 68);
            this.LblSoundTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblSoundTime.Name = "LblSoundTime";
            this.LblSoundTime.Size = new System.Drawing.Size(49, 16);
            this.LblSoundTime.TabIndex = 3;
            this.LblSoundTime.Text = "Speltid";
            // 
            // LblSoundTitle
            // 
            this.LblSoundTitle.AutoSize = true;
            this.LblSoundTitle.Location = new System.Drawing.Point(8, 32);
            this.LblSoundTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblSoundTitle.Name = "LblSoundTitle";
            this.LblSoundTitle.Size = new System.Drawing.Size(33, 16);
            this.LblSoundTitle.TabIndex = 2;
            this.LblSoundTitle.Text = "Titel";
            // 
            // BoxSoundTitle
            // 
            this.BoxSoundTitle.Location = new System.Drawing.Point(72, 29);
            this.BoxSoundTitle.Margin = new System.Windows.Forms.Padding(4);
            this.BoxSoundTitle.Name = "BoxSoundTitle";
            this.BoxSoundTitle.Size = new System.Drawing.Size(159, 22);
            this.BoxSoundTitle.TabIndex = 1;
            // 
            // BtnRegisterSound
            // 
            this.BtnRegisterSound.Location = new System.Drawing.Point(72, 131);
            this.BtnRegisterSound.Margin = new System.Windows.Forms.Padding(4);
            this.BtnRegisterSound.Name = "BtnRegisterSound";
            this.BtnRegisterSound.Size = new System.Drawing.Size(159, 37);
            this.BtnRegisterSound.TabIndex = 0;
            this.BtnRegisterSound.Text = "Registrera ljudspår";
            this.BtnRegisterSound.UseVisualStyleBackColor = true;
            this.BtnRegisterSound.Click += new System.EventHandler(this.BtnRegisterSound_Click);
            // 
            // GroupMovie
            // 
            this.GroupMovie.Controls.Add(this.BoxMovieResolution);
            this.GroupMovie.Controls.Add(this.LblMovieResolution);
            this.GroupMovie.Controls.Add(this.BoxMovieTime);
            this.GroupMovie.Controls.Add(this.LblMovieTime);
            this.GroupMovie.Controls.Add(this.LblMovieTitle);
            this.GroupMovie.Controls.Add(this.BoxMovieTitle);
            this.GroupMovie.Controls.Add(this.BtnRegisterMovie);
            this.GroupMovie.Location = new System.Drawing.Point(570, 15);
            this.GroupMovie.Margin = new System.Windows.Forms.Padding(4);
            this.GroupMovie.Name = "GroupMovie";
            this.GroupMovie.Padding = new System.Windows.Forms.Padding(4);
            this.GroupMovie.Size = new System.Drawing.Size(288, 179);
            this.GroupMovie.TabIndex = 2;
            this.GroupMovie.TabStop = false;
            this.GroupMovie.Text = "Film";
            // 
            // BoxMovieResolution
            // 
            this.BoxMovieResolution.BackColor = System.Drawing.SystemColors.Window;
            this.BoxMovieResolution.ForeColor = System.Drawing.Color.Gray;
            this.BoxMovieResolution.Location = new System.Drawing.Point(101, 94);
            this.BoxMovieResolution.Name = "BoxMovieResolution";
            this.BoxMovieResolution.Size = new System.Drawing.Size(164, 22);
            this.BoxMovieResolution.TabIndex = 6;
            this.BoxMovieResolution.Text = "ex: HD";
            this.BoxMovieResolution.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BoxMovieResolution_KeyDown);
            // 
            // LblMovieResolution
            // 
            this.LblMovieResolution.AutoSize = true;
            this.LblMovieResolution.Location = new System.Drawing.Point(6, 97);
            this.LblMovieResolution.Name = "LblMovieResolution";
            this.LblMovieResolution.Size = new System.Drawing.Size(76, 16);
            this.LblMovieResolution.TabIndex = 5;
            this.LblMovieResolution.Text = "Upplösning";
            // 
            // BoxMovieTime
            // 
            this.BoxMovieTime.Location = new System.Drawing.Point(99, 59);
            this.BoxMovieTime.Margin = new System.Windows.Forms.Padding(4);
            this.BoxMovieTime.Name = "BoxMovieTime";
            this.BoxMovieTime.Size = new System.Drawing.Size(166, 22);
            this.BoxMovieTime.TabIndex = 4;
            // 
            // LblMovieTime
            // 
            this.LblMovieTime.AutoSize = true;
            this.LblMovieTime.Location = new System.Drawing.Point(6, 65);
            this.LblMovieTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblMovieTime.Name = "LblMovieTime";
            this.LblMovieTime.Size = new System.Drawing.Size(49, 16);
            this.LblMovieTime.TabIndex = 3;
            this.LblMovieTime.Text = "Speltid";
            // 
            // LblMovieTitle
            // 
            this.LblMovieTitle.AutoSize = true;
            this.LblMovieTitle.Location = new System.Drawing.Point(8, 32);
            this.LblMovieTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblMovieTitle.Name = "LblMovieTitle";
            this.LblMovieTitle.Size = new System.Drawing.Size(33, 16);
            this.LblMovieTitle.TabIndex = 2;
            this.LblMovieTitle.Text = "Titel";
            // 
            // BoxMovieTitle
            // 
            this.BoxMovieTitle.Location = new System.Drawing.Point(99, 26);
            this.BoxMovieTitle.Margin = new System.Windows.Forms.Padding(4);
            this.BoxMovieTitle.Name = "BoxMovieTitle";
            this.BoxMovieTitle.Size = new System.Drawing.Size(166, 22);
            this.BoxMovieTitle.TabIndex = 1;
            // 
            // BtnRegisterMovie
            // 
            this.BtnRegisterMovie.Location = new System.Drawing.Point(99, 131);
            this.BtnRegisterMovie.Margin = new System.Windows.Forms.Padding(4);
            this.BtnRegisterMovie.Name = "BtnRegisterMovie";
            this.BtnRegisterMovie.Size = new System.Drawing.Size(166, 40);
            this.BtnRegisterMovie.TabIndex = 0;
            this.BtnRegisterMovie.Text = "Registrera film";
            this.BtnRegisterMovie.UseVisualStyleBackColor = true;
            this.BtnRegisterMovie.Click += new System.EventHandler(this.BtnRegisterMovie_Click);
            // 
            // TextResult
            // 
            this.TextResult.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TextResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextResult.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TextResult.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextResult.Location = new System.Drawing.Point(16, 267);
            this.TextResult.Multiline = true;
            this.TextResult.Name = "TextResult";
            this.TextResult.ReadOnly = true;
            this.TextResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextResult.Size = new System.Drawing.Size(842, 268);
            this.TextResult.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(365, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Bibliotek";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 582);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextResult);
            this.Controls.Add(this.GroupMovie);
            this.Controls.Add(this.GroupSoundtrack);
            this.Controls.Add(this.GroupBook);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(895, 621);
            this.Name = "Form1";
            this.Text = "MediaBibliotek";
            this.GroupBook.ResumeLayout(false);
            this.GroupBook.PerformLayout();
            this.GroupSoundtrack.ResumeLayout(false);
            this.GroupSoundtrack.PerformLayout();
            this.GroupMovie.ResumeLayout(false);
            this.GroupMovie.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBook;
        private System.Windows.Forms.TextBox BoxBookPages;
        private System.Windows.Forms.TextBox BoxBookTitle;
        private System.Windows.Forms.Label LblBookPages;
        private System.Windows.Forms.Label lblBookTitle;
        private System.Windows.Forms.Button BtnRegisterBook;
        private System.Windows.Forms.GroupBox GroupSoundtrack;
        private System.Windows.Forms.TextBox BoxSoundTime;
        private System.Windows.Forms.Label LblSoundTime;
        private System.Windows.Forms.Label LblSoundTitle;
        private System.Windows.Forms.TextBox BoxSoundTitle;
        private System.Windows.Forms.Button BtnRegisterSound;
        private System.Windows.Forms.GroupBox GroupMovie;
        private System.Windows.Forms.TextBox BoxMovieTime;
        private System.Windows.Forms.Label LblMovieTime;
        private System.Windows.Forms.Label LblMovieTitle;
        private System.Windows.Forms.TextBox BoxMovieTitle;
        private System.Windows.Forms.Button BtnRegisterMovie;
        private System.Windows.Forms.TextBox TextResult;
        private System.Windows.Forms.Label LblMovieResolution;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox BoxMovieResolution;
    }
}

