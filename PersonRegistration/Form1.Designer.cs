namespace PersonRegistration
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
            this.LblName = new System.Windows.Forms.Label();
            this.LblLastname = new System.Windows.Forms.Label();
            this.LblPersonnr = new System.Windows.Forms.Label();
            this.BoxPersonnr = new System.Windows.Forms.TextBox();
            this.BoxLastname = new System.Windows.Forms.TextBox();
            this.BoxName = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnRegister = new System.Windows.Forms.Button();
            this.GroupBoxRegister = new System.Windows.Forms.GroupBox();
            this.LblResult = new System.Windows.Forms.Label();
            this.TxtBoxPersons = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.GroupBoxRegister.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblName
            // 
            this.LblName.AutoSize = true;
            this.LblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblName.Location = new System.Drawing.Point(6, 36);
            this.LblName.Name = "LblName";
            this.LblName.Size = new System.Drawing.Size(64, 17);
            this.LblName.TabIndex = 0;
            this.LblName.Text = "Förnamn";
            // 
            // LblLastname
            // 
            this.LblLastname.AutoSize = true;
            this.LblLastname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblLastname.Location = new System.Drawing.Point(6, 77);
            this.LblLastname.Name = "LblLastname";
            this.LblLastname.Size = new System.Drawing.Size(73, 17);
            this.LblLastname.TabIndex = 1;
            this.LblLastname.Text = "Efternamn";
            // 
            // LblPersonnr
            // 
            this.LblPersonnr.AutoSize = true;
            this.LblPersonnr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPersonnr.Location = new System.Drawing.Point(6, 119);
            this.LblPersonnr.Name = "LblPersonnr";
            this.LblPersonnr.Size = new System.Drawing.Size(104, 17);
            this.LblPersonnr.TabIndex = 2;
            this.LblPersonnr.Text = "Personnummer";
            // 
            // BoxPersonnr
            // 
            this.BoxPersonnr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxPersonnr.Location = new System.Drawing.Point(140, 116);
            this.BoxPersonnr.MaxLength = 11;
            this.BoxPersonnr.Name = "BoxPersonnr";
            this.BoxPersonnr.Size = new System.Drawing.Size(169, 23);
            this.BoxPersonnr.TabIndex = 3;
            // 
            // BoxLastname
            // 
            this.BoxLastname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxLastname.Location = new System.Drawing.Point(140, 71);
            this.BoxLastname.Name = "BoxLastname";
            this.BoxLastname.Size = new System.Drawing.Size(169, 23);
            this.BoxLastname.TabIndex = 4;
            // 
            // BoxName
            // 
            this.BoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxName.Location = new System.Drawing.Point(140, 33);
            this.BoxName.Name = "BoxName";
            this.BoxName.Size = new System.Drawing.Size(169, 23);
            this.BoxName.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuRegister});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuRegister
            // 
            this.MenuRegister.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuClose});
            this.MenuRegister.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MenuRegister.Name = "MenuRegister";
            this.MenuRegister.Size = new System.Drawing.Size(110, 20);
            this.MenuRegister.Text = "Registrera person";
            // 
            // MenuClose
            // 
            this.MenuClose.Name = "MenuClose";
            this.MenuClose.Size = new System.Drawing.Size(113, 22);
            this.MenuClose.Text = "Avsluta";
            this.MenuClose.Click += new System.EventHandler(this.MenuClose_Click);
            // 
            // BtnRegister
            // 
            this.BtnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRegister.Location = new System.Drawing.Point(140, 162);
            this.BtnRegister.Name = "BtnRegister";
            this.BtnRegister.Size = new System.Drawing.Size(169, 36);
            this.BtnRegister.TabIndex = 9;
            this.BtnRegister.Text = "Registrera";
            this.BtnRegister.UseVisualStyleBackColor = true;
            this.BtnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // GroupBoxRegister
            // 
            this.GroupBoxRegister.Controls.Add(this.BtnRegister);
            this.GroupBoxRegister.Controls.Add(this.LblPersonnr);
            this.GroupBoxRegister.Controls.Add(this.LblLastname);
            this.GroupBoxRegister.Controls.Add(this.LblName);
            this.GroupBoxRegister.Controls.Add(this.BoxLastname);
            this.GroupBoxRegister.Controls.Add(this.BoxPersonnr);
            this.GroupBoxRegister.Controls.Add(this.BoxName);
            this.GroupBoxRegister.Location = new System.Drawing.Point(12, 36);
            this.GroupBoxRegister.Name = "GroupBoxRegister";
            this.GroupBoxRegister.Size = new System.Drawing.Size(333, 217);
            this.GroupBoxRegister.TabIndex = 10;
            this.GroupBoxRegister.TabStop = false;
            this.GroupBoxRegister.Text = "Person";
            // 
            // LblResult
            // 
            this.LblResult.AutoSize = true;
            this.LblResult.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LblResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblResult.Location = new System.Drawing.Point(12, 272);
            this.LblResult.MinimumSize = new System.Drawing.Size(330, 180);
            this.LblResult.Name = "LblResult";
            this.LblResult.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.LblResult.Size = new System.Drawing.Size(330, 180);
            this.LblResult.TabIndex = 11;
            this.LblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtBoxPersons
            // 
            this.TxtBoxPersons.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBoxPersons.Location = new System.Drawing.Point(401, 69);
            this.TxtBoxPersons.Multiline = true;
            this.TxtBoxPersons.Name = "TxtBoxPersons";
            this.TxtBoxPersons.ReadOnly = true;
            this.TxtBoxPersons.Size = new System.Drawing.Size(271, 383);
            this.TxtBoxPersons.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(398, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Registrerare personer";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtBoxPersons);
            this.Controls.Add(this.GroupBoxRegister);
            this.Controls.Add(this.LblResult);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(700, 600);
            this.MinimumSize = new System.Drawing.Size(700, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Person";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.GroupBoxRegister.ResumeLayout(false);
            this.GroupBoxRegister.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblName;
        private System.Windows.Forms.Label LblLastname;
        private System.Windows.Forms.Label LblPersonnr;
        private System.Windows.Forms.TextBox BoxPersonnr;
        private System.Windows.Forms.TextBox BoxLastname;
        private System.Windows.Forms.TextBox BoxName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuRegister;
        private System.Windows.Forms.ToolStripMenuItem MenuClose;
        private System.Windows.Forms.Button BtnRegister;
        private System.Windows.Forms.GroupBox GroupBoxRegister;
        private System.Windows.Forms.Label LblResult;
        private System.Windows.Forms.TextBox TxtBoxPersons;
        private System.Windows.Forms.Label label1;
    }
}
