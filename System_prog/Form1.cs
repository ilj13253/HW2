using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace System_prog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        delegate void UpdateResultCallback(string message);
        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(CountWords);
            t.Start(textBox1.Text);
        }
        private void UpdateResult(string message)
        {
            Result.Text = message;
        }
        private void CountWords(object data)
        {
            var path = data as string;
            try
            {
                var text = File.ReadAllText(path);
                var words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string message = $"Result: {words.Length} words";
                if (Result.InvokeRequired)
                {
                    UpdateResultCallback callback = new UpdateResultCallback(UpdateResult);
                    Result.Invoke(callback, new object[] { message });
                }
                else
                {
                    UpdateResult(message);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error reading file: {ex.Message}";
                if (Result.InvokeRequired)
                {
                    UpdateResultCallback callback = new UpdateResultCallback(UpdateResult);
                    Result.Invoke(callback, new object[] { errorMessage });
                }
                else
                {
                    UpdateResult(errorMessage);
                }
            }
        }
    }
}