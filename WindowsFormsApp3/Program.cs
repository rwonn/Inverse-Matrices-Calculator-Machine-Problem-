using System;
using System.Windows.Forms;

// Ensure this namespace matches your form files
namespace MatrixInverseCalculator
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // This is the fix:
            // Run 'MatrixInverseForm' instead of 'Form1'
            Application.Run(new MatrixInverseForm());
        }
    }
}
