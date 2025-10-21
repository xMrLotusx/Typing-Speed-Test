using System.Diagnostics; //measuring time taken to type.
using System.Drawing.Text; //(unused here and can be removed)
using System.Web;//(unused and can be removed)

namespace Typing_Speed_Test_V1._2_RevA
{
    public partial class Form1 : Form
    {
        //array of sentences used for the type test.
        private string[] testSentences = new string[]
        {
            "How fast can you type?",
            "Typing fast and accurately is a valuable skill.",
            "Practice makes perfect when it comes to typing speed."
        };

        //Index to track which sentence is currently being used
        private int currentSentenceIndex = 0;
        //stopwatch to measure typing duration
        private Stopwatch stopwatch = new Stopwatch();
        private int deleteCount = 0; //tracks how many times the user presses Backspace

        //Constructor: Initializes the form and sets up the initial state
        public Form1()
        {
            InitializeComponent();
            LoadSentence(); // Load the first sentence
            txtInput.Enabled = false; //disable typing until test starts
            btnSubmit.Enabled = false;//disable submit until test starts
            btnNext.Enabled = false;// disable next until the test is submitted
        }
        //loads the current sentence into the label for the user to type
        private void LoadSentence()
        {
            lblOutput1.Text = testSentences[currentSentenceIndex];
        }
        // starts the typing test
        private void btnStart_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";// clear the previous input
            txtInput.Enabled = true;//enable typing
            txtInput.Focus();//Focus the input box
            stopwatch.Restart();//start the timer
            btnSubmit.Enabled = true;//enable submit button
            btnNext.Enabled = false;//disables next button
            lblResult.Text = "";//clear previous result
            deleteCount = 0; //reset delete counter
        }
        // submits the user's input and calculates WPM and accuracy
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();// stop timer
            txtInput.Enabled = false; // disable typing
            btnSubmit.Enabled = false; // disable submit button
            btnNext.Enabled = true;// enable next button

            string testText = testSentences[currentSentenceIndex];// get the current sentence
            double timeInMinutes = stopwatch.Elapsed.TotalMinutes;//calculate time in minutes
            int wordCount = testText.Split(' ').Length;// count words in the sentence
            int correctChars = 0;

            //compare each character typed with the original sentence
            for (int i = 0; i < testText.Length && i < txtInput.Text.Length; i++)
            {
                if (testText[i] == txtInput.Text[i])
                    correctChars++;
            }

            //calculate Accuracy and WPM
            double accuracy = (double)correctChars / testText.Length * 100;
            double wpm = wordCount / timeInMinutes;

            //Display the result
            lblResult.Text = $"WPM: {wpm:F2}, Accuracy: {accuracy:F2}%, Deletes: {deleteCount}";
        }

        //loads the next sentence and resets the form for a new test
        private void btnNext_Click(object sender, EventArgs e)
        {
            currentSentenceIndex = (currentSentenceIndex + 1) % testSentences.Length; // move to next sentence
            LoadSentence(); //load the new sentence
            txtInput.Text = "";// clear input
            lblResult.Text = "";//clear result
            txtInput.Enabled = false;//disable typing until start is pressed
            btnSubmit.Enabled = false;//disable submit
            btnNext.Enabled = false;//disable next

        }

        //allows the user to press Enter to submit instead of clicking the button
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                deleteCount++; // increment delete Counter
            }

            if (e.KeyCode == Keys.Enter && btnSubmit.Enabled)
            {
                e.SuppressKeyPress = true; // prevents the beep sound and newline.
                btnSubmit.PerformClick(); // triggers the submit button click/logic.
            }
            
        }
    }
}
