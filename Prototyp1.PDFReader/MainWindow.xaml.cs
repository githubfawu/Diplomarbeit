using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using IronPdf;
using System.Windows.Media;
using System.Drawing;

namespace Prototyp1.PDFReader
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string filePath = @"C:\Users\adm_fwue\Desktop\Eintritts-PDFs\VZ_Eintritt_per_2022_09_09.pdf";

            using PdfDocument pdf = PdfDocument.FromFile(filePath);
            string text = pdf.ExtractAllText();

            var splitString = Regex.Split(text, "\r\n", RegexOptions.Multiline);

            List<Mitarbeiter> mitarbeiterliste = new List<Mitarbeiter>();
            Mitarbeiter ?mitarbeiter = null;

            foreach (var entity in splitString)
            {
                if (entity.Contains("Mitarb.-Nr.:"))
                {
                    var maNrString = entity.Substring(13);
                    var maNrInt = Int32.Parse(maNrString);

                    var neuerMitarbeiter = new Mitarbeiter()
                    {
                        MitarbeiterNummer = maNrInt
                    };
                    mitarbeiterliste.Add(neuerMitarbeiter);
                    mitarbeiter = neuerMitarbeiter;
                }

                if (entity.Contains("Kürzel"))
                {
                    var maKuerzel = entity.Substring(8);
                    mitarbeiter.Kuerzel = maKuerzel;
                }

                if (entity.Contains("Vorname"))
                {
                    var maVorname = entity.Substring(9);
                    mitarbeiter.Vorname = maVorname;
                }
            }

            foreach (var employee in mitarbeiterliste)
            {
                TB.AppendText(employee.MitarbeiterNummer.ToString());
                TB.AppendText(System.Environment.NewLine);
                TB.AppendText(employee.Kuerzel);
                TB.AppendText(System.Environment.NewLine);
                TB.AppendText(employee.Vorname);
                TB.AppendText(System.Environment.NewLine);
            }
        }

        public class Mitarbeiter
        {
            public int MitarbeiterNummer;
            public string? Kuerzel;
            public string? Vorname;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
