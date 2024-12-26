using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using QRCoder;

namespace demoekz
{
    public partial class QRCodeForm : Form
    {
        public QRCodeForm()
        {
            InitializeComponent();
            GenerateQRCode();
        }

        private void GenerateQRCode()
        {
            string qrCodeText = "https://docs.google.com/forms/d/e/1FAIpQLSfkJf4oLCYcKbQggFu97aT6VplRHjBeAAj23LbdNANcQoncPw/viewform?usp=dialog";

            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(5);
                pictureBoxQRCode.Image = qrCodeImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при генерации QR-кода: " + ex.Message);
            }
        }
    }
}
