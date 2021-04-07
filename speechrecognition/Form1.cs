using System;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace speechrecognition
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer synth = new SpeechSynthesizer();

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            stopButton.Enabled = true;
            startButton.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "say hello", "print my name" });
            GrammarBuilder g = new GrammarBuilder();
            g.Append(commands);
            Grammar grammar = new Grammar(g);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;

            synth.Rate = 3;
        }
        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "say hello":
                    synth.SpeakAsync("Hello");
                    break;
                case "print my name":
                    log.Text += "\nName: Owais Quadri";
                    break;
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            startButton.Enabled = true;
            stopButton.Enabled = false;
        }
    }
}
