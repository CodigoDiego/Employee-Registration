using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;


namespace Logic
{
    public class ProfPics
    {
        private OpenFileDialog fd = new OpenFileDialog();
        public void CargarImagen(PictureBox pictureBox)
        {
            pictureBox.WaitOnLoad = true;
            fd.Filter = "Images|*.jpg;*.gif;*.png;*.bmp";
            //fd.Multiselect = true;  //Este metodo deja escoger varios archivos para abrir
            fd.ShowDialog();
            if (fd.FileName != string.Empty)
            {
                pictureBox.ImageLocation = fd.FileName;
            }

        }
        public byte[] ImageToByte(Image img)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

    }

    }

// ESTA CLASE CONTIENE EL METODO PARA CARGAR UNA IMAGEN DESDE EL DIRECTORIO DE WINDOWS