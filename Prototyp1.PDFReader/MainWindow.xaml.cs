using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using IronPdf;

namespace Prototyp1.PDFReader_IronPDF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string filePath = @"C:\Users\adm_fwue\Desktop\Eintritts-PDFs\VZ_Eintritt_per_2022_10_10.pdf";

            using PdfDocument pdf = PdfDocument.FromFile(filePath);
            string text = pdf.ExtractAllText();

            var splitString = Regex.Split(text, "\r\n", RegexOptions.IgnoreCase);

            List<Mitarbeiter> mitarbeiterliste = new List<Mitarbeiter>();
            Mitarbeiter ?mitarbeiter = null;

            foreach (var entity in splitString)
            {
                if (entity.Contains("Mitarb.-Nr.:"))
                {
                    var maNrString = entity.Substring(13);
                    var maNrInt = int.Parse(maNrString);

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
                    if (mitarbeiter != null) mitarbeiter.Kuerzel = maKuerzel;
                }

                if (entity.Contains("Vorname"))
                {
                    var maVorname = entity.Substring(9);
                    if (mitarbeiter != null) mitarbeiter.Vorname = maVorname;
                }

                if (entity.Contains("Name"))
                {
                    var maName = entity.Substring(6);
                    if (mitarbeiter != null) mitarbeiter.Nachname = maName;
                }

                if (entity.Contains("Vorgesetzter") && (!entity.Contains("dir")))
                {
                    var maVorgesetzter = entity.Substring(14);
                    if (mitarbeiter != null) mitarbeiter.Vorgesetzter = maVorgesetzter;
                }
            }

            foreach (var employee in mitarbeiterliste)
            {
                TB.AppendText($"Mitarbeiternummer: " + employee.MitarbeiterNummer.ToString());
                TB.AppendText(Environment.NewLine);
                TB.AppendText($"Kürzel: " + employee.Kuerzel);
                TB.AppendText(Environment.NewLine);
                TB.AppendText($"Vorname: " + employee.Vorname);
                TB.AppendText(Environment.NewLine);
                TB.AppendText($"Nachname: " + employee.Nachname);
                TB.AppendText(Environment.NewLine);
                TB.AppendText($"Vorgesetzter: " + employee.Vorgesetzter);
                TB.AppendText(Environment.NewLine);
            }
        }

        public class Mitarbeiter
        {
            public int? MitarbeiterNummer;
            public string? Kuerzel;
            public string? Vorname;
            public string? Nachname;
            public string? Vorgesetzter;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
