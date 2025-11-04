using System.Windows.Forms;
using System.Drawing;

namespace MatrixInverseCalculator
{
    // This is a partial class, meaning its other half is in MatrixInverseForm.cs
    public partial class MatrixInverseForm : Form
    {
        // --- Declare all the UI components ---

        // TextBoxes for Matrix A (Coefficient Matrix)
        private TextBox txtA_00;
        private TextBox txtA_01;
        private TextBox txtA_02;
        private TextBox txtA_10;
        private TextBox txtA_11;
        private TextBox txtA_12;
        private TextBox txtA_20;
        private TextBox txtA_21;
        private TextBox txtA_22;

        // TextBoxes for Matrix B (Constant Vector)
        private TextBox txtB_0;
        private TextBox txtB_1;
        private TextBox txtB_2;

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
            this.txtA_00 = new System.Windows.Forms.TextBox();
            this.txtA_01 = new System.Windows.Forms.TextBox();
            this.txtA_02 = new System.Windows.Forms.TextBox();
            this.txtA_10 = new System.Windows.Forms.TextBox();
            this.txtA_11 = new System.Windows.Forms.TextBox();
            this.txtA_12 = new System.Windows.Forms.TextBox();
            this.txtA_20 = new System.Windows.Forms.TextBox();
            this.txtA_21 = new System.Windows.Forms.TextBox();
            this.txtA_22 = new System.Windows.Forms.TextBox();

            this.txtB_0 = new System.Windows.Forms.TextBox();
            this.txtB_1 = new System.Windows.Forms.TextBox();
            this.txtB_2 = new System.Windows.Forms.TextBox();

            this.calculateButton = new System.Windows.Forms.Button();

            this.labelA = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.labelInverse = new System.Windows.Forms.Label();
            this.remarksLabel = new System.Windows.Forms.Label();

            this.inverseResultTextBox = new System.Windows.Forms.TextBox();
            this.solutionResultTextBox = new System.Windows.Forms.TextBox();

            this.SuspendLayout();

            // --- Setup Form ---
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 353);
            this.Font = new Font("Arial", 10F);
            this.Text = "3x3 Inverse Matrix Solver";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // --- Labels ---
            this.labelA.Text = "Matrix [A]";
            this.labelA.Location = new Point(12, 9);
            this.labelA.Size = new Size(100, 20);

            this.labelB.Text = "Vector [B]";
            this.labelB.Location = new Point(200, 9);
            this.labelB.Size = new Size(100, 20);

            this.labelInverse.Text = "Inverse [A]⁻¹";
            this.labelInverse.Location = new Point(12, 170);
            this.labelInverse.Size = new Size(100, 20);

            this.labelX.Text = "Solution [X]";
            this.labelX.Location = new Point(280, 170);
            this.labelX.Size = new Size(100, 20);

            this.remarksLabel.Text = "Remarks: Enter values and press calculate.";
            this.remarksLabel.Location = new Point(12, 320);
            this.remarksLabel.Size = new Size(558, 20);
            this.remarksLabel.ForeColor = Color.Blue;

            // --- TextBoxes for Matrix A ---
            int startX_A = 15;
            int startY_A = 35;
            int spacing = 35;
            int txtSize = 50;

            this.txtA_00.Location = new Point(startX_A, startY_A);
            this.txtA_01.Location = new Point(startX_A + spacing, startY_A);
            this.txtA_02.Location = new Point(startX_A + 2 * spacing, startY_A);

            this.txtA_10.Location = new Point(startX_A, startY_A + spacing);
            this.txtA_11.Location = new Point(startX_A + spacing, startY_A + spacing);
            this.txtA_12.Location = new Point(startX_A + 2 * spacing, startY_A + spacing);

            this.txtA_20.Location = new Point(startX_A, startY_A + 2 * spacing);
            this.txtA_21.Location = new Point(startX_A + spacing, startY_A + 2 * spacing);
            this.txtA_22.Location = new Point(startX_A + 2 * spacing, startY_A + 2 * spacing);

            TextBox[] aBoxes = { txtA_00, txtA_01, txtA_02, txtA_10, txtA_11, txtA_12, txtA_20, txtA_21, txtA_22 };
            foreach (var box in aBoxes) { box.Size = new Size(txtSize, 23); box.Text = "0"; box.TextAlign = HorizontalAlignment.Right; }

            // --- TextBoxes for Matrix B ---
            int startX_B = 203;
            this.txtB_0.Location = new Point(startX_B, startY_A);
            this.txtB_1.Location = new Point(startX_B, startY_A + spacing);
            this.txtB_2.Location = new Point(startX_B, startY_A + 2 * spacing);

            TextBox[] bBoxes = { txtB_0, txtB_1, txtB_2 };
            foreach (var box in bBoxes) { box.Size = new Size(txtSize, 23); box.Text = "0"; box.TextAlign = HorizontalAlignment.Right; }

            // --- Calculate Button ---
            this.calculateButton.Text = "Calculate [X] = A⁻¹ B";
            this.calculateButton.Location = new Point(280, 35);
            this.calculateButton.Size = new Size(290, 105);
            this.calculateButton.Font = new Font("Arial", 12F, FontStyle.Bold);
            this.calculateButton.BackColor = Color.LightGreen;

            // --- Result TextBoxes ---
            this.inverseResultTextBox.Location = new Point(15, 195);
            this.inverseResultTextBox.Size = new Size(250, 110);
            this.inverseResultTextBox.Multiline = true;
            this.inverseResultTextBox.ReadOnly = true;
            this.inverseResultTextBox.Font = new Font("Courier New", 10F);

            this.solutionResultTextBox.Location = new Point(280, 195);
            this.solutionResultTextBox.Size = new Size(150, 110);
            this.solutionResultTextBox.Multiline = true;
            this.solutionResultTextBox.ReadOnly = true;
            this.solutionResultTextBox.Font = new Font("Courier New", 10F);

            // --- Add Controls to Form ---
            this.Controls.AddRange(aBoxes);
            this.Controls.AddRange(bBoxes);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.labelA);
            this.Controls.Add(this.labelB);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.labelInverse);
            this.Controls.Add(this.remarksLabel);
            this.Controls.Add(this.inverseResultTextBox);
            this.Controls.Add(this.solutionResultTextBox);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
