using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace namasdev.Web.Forms.UI.Extensiones
{
    public static class ControlExtensiones
    {
        public static TControl ObtenerControl<TControl>(this Control control, string controlId,
            bool validarExistencia = false)
            where TControl : Control
        {
            var res = control.FindControl(controlId) as TControl;
            if (validarExistencia && res == null)
            {
                throw new Exception(String.Format("Control {0} inexistente.", controlId));
            }

            return res;
        }
    }
}
