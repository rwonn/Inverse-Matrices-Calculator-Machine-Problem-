using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MatrixInverseCalculator
{
    public partial class MatrixInverseForm : Form
    {
        // UI elements will be defined in the .Designer.cs file
        // We will create 9 TextBoxes for Matrix A, 3 for Matrix B
        private TextBox[,] matrixATextBoxes;
        private TextBox[] matrixBTextBoxes;
        private Button calculateButton;
        private Label labelA;
        private Label labelB;
        private Label labelX;
        private Label labelInverse;
        private Label remarksLabel;
        private TextBox inverseResultTextBox;
        private TextBox solutionResultTextBox;

        public MatrixInverseForm()
        {
            // This function (defined in .Designer.cs) sets up all the UI
            InitializeComponent();

            // Manually group the TextBoxes for easier access
            GroupTextBoxes();

            // Hook up the button's click event
            this.calculateButton.Click += new System.EventHandler(this.CalculateButton_Click);
        }

        /// <summary>
        /// Groups the dynamically created TextBoxes into arrays for easy processing.
        /// </summary>
        private void GroupTextBoxes()
        {
            // This assumes the TextBoxes are named txtA_00, txtA_01, etc.
            // and txtB_0, txtB_1, etc. in the Designer file.
            matrixATextBoxes = new TextBox[3, 3]
            {
                { txtA_00, txtA_01, txtA_02 },
                { txtA_10, txtA_11, txtA_12 },
                { txtA_20, txtA_21, txtA_22 }
            };

            matrixBTextBoxes = new TextBox[3] { txtB_0, txtB_1, txtB_2 };
        }

        /// <summary>
        /// Main event handler for the "Calculate" button.
        /// </summary>
        private void CalculateButton_Click(object sender, EventArgs e)
        {
            double[,] matrixA = new double[3, 3];
            double[] matrixB = new double[3];

            // --- 1. Parse Input ---
            // Try to read all values from the TextBoxes
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        matrixA[i, j] = ParseInputToDouble(matrixATextBoxes[i, j].Text);
                    }
                    matrixB[i] = ParseInputToDouble(matrixBTextBoxes[i].Text);
                }
            }
            catch (FormatException ex)
            {
                SetRemark($"Error: {ex.Message}. Please enter valid numbers or fractions (e.g., 1/3).", true);
                return;
            }

            // --- 2. Calculate Determinant and Check Invertibility ---
            double determinant = CalculateDeterminant(matrixA);

            if (Math.Abs(determinant) < 1e-10) // Check if determinant is (close to) zero
            {
                // Guideline b.)
                SetRemark("Remarks: Coefficient matrix is non-invertible (Determinant is 0).", true);
                inverseResultTextBox.Text = "N/A";
                solutionResultTextBox.Text = "N/A";
                return;
            }

            // --- 3. Calculate Inverse (Guideline b.) ---
            double[,] inverseA = CalculateInverse(matrixA, determinant);
            DisplayMatrix(inverseA, inverseResultTextBox);

            // --- 4. Calculate Solution [X] = A^-1 * B (Guideline c.) ---
            double[] solutionX = MultiplyMatrixVector(inverseA, matrixB);
            DisplayVector(solutionX, solutionResultTextBox);

            // Clear remarks if successful
            SetRemark("Remarks: Solution found.", false);
        }

        /// <summary>
        /// Parses a string that is either a decimal or a fraction (e.g., "1/3") into a double.
        /// </summary>
        private double ParseInputToDouble(string input)
        {
            input = input.Trim();
            try
            {
                // Try parsing directly as a double first
                return double.Parse(input);
            }
            catch (FormatException)
            {
                // If it fails, check for a fraction
                if (input.Contains("/"))
                {
                    string[] parts = input.Split('/');
                    if (parts.Length == 2)
                    {
                        try
                        {
                            double numerator = double.Parse(parts[0].Trim());
                            double denominator = double.Parse(parts[1].Trim());

                            if (Math.Abs(denominator) < 1e-10) // Check for division by zero
                            {
                                throw new FormatException("Division by zero in fraction");
                            }
                            return numerator / denominator;
                        }
                        catch (FormatException)
                        {
                            // Re-throw if parsing numerator/denominator fails
                            throw new FormatException("Invalid fraction format");
                        }
                    }
                }
                // If it's not a double and not a valid fraction, throw
                throw new FormatException("Invalid input format");
            }
        }

        /// <summary>
        /// Calculates the determinant of a 3x3 matrix.
        /// </summary>
        private double CalculateDeterminant(double[,] m)
        {
            // Using Sarrus' rule or cofactor expansion
            double det = m[0, 0] * (m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1]) -
                         m[0, 1] * (m[1, 0] * m[2, 2] - m[1, 2] * m[2, 0]) +
                         m[0, 2] * (m[1, 0] * m[2, 1] - m[1, 1] * m[2, 0]);
            return det;
        }

        /// <summary>
        /// Calculates the inverse of a 3x3 matrix.
        /// Assumes determinant is non-zero.
        /// </summary>
        private double[,] CalculateInverse(double[,] m, double determinant)
        {
            double[,] inverse = new double[3, 3];
            double invDet = 1.0 / determinant;

            // Calculate the adjugate matrix (transpose of the cofactor matrix)
            // and multiply by (1 / determinant)

            // Row 0
            inverse[0, 0] = (m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1]) * invDet;
            inverse[0, 1] = (m[0, 2] * m[2, 1] - m[0, 1] * m[2, 2]) * invDet;
            inverse[0, 2] = (m[0, 1] * m[1, 2] - m[0, 2] * m[1, 1]) * invDet;

            // Row 1
            inverse[1, 0] = (m[1, 2] * m[2, 0] - m[1, 0] * m[2, 2]) * invDet;
            inverse[1, 1] = (m[0, 0] * m[2, 2] - m[0, 2] * m[2, 0]) * invDet;
            inverse[1, 2] = (m[0, 2] * m[1, 0] - m[0, 0] * m[1, 2]) * invDet;

            // Row 2
            inverse[2, 0] = (m[1, 0] * m[2, 1] - m[1, 1] * m[2, 0]) * invDet;
            inverse[2, 1] = (m[0, 1] * m[2, 0] - m[0, 0] * m[2, 1]) * invDet;
            inverse[2, 2] = (m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0]) * invDet;

            return inverse;
        }

        /// <summary>
        /// Multiplies a 3x3 matrix by a 3x1 vector.
        /// </summary>
        private double[] MultiplyMatrixVector(double[,] matrix, double[] vector)
        {
            double[] result = new double[3];
            for (int i = 0; i < 3; i++)
            {
                result[i] = matrix[i, 0] * vector[0] +
                            matrix[i, 1] * vector[1] +
                            matrix[i, 2] * vector[2];
            }
            return result;
        }

        // --- Helper functions for UI display ---

        /// <summary>
        /// Sets the text and color of the remarks label.
        /// </summary>
        private void SetRemark(string text, bool isError)
        {
            remarksLabel.Text = text;
            remarksLabel.ForeColor = isError ? Color.Red : Color.Green;
        }

        /// <summary>
        /// Formats and displays a 3x3 matrix in a TextBox.
        /// </summary>
        private void DisplayMatrix(double[,] matrix, TextBox textBox)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                sb.AppendFormat("[ {0,8:F4}  {1,8:F4}  {2,8:F4} ]\r\n",
                    matrix[i, 0], matrix[i, 1], matrix[i, 2]);
            }
            textBox.Text = sb.ToString();
        }

        /// <summary>
        /// Formats and displays a 3x1 vector in a TextBox.
        /// </summary>
        private void DisplayVector(double[] vector, TextBox textBox)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[ {0,8:F4} ]\r\n", vector[0]);
            sb.AppendFormat("[ {0,8:F4} ]\r\n", vector[1]);
            sb.AppendFormat("[ {0,8:F4} ]", vector[2]);
            textBox.Text = sb.ToString();
        }
    }
}