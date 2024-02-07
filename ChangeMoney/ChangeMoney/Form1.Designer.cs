namespace ChangeMoney
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
            //this.components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(800, 450);
            //this.Text = "Form1";
                        this.LblPrice = new System.Windows.Forms.Label();
            this.LblPaid = new System.Windows.Forms.Label();
            this.LblResult = new System.Windows.Forms.Label();
            this.BtnRun = new System.Windows.Forms.Button();
            this.BtnEnd = new System.Windows.Forms.Button();
            this.LblFinalResult = new System.Windows.Forms.Label();
            this.NrPrice = new System.Windows.Forms.NumericUpDown();
            this.NrPaid = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.NrPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NrPaid)).BeginInit();
            this.SuspendLayout();
            // 
            // LblPrice
            // 
            this.LblPrice.AutoSize = true;
            this.LblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPrice.Location = new System.Drawing.Point(26, 26);
            this.LblPrice.Name = "LblPrice";
            this.LblPrice.Size = new System.Drawing.Size(76, 20);
            this.LblPrice.TabIndex = 0;
            this.LblPrice.Text = "Ange pris";
            // 
            // LblPaid
            // 
            this.LblPaid.AutoSize = true;
            this.LblPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPaid.Location = new System.Drawing.Point(26, 73);
            this.LblPaid.Name = "LblPaid";
            this.LblPaid.Size = new System.Drawing.Size(51, 20);
            this.LblPaid.TabIndex = 1;
            this.LblPaid.Text = "Betalt";
            // 
            // LblResult
            // 
            this.LblResult.AutoSize = true;
            this.LblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblResult.Location = new System.Drawing.Point(26, 112);
            this.LblResult.Margin = new System.Windows.Forms.Padding(0);
            this.LblResult.Name = "LblResult";
            this.LblResult.Size = new System.Drawing.Size(105, 20);
            this.LblResult.TabIndex = 4;
            this.LblResult.Text = "Växel tillbaka:";
            // 
            // BtnRun
            // 
            this.BtnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRun.Location = new System.Drawing.Point(195, 305);
            this.BtnRun.Name = "BtnRun";
            this.BtnRun.Size = new System.Drawing.Size(132, 44);
            this.BtnRun.TabIndex = 5;
            this.BtnRun.Text = "Kör";
            this.BtnRun.UseVisualStyleBackColor = true;
            this.BtnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // BtnEnd
            // 
            this.BtnEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEnd.Location = new System.Drawing.Point(30, 305);
            this.BtnEnd.Name = "BtnEnd";
            this.BtnEnd.Size = new System.Drawing.Size(124, 44);
            this.BtnEnd.TabIndex = 6;
            this.BtnEnd.Text = "Avsluta";
            this.BtnEnd.UseVisualStyleBackColor = true;
            this.BtnEnd.Click += new System.EventHandler(this.BtnEnd_Click);
            // 
            // LblFinalResult
            // 
            this.LblFinalResult.AutoSize = true;
            this.LblFinalResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.LblFinalResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblFinalResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFinalResult.Location = new System.Drawing.Point(27, 143);
            this.LblFinalResult.MinimumSize = new System.Drawing.Size(300, 150);
            this.LblFinalResult.Name = "LblFinalResult";
            this.LblFinalResult.Size = new System.Drawing.Size(300, 150);
            this.LblFinalResult.TabIndex = 7;
            // 
            // NrPrice
            // 
            this.NrPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NrPrice.Location = new System.Drawing.Point(145, 26);
            this.NrPrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NrPrice.Name = "NrPrice";
            this.NrPrice.Size = new System.Drawing.Size(120, 24);
            this.NrPrice.TabIndex = 8;
            this.NrPrice.ValueChanged += new System.EventHandler(this.NrPrice_ValueChanged);
            // 
            // NrPaid
            // 
            this.NrPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NrPaid.Location = new System.Drawing.Point(145, 69);
            this.NrPaid.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NrPaid.Name = "NrPaid";
            this.NrPaid.Size = new System.Drawing.Size(120, 24);
            this.NrPaid.TabIndex = 9;
            this.NrPaid.ValueChanged += new System.EventHandler(this.NrPaid_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.NrPaid);
            this.Controls.Add(this.NrPrice);
            this.Controls.Add(this.LblFinalResult);
            this.Controls.Add(this.BtnEnd);
            this.Controls.Add(this.BtnRun);
            this.Controls.Add(this.LblResult);
            this.Controls.Add(this.LblPaid);
            this.Controls.Add(this.LblPrice);
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kassa System";
            ((System.ComponentModel.ISupportInitialize)(this.NrPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NrPaid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion


        private System.Windows.Forms.Label LblPrice;
        private System.Windows.Forms.Label LblPaid;
        private System.Windows.Forms.Label LblResult;
        private System.Windows.Forms.Button BtnRun;
        private System.Windows.Forms.Button BtnEnd;
        private System.Windows.Forms.Label LblFinalResult;
        private System.Windows.Forms.NumericUpDown NrPrice;
        private System.Windows.Forms.NumericUpDown NrPaid;
    }
}
