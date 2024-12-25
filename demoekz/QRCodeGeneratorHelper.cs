using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoekz
{
    public class QRCodeGeneratorHelper
    {
        public static Bitmap GenerateQRCode(string url, int pixelsPerModule = 4)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                using (var qrCode = new QRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(pixelsPerModule); // Размер каждого модуля QR-кода
                }
            }
        }

        
    }
}
