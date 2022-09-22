using System;
using System.Windows;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;


namespace Texteditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string FILENAME = @"C:\Users\Admin\Desktop\TextFile.txt";
        public MainWindow()
        {
            InitializeComponent();
            RTBox.Focus();
             
        }

        // Font Style Italic ein
        private void Bold_Checked(object sender, RoutedEventArgs e)
        {
           var x = RTBox.FontWeight = FontWeights.Bold;
         
        }

        // Font Style Bold aus
        private void Bold_Unchecked(object sender, RoutedEventArgs e)
        {
            RTBox.FontWeight = FontWeights.Normal;
        }

        // Font Style Italic ein
        private void Italic_Checked(object sender, RoutedEventArgs e)
        {
            RTBox.FontStyle = FontStyles.Italic;
        }

        // Font Style Italic aus
        private void Italic_Unchecked(object sender, RoutedEventArgs e)
        {
            RTBox.FontStyle = FontStyles.Normal;
        }

        // Font Size +
        private void IncreaseFont_Click(object sender, RoutedEventArgs e)
        {
            if (RTBox.FontSize < 18)
            {
                RTBox.FontSize += 2;
            }
        }

        //Font Size -
        private void DecreaseFont_Click(object sender, RoutedEventArgs e)
        {
            if (RTBox.FontSize > 10)
            {
                RTBox.FontSize -= 2;
            }
        }

        // Close Menu
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Speichern der Datei und neu Erstellen
            private void Speichern(object sender, RoutedEventArgs e)
            {
                SaveFileDialog dlg = new SaveFileDialog();            
                dlg.FileName = "TextFile"; // Standart Dokument name
                dlg.DefaultExt = ".text"; // Normale datei erweiterung
                dlg.Filter = "Text documents (.txt)|*.txt"; // File Filter

                // Zeigen der Dialog Box
                Nullable<bool> result = dlg.ShowDialog();

                // Ergebnisse des Dialogfelds „Datei speichern“ verarbeiten
                if (result == true)
                {
                    FILENAME = dlg.FileName;
                    pfadi.Text = FILENAME;
                    // Speichern des Dokuments
                
                    TextWriter tw = new StreamWriter(FILENAME);
                    tw.Write(RTBox.Text);
                    tw.Close();
                
                }
            }
        private void Laden(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "TextFile"; // Standart Dokument name
            dlg.DefaultExt = ".text"; // Normale datei erweiterung
            dlg.Filter = "Text documents (.txt)|*.txt"; // File Filter

            if (dlg.ShowDialog() == true)
            {
                TextReader tr = new StreamReader(dlg.FileName);
                FILENAME = dlg.FileName;
                RTBox.Text = tr.ReadToEnd();
                tr.Close();
                pfadi.Text = FILENAME;
            }
        }

        private void Neu(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wirklich alles löschen?", "Editor", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                RTBox.Clear();
            }
        }
        private void txtEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int row = RTBox.GetLineIndexFromCharacterIndex(RTBox.CaretIndex);
            int col = RTBox.CaretIndex - RTBox.GetCharacterIndexFromLineIndex(row);
            lblCursorPosition.Text = "Zeile: " + (row + 1) + ", Spalte: " + (col + 1);
        }


        private void Controls(object sender, KeyEventArgs e)
            {
            
            if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                
                if (File.Exists(FILENAME))
                {
                    TextWriter tw = new StreamWriter(FILENAME);
                    tw.Write(RTBox.Text);
                    tw.Close();
                }
                
                else {
                    Speichern(sender, e);
                }
                e.Handled = true;
            }
        }
    }
}
