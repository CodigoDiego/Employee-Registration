
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;

namespace Logic
{
    public class Workers:ProfPics
    {
        public class lWorkers { 
        private List<TextBox> listTextBox;//se crea la lista para poder inicializarla en la linea 16
        public PictureBox image; 
        public lWorkers(List<TextBox> listTextBox, object[] objetos)  //Metodo que tiene de parametro una lista de objeto textbox con el nombre textboxlist
        {
            image = (PictureBox)objetos[0];
            this.listTextBox = listTextBox;   //Linea 16 en donde se inicializa la lista con la propiedad de la otra lista obtenida
        }
    }
        }
}
// EN ESTA LA CLASE WORKERS HEREDA DE LA QUE TIENE EL METODO