using namasdev.Reflection;
using namasdev.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace namasdev.Web.Forms.IU
{
    public class ListViewEditable<T>
        where T : class, new()
    {
        public event EventHandler<Exception> Error;

        public delegate T MapearObjetoDesdeListViewItem(ListViewItem item);

        private ListView _controlListView;
        private IButtonControl _controlBotonAgregar;
        private List<T> _itemsObjetos;
        private MapearObjetoDesdeListViewItem _mapearObjetoDesdeListViewItem;
        private bool _asegurarExistenciaDeUnItem;

        public ListViewEditable(ListView controlListView, IButtonControl controlBotonAgregar, MapearObjetoDesdeListViewItem mapearObjetoDesdeListViewItem,
            bool asegurarExistenciaDeUnItem = true, Func<T> crearNuevoItem = null)
        {
            Validador.ValidarRequerido(controlListView, "controlListView");
            Validador.ValidarRequerido(controlBotonAgregar, "controlBotonAgregar");
            Validador.ValidarRequerido(mapearObjetoDesdeListViewItem, "mapearObjetoDesdeListViewItem");

            _controlListView = controlListView;
            _controlBotonAgregar = controlBotonAgregar;
            _mapearObjetoDesdeListViewItem = mapearObjetoDesdeListViewItem;
            _asegurarExistenciaDeUnItem = asegurarExistenciaDeUnItem;

            CrearNuevoItem = crearNuevoItem ?? (() => new T());
            
            _itemsObjetos = new List<T>();
            
            ConfigurarControles();
        }

        #region Propiedades
        
        public IReadOnlyList<T> ItemsObjetos
        {
            get { return _itemsObjetos.AsReadOnly(); }
        }

        private Func<T> CrearNuevoItem { get; set; }

        #endregion

        #region Eventos

        void _control_Load(object sender, EventArgs e)
        {
            try
            {
                if (_controlListView.Page.IsPostBack)
                {
                    ActualizarItemsObjetoDesdeControles();
                }
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        void _controlBotonAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                AgregarNuevoItemObjetoALista();
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        void _control_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Eliminar":
                        EliminarItemObjetoDeLista(e.Item.DataItemIndex);
                        break;
                }
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        #endregion

        #region Metodos

        private void ConfigurarControles()
        {
            _controlListView.Load += _control_Load;
            _controlListView.ItemCommand += _control_ItemCommand;

            _controlBotonAgregar.Click += _controlBotonAgregar_Click;
        }

        private void OnError(Exception ex)
        {
            var evt = Error;
            if (evt != null)
            {
                evt(this, ex);
            }
        }

        public void LimpiarListaItemsObjeto()
        {
            _itemsObjetos.Clear();

            AsegurarExistenciaDeUnItemSiCorresponde();
            ActualizarControlListView();
        }

        public void CargarItemsObjetosExistentes(IEnumerable<T> itemsObjetos)
        {
            _itemsObjetos = itemsObjetos.ToList();

            AsegurarExistenciaDeUnItemSiCorresponde();
            ActualizarControlListView();
        }

        private void AgregarNuevoItemObjetoALista()
        {
            _itemsObjetos.Add(CrearNuevoItem());

            ActualizarControlListView();
        }

        private void EliminarItemObjetoDeLista(int indice)
        {
            _itemsObjetos.RemoveAt(indice);

            AsegurarExistenciaDeUnItemSiCorresponde();
            ActualizarControlListView();
        }

        private void ActualizarControlListView()
        {
            _controlListView.DataBind();
        }

        public IEnumerable<T> CrearItemsObjetosDesdeControles()
        {
            var itemsObjeto = new List<T>();
            foreach (var item in _controlListView.Items)
            {
                var objeto = _mapearObjetoDesdeListViewItem(item);
                if (!ReflectionUtilidades.TodasLasPropiedadesDelObjetoTienenValorDefault(objeto))
                {
                    itemsObjeto.Add(objeto);
                }
            }
            return itemsObjeto;
        }

        private void AsegurarExistenciaDeUnItemSiCorresponde()
        {
            if (_asegurarExistenciaDeUnItem && !_itemsObjetos.Any())
            {
                _itemsObjetos.Add(CrearNuevoItem());
            }
        }

        private void ActualizarItemsObjetoDesdeControles()
        {
            _itemsObjetos = CrearItemsObjetosDesdeControles()
                .ToList();

            AsegurarExistenciaDeUnItemSiCorresponde();
            ActualizarControlListView();
        }

        #endregion
    }
}
