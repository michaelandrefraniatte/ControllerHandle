namespace Mouse
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.lbSource = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbNumKeyboards = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbHandle = new System.Windows.Forms.Label();
            this.lLastX = new System.Windows.Forms.Label();
            this.lLastY = new System.Windows.Forms.Label();
            this.ulButtons = new System.Windows.Forms.Label();
            this.ulExtraInformation = new System.Windows.Forms.Label();
            this.usButtonData = new System.Windows.Forms.Label();
            this.usButtonFlags = new System.Windows.Forms.Label();
            this.usFlags = new System.Windows.Forms.Label();
            this.gbDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(234, 339);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(637, 34);
            this.textBox1.TabIndex = 31;
            // 
            // gbDetails
            // 
            this.gbDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetails.Controls.Add(this.lbSource);
            this.gbDetails.Controls.Add(this.label8);
            this.gbDetails.Controls.Add(this.lbDescription);
            this.gbDetails.Controls.Add(this.lbName);
            this.gbDetails.Controls.Add(this.label6);
            this.gbDetails.Controls.Add(this.label1);
            this.gbDetails.Controls.Add(this.lbType);
            this.gbDetails.Controls.Add(this.label2);
            this.gbDetails.Controls.Add(this.lbNumKeyboards);
            this.gbDetails.Controls.Add(this.label4);
            this.gbDetails.Controls.Add(this.label3);
            this.gbDetails.Controls.Add(this.lbHandle);
            this.gbDetails.Location = new System.Drawing.Point(22, 147);
            this.gbDetails.Margin = new System.Windows.Forms.Padding(4);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Padding = new System.Windows.Forms.Padding(4);
            this.gbDetails.Size = new System.Drawing.Size(1044, 172);
            this.gbDetails.TabIndex = 29;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Device details";
            // 
            // lbSource
            // 
            this.lbSource.AutoSize = true;
            this.lbSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSource.Location = new System.Drawing.Point(73, 133);
            this.lbSource.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSource.Name = "lbSource";
            this.lbSource.Size = new System.Drawing.Size(0, 17);
            this.lbSource.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 133);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 16);
            this.label8.TabIndex = 23;
            this.label8.Text = "Source:";
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescription.Location = new System.Drawing.Point(113, 103);
            this.lbDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(0, 17);
            this.lbDescription.TabIndex = 22;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(113, 74);
            this.lbName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(0, 17);
            this.lbName.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "# Mouses:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Handle:";
            // 
            // lbType
            // 
            this.lbType.AutoSize = true;
            this.lbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbType.Location = new System.Drawing.Point(505, 22);
            this.lbType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(0, 17);
            this.lbType.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(451, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Type:";
            // 
            // lbNumKeyboards
            // 
            this.lbNumKeyboards.AutoSize = true;
            this.lbNumKeyboards.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumKeyboards.Location = new System.Drawing.Point(111, 48);
            this.lbNumKeyboards.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNumKeyboards.Name = "lbNumKeyboards";
            this.lbNumKeyboards.Size = new System.Drawing.Size(0, 17);
            this.lbNumKeyboards.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 103);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Device Desc:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name:";
            // 
            // lbHandle
            // 
            this.lbHandle.AutoSize = true;
            this.lbHandle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHandle.Location = new System.Drawing.Point(111, 22);
            this.lbHandle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHandle.Name = "lbHandle";
            this.lbHandle.Size = new System.Drawing.Size(0, 17);
            this.lbHandle.TabIndex = 16;
            // 
            // lLastX
            // 
            this.lLastX.AutoSize = true;
            this.lLastX.Location = new System.Drawing.Point(19, 23);
            this.lLastX.Name = "lLastX";
            this.lLastX.Size = new System.Drawing.Size(43, 16);
            this.lLastX.TabIndex = 32;
            this.lLastX.Text = "lLastX";
            // 
            // lLastY
            // 
            this.lLastY.AutoSize = true;
            this.lLastY.Location = new System.Drawing.Point(19, 55);
            this.lLastY.Name = "lLastY";
            this.lLastY.Size = new System.Drawing.Size(44, 16);
            this.lLastY.TabIndex = 33;
            this.lLastY.Text = "lLastY";
            // 
            // ulButtons
            // 
            this.ulButtons.AutoSize = true;
            this.ulButtons.Location = new System.Drawing.Point(18, 88);
            this.ulButtons.Name = "ulButtons";
            this.ulButtons.Size = new System.Drawing.Size(61, 16);
            this.ulButtons.TabIndex = 34;
            this.ulButtons.Text = "ulButtons";
            // 
            // ulExtraInformation
            // 
            this.ulExtraInformation.AutoSize = true;
            this.ulExtraInformation.Location = new System.Drawing.Point(348, 23);
            this.ulExtraInformation.Name = "ulExtraInformation";
            this.ulExtraInformation.Size = new System.Drawing.Size(112, 16);
            this.ulExtraInformation.TabIndex = 35;
            this.ulExtraInformation.Text = "ulExtraInformation";
            // 
            // usButtonData
            // 
            this.usButtonData.AutoSize = true;
            this.usButtonData.Location = new System.Drawing.Point(348, 55);
            this.usButtonData.Name = "usButtonData";
            this.usButtonData.Size = new System.Drawing.Size(87, 16);
            this.usButtonData.TabIndex = 36;
            this.usButtonData.Text = "usButtonData";
            // 
            // usButtonFlags
            // 
            this.usButtonFlags.AutoSize = true;
            this.usButtonFlags.Location = new System.Drawing.Point(348, 88);
            this.usButtonFlags.Name = "usButtonFlags";
            this.usButtonFlags.Size = new System.Drawing.Size(92, 16);
            this.usButtonFlags.TabIndex = 37;
            this.usButtonFlags.Text = "usButtonFlags";
            // 
            // usFlags
            // 
            this.usFlags.AutoSize = true;
            this.usFlags.Location = new System.Drawing.Point(704, 23);
            this.usFlags.Name = "usFlags";
            this.usFlags.Size = new System.Drawing.Size(55, 16);
            this.usFlags.TabIndex = 38;
            this.usFlags.Text = "usFlags";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 398);
            this.Controls.Add(this.usFlags);
            this.Controls.Add(this.usButtonFlags);
            this.Controls.Add(this.usButtonData);
            this.Controls.Add(this.ulExtraInformation);
            this.Controls.Add(this.ulButtons);
            this.Controls.Add(this.lLastY);
            this.Controls.Add(this.lLastX);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.gbDetails);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Label lbSource;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbNumKeyboards;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbHandle;
        private System.Windows.Forms.Label lLastX;
        private System.Windows.Forms.Label lLastY;
        private System.Windows.Forms.Label ulButtons;
        private System.Windows.Forms.Label ulExtraInformation;
        private System.Windows.Forms.Label usButtonData;
        private System.Windows.Forms.Label usButtonFlags;
        private System.Windows.Forms.Label usFlags;
    }
}

