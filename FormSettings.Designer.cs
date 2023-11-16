namespace AT2Recycle
{
    partial class FormSettings
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
            this.groupBoxNavigationButton = new System.Windows.Forms.GroupBox();
            this.radioButtonOptionHide = new System.Windows.Forms.RadioButton();
            this.radioButtonOptionDisable = new System.Windows.Forms.RadioButton();
            this.radioButtonOptionNone = new System.Windows.Forms.RadioButton();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxNavigationButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxNavigationButton
            // 
            this.groupBoxNavigationButton.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxNavigationButton.Controls.Add(this.radioButtonOptionHide);
            this.groupBoxNavigationButton.Controls.Add(this.radioButtonOptionDisable);
            this.groupBoxNavigationButton.Controls.Add(this.radioButtonOptionNone);
            this.groupBoxNavigationButton.Location = new System.Drawing.Point(12, 12);
            this.groupBoxNavigationButton.Name = "groupBoxNavigationButton";
            this.groupBoxNavigationButton.Size = new System.Drawing.Size(361, 99);
            this.groupBoxNavigationButton.TabIndex = 0;
            this.groupBoxNavigationButton.TabStop = false;
            this.groupBoxNavigationButton.Text = "Navigation buttons";
            // 
            // radioButtonOptionHide
            // 
            this.radioButtonOptionHide.AutoSize = true;
            this.radioButtonOptionHide.Location = new System.Drawing.Point(6, 72);
            this.radioButtonOptionHide.Name = "radioButtonOptionHide";
            this.radioButtonOptionHide.Size = new System.Drawing.Size(156, 19);
            this.radioButtonOptionHide.TabIndex = 2;
            this.radioButtonOptionHide.TabStop = true;
            this.radioButtonOptionHide.Text = "Hide on first / last record";
            this.radioButtonOptionHide.UseVisualStyleBackColor = true;
            // 
            // radioButtonOptionDisable
            // 
            this.radioButtonOptionDisable.AutoSize = true;
            this.radioButtonOptionDisable.Location = new System.Drawing.Point(6, 47);
            this.radioButtonOptionDisable.Name = "radioButtonOptionDisable";
            this.radioButtonOptionDisable.Size = new System.Drawing.Size(186, 19);
            this.radioButtonOptionDisable.TabIndex = 1;
            this.radioButtonOptionDisable.TabStop = true;
            this.radioButtonOptionDisable.Text = "Deactivate on first / last record";
            this.radioButtonOptionDisable.UseVisualStyleBackColor = true;
            // 
            // radioButtonOptionNone
            // 
            this.radioButtonOptionNone.AutoSize = true;
            this.radioButtonOptionNone.Location = new System.Drawing.Point(6, 22);
            this.radioButtonOptionNone.Name = "radioButtonOptionNone";
            this.radioButtonOptionNone.Size = new System.Drawing.Size(101, 19);
            this.radioButtonOptionNone.TabIndex = 0;
            this.radioButtonOptionNone.TabStop = true;
            this.radioButtonOptionNone.Text = "Display always";
            this.radioButtonOptionNone.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(298, 120);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(385, 155);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxNavigationButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.Text = "FormSettings";
            this.TransparencyKey = System.Drawing.Color.Red;
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.groupBoxNavigationButton.ResumeLayout(false);
            this.groupBoxNavigationButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxNavigationButton;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.RadioButton radioButtonOptionHide;
        private System.Windows.Forms.RadioButton radioButtonOptionDisable;
        private System.Windows.Forms.RadioButton radioButtonOptionNone;
    }
}