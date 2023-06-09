﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Configuration;
using Entidades;
using Logica;
using Microsoft.VisualBasic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Asn1.Crmf;
using System.Diagnostics;

namespace Presentacion
{
    public partial class VentanaPrincipal : Form
    {
        //Instanciamos los servicios mandandoles el connectionString
        MateriaPrimaSerivicio servicioMateriaPrima = new MateriaPrimaSerivicio(ConfigConnection.connectionString);
        ProveedorServicio servicioProveedor = new ProveedorServicio(ConfigConnection.connectionString);
        GastoServicio servicioGasto = new GastoServicio(ConfigConnection.connectionString);
        ProductoServicio servicioProducto = new ProductoServicio(ConfigConnection.connectionString);
        RecetaServicio servicioReceta = new RecetaServicio(ConfigConnection.connectionString);
        IngredienteServicio servicioIngrediente = new IngredienteServicio(ConfigConnection.connectionString);
        FacturaServicio servicioFactura = new FacturaServicio(ConfigConnection.connectionString);
        VentaServicio servicioVenta = new VentaServicio(ConfigConnection.connectionString);
        ClienteServicio servicioCLiente = new ClienteServicio(ConfigConnection.connectionString);

        //Instancias de variables globales

        //sección de recetas
        bool creandoReceta = false;
        string ProductoEnProceso = "";
        string PrecioProductoEnProceso = "";
        List<IngredientesEnEspera> ingredientesProceso = new List<IngredientesEnEspera>();



        //Sección de facturación
        ProductoDTO productoSeleccionado = new ProductoDTO();
        float subTotal = 0;
        float total = 0;
        float dineroDado = 0;
        float cambio = 0F;
        List<Vendidos> ventasFacturando = new List<Vendidos>();
        string CodigoFactura = null;
        List<MateriaPrima> materiasPrimasActuales = new List<MateriaPrima>();
        List<Ingrediente> ingredientesEnProceso = new List<Ingrediente>();
        List<MateriaPrima> materiasPrimasParaActualizar = new List<MateriaPrima>();

        //Sección de productos
        Ingrediente ingredienteSelected = null;

        //Sección de materias primas
        MateriaPrima materiaPrimaSeleccionada = null;
        bool cargandoMateriasPrimas = false;

        public string Informacion { get; set; }
        public VentanaPrincipal()   
        {
            InitializeComponent();
            CargarProveedores();
            CargarMateriasP();
            cargarProductosVenta();
            cargarComboIngredientes();
            txtIdMateriaPrima.Text = servicioMateriaPrima.obtenerSiguienteCodigo() + "";
        }

        private void VentanaPrincipal_Load(object sender, EventArgs e)
        {
         


        }

        private void btnEmpezarReceta_Click(object sender, EventArgs e)
        {
            if (!EsNulo(txtPrecioProductoR.Text) && (!EsNulo(txtnombreProducto.Text)))
            {
                if (!creandoReceta)
                {
                    if (float.Parse(txtPrecioProductoR.Text) != 0)
                    {
                        creandoReceta = true;
                        groupIngredientes.Enabled = true;
                        btnEmpezarReceta.Text = "Finalizar";
                        txtnombreProducto.Enabled = false;
                        txtPrecioProductoR.Enabled = false;
                        ProductoEnProceso = txtnombreProducto.Text;
                        PrecioProductoEnProceso = txtPrecioProductoR.Text;

                    }
                    else 
                    {

                        MessageBox.Show("No puede tener un valor de 0");
                    }
                  


                }
                else
                {

                    DialogResult result = MessageBox.Show("¿Desea finalizar la receta?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        btnEmpezarReceta.Text = "Empezar";
                        creandoReceta = false;
                        groupIngredientes.Enabled = false;
                        txtnombreProducto.Enabled = true;

                        //Ingreso de Producto

                        string idProducto = servicioProducto.obtenerSiguienteID();
                        Producto producto = new Producto(idProducto, ProductoEnProceso, float.Parse(PrecioProductoEnProceso));
                        string resultado = servicioProducto.Insert(producto);

                        //Ingreso de Receta

                        string idReceta = servicioReceta.obtenerSiguienteID();
                        Receta receta = new Receta(idReceta, ProductoEnProceso, idProducto, txtDescripcion.Text);
                        string resultado2 = servicioReceta.Insert(receta);

                        //Ingreso de ingredientes
                        List<Ingrediente> ingredientes = new List<Ingrediente>();
                        foreach (IngredientesEnEspera ingr in ingredientesProceso)
                        {

                            Ingrediente ingrediente = new Ingrediente();
                            ingrediente.idmateriaPrima = ingr.materiaP.idMateriaPrima;
                            ingrediente.idReceta = idReceta;

                            if (ingr.unidad.Equals("gr"))
                            {
                                ingrediente.gramos = float.Parse(ingr.cantidad);
                                ingrediente.unidades = 0;
                                ingrediente.mililitros = 0;
                            }
                            else if (ingr.unidad.Equals("ml"))
                            {
                                ingrediente.gramos = 0;
                                ingrediente.unidades = 0;
                                ingrediente.mililitros = float.Parse(ingr.cantidad);
                            }
                            else
                            {
                                ingrediente.gramos = 0;
                                ingrediente.unidades = Int32.Parse(ingr.cantidad);
                                ingrediente.mililitros = 0;

                            }

                            ingredientes.Add(ingrediente);

                        }


                        string resultado3 = servicioIngrediente.Insert(ingredientes);

                        MessageBox.Show("Productos: " + resultado + " Receta: " + resultado2 + " Ingredientes: " + resultado3);
                        cargarComboIngredientes();
                        cargarProductosVenta();
                        txtnombreProducto.Text = "";
                        txtPrecioProductoR.Text = "";
                        txtDescripcion.Text = " ";
                        txtCantidadIngrediente.Text = "0";
                         creandoReceta = false;
                         ProductoEnProceso = "";
                        PrecioProductoEnProceso = "";
                        ingredientesProceso = new List<IngredientesEnEspera>();
                        listaIngredientesR.Items.Clear();


                    }
                    else if (result == DialogResult.No)
                    {

                    }

                }
            }
            else
            {
                MessageBox.Show("Campo Nombre del producto o Precio inválido");
            }
            
        }

        //Materia Prima ----------------------------------------------------------
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if ((!EsNulo(txtNombreMateriaP.Text)) && (!EsNulo(txtGramosM.Text)) &&(!EsNulo(txtGramosM.Text))
                && (!EsNulo(txtUnidadesM.Text)) && (!EsNulo(txtCostoM.Text)))
            {
                if (txtDateCaducidad.Value > DateTime.Now)
                {
                    bool ActualizarMp = false;
                    List<MateriaPrimaDTO> mtDTO = servicioMateriaPrima.obtenerDTO();
                    int idViejo = 0;
                    foreach (MateriaPrimaDTO materia in mtDTO)
                    {
                        if (txtNombreMateriaP.Text.ToUpper().Equals(materia.NOMBRE))
                        {
                            if (Int32.Parse(materia.ID) > idViejo)
                            {
                                idViejo = Int32.Parse(materia.ID);


                                ActualizarMp = true;
                                idViejo = Int32.Parse(materia.ID);
                            }


                        }
                    }
                    Console.WriteLine(idViejo);
                    ingresarMateriaPrima();
                    if (ActualizarMp)
                    {

                        reEnfocarProductos(Int32.Parse(txtIdMateriaPrima.Text) - 1, idViejo);
                        DesecharMateriaPrima(idViejo);
                        CargarMateriasP();
                        cargarComboIngredientes();

                    }

                }
                else 
                {
                MessageBox.Show("No puede registrar materia prima caducada.");

                }

            }
            else
            {
                MessageBox.Show("Digite toda la información");
            }

        }
       
        private void reEnfocarProductos(int idNuevo, int idViejo)
        {
            servicioIngrediente.reEnfocarProductos(idNuevo,idViejo);
        }

        private string DesecharMateriaPrima(int viejo)
        {
            return servicioMateriaPrima.UpdateCero(viejo);
        }
        public void ingresarMateriaPrima() 
        {


            //Ingresamos la materia prima

            string idMateriaP = txtIdMateriaPrima.Text;
            string NombreMP = txtNombreMateriaP.Text.ToUpper();
            DateTime FechaCaducidad = txtDateCaducidad.Value;
            Console.WriteLine(FechaCaducidad.ToShortDateString());

            float Gramos;
            if (!float.TryParse(txtGramosM.Text, out Gramos))
            { return;}

            float Mililitros;
            if (!float.TryParse(txtMililitrosM.Text, out Mililitros))
            { return; }

            int Unidades;
            if (!int.TryParse(txtUnidadesM.Text, out Unidades))
            { return; }

            MateriaPrima materiaP = new MateriaPrima(idMateriaP, NombreMP, FechaCaducidad, Unidades, Mililitros, Gramos);
            var msg = servicioMateriaPrima.Insert(materiaP);


            //Registrados un gasto 

            DateTime fechaActual = DateTime.Now;

            float costo;
            string codigo = ObtenerDigitosFecha(fechaActual.ToShortDateString()) + servicioMateriaPrima.ObtenerCantidad();
            if (radioGramos.Checked)
            {
                costo = float.Parse(txtCostoM.Text) * Gramos;

            }
            else if (radioMililitros.Checked)
            {
                costo = float.Parse(txtCostoM.Text) * Mililitros;

            }
            else 
            {
                costo = Int32.Parse(txtCostoM.Text) * Unidades;
            }

            Proveedor proveedor = servicioProveedor.ObtenerProveedorPorCodigo(comboProveedor.Text);
            Gasto gasto = new Gasto(codigo,costo,proveedor,materiaP,fechaActual,Gramos,Mililitros,Unidades);
            txtIdMateriaPrima.Text = servicioMateriaPrima.obtenerSiguienteCodigo() + "";
            var msg2 = servicioGasto.Insert(gasto);
            MessageBox.Show("Se generó un gasto de: "+costo+" con el código: "+ codigo);

            CargarMateriasP();
            cargarComboIngredientes();
           txtCostoM.Text = "0";
           txtGramosM.Text = "0";
            txtMililitrosM.Text = "0";
            txtUnidadesM.Text = "0";
            comboProveedor.SelectedIndex = 0;
            txtNombreMateriaP.Text = "";
            
        }
        public void CargarMateriasP()
        {
            List<MateriaPrimaDTO> listaDTO = servicioMateriaPrima.obtenerDTOValido();
            grillaMateriaP.DataSource = listaDTO;
        }


        //Proveedor --------------------------------------------------------------
        private void btnIngresarProveedor_Click(object sender, EventArgs e)
        {
            GuardarProveedor();
            CargarProveedores();
        }
        private void CargarProveedores()
        {
            List<Proveedor> proveedores = servicioProveedor.obtenerProveedores();
            grillaProveedores.DataSource = proveedores;
            comboProveedor.Items.Clear();
            comboProveedor.Items.Add("--Seleccionar proveedor--");

            // Agregar los nombres de los proveedores al ComboBox
            foreach (Proveedor proveedor in proveedores)
            {
                comboProveedor.Items.Add(proveedor.nombre);
            }
            comboProveedor.SelectedIndex = 0;
        }
        public void GuardarProveedor()
        {
            string id = txtIdProveedor.Text;
            string nombre = txtNombreProveedor.Text;
            string telefono = txtTelefonoProveedor.Text;
            string correo = txtCorreoProveedor.Text;

            Proveedor proveedor = new Proveedor(id, nombre, telefono, correo);
            string msg = servicioProveedor.Insert(proveedor);
            MessageBox.Show(msg);

        }


        //Prductos - Facturación -------------------------------------------------
        public void cargarProductosVenta() 
        {
            tablaProductos.DataSource = servicioProducto.obtenerProductosDTO();
            productosTabla.DataSource = servicioProducto.obtenerProductosDTO();


        }
        private void tablaProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = tablaProductos.Rows[e.RowIndex];
                productoSeleccionado = (ProductoDTO)row.DataBoundItem;
                txtProductoFactura.Text = productoSeleccionado.PRODUCTO;
                subTotal = float.Parse(productoSeleccionado.PRECIO);
                labelSubtotal.Text = subTotal + " $";
                comboCantidad.SelectedIndex = 0;
            }

        }


        //Recetas-----------------------------------------------------------------
        public void cargarComboIngredientes() 
        {
            cargandoMateriasPrimas = true;
            List<MateriaPrima> materiasP = servicioMateriaPrima.obtenerMateriasPrimasValidas();
            comboMateriaP.Items.Clear();
            comboMateriaP.Items.Add("--Seleccionar ingrediente--");
            comboMateriaP2.Items.Clear();
            comboMateriaP2.Items.Add("--Seleccionar ingrediente--");
            foreach (MateriaPrima materiaP in materiasP) 
            {
                comboMateriaP.Items.Add(materiaP.nombreMateriaPrima);
                comboMateriaP2.Items.Add(materiaP.nombreMateriaPrima);

            }
            comboMateriaP.SelectedIndex= 0;
            comboMateriaP2.SelectedIndex= 0;
            cargandoMateriasPrimas = false;
        }

        private void btnIngresarIngrediente_Click(object sender, EventArgs e)
        {
            if ((!EsNulo(txtCantidadIngrediente.Text)) && (!EsNulo(txtDescripcion.Text)))
            {
                agregarIngrediente();
            }
            else
            {
                MessageBox.Show("Campo Cantidad o Descripción Inválido");
            }
        }

        private void agregarIngrediente()
        {
            if (comboMateriaP.SelectedIndex == 0)
            {
                MessageBox.Show("Debe seleccionar un ingrediente");
            }
            else
            {
                string nombre = comboMateriaP.Text;
                string cantidad = txtCantidadIngrediente.Text;
                string unidad = comboUnidad.Text;

                MateriaPrima materiaP = servicioMateriaPrima.obtenerMateriaPrimaConNombre(nombre);
                if (!(materiaP == null))
                {
                    bool regist = true;
                    foreach (IngredientesEnEspera ingr in ingredientesProceso)
                    {
                        if (ingr.materiaP.idMateriaPrima.Equals(materiaP.idMateriaPrima))
                        {
                            regist = false;
                        }
                    }
                    if (regist)
                    {
                        IngredientesEnEspera ingrediente = new IngredientesEnEspera(materiaP, cantidad, unidad);
                        ingredientesProceso.Add(ingrediente);
                        MessageBox.Show("Ingrediente Agregado");
                        string item = materiaP.nombreMateriaPrima + "--" + cantidad + " " + unidad;
                        listaIngredientesR.Items.Add(item);
                        txtCantidadIngrediente.Text = "0";

                    }
                    else
                    {
                        MessageBox.Show("Ingrediente ya incluido");
                    }

                }

            }
        }


        private void comboCantidad_MouseClick(object sender, MouseEventArgs e)
        {
         
        }

        private void comboCantidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int existencias = 0;
            existencias  = VerificarExistencias();

            if (productoSeleccionado.PRECIO != null ) 
            {
                if (existencias == -1)
                {
                    MessageBox.Show("Este prdoucto no se puede preparar: vencimiento de materia prima. ");
                    comboCantidad.Text = "1";
                }
                else 
                {
                    if (existencias >= Int32.Parse(comboCantidad.Text))
                    {

                        subTotal = float.Parse(productoSeleccionado.PRECIO) * Int32.Parse(comboCantidad.Text);
                        labelSubtotal.Text = subTotal + " $";

                    }
                    else
                    {
                        MessageBox.Show("solo hay para preparar: " + existencias);

                    }

                }
                
               
               
            }
           
        }

        private int VerificarExistencias()
        {
            if (materiasPrimasActuales.Count == 0)
            {

                materiasPrimasActuales = servicioMateriaPrima.obtenerMateriasPrimas();
            }
            int cantidad = 0;
            int existencias = 0;
            
            Receta receta = servicioReceta.buscarRecetaDeProducto(productoSeleccionado.ID);
            if (receta != null) 
            {
                List<Ingrediente> ingredientes = servicioIngrediente.obtenerIngredientesPorReceta(receta.id);
                ingredientesEnProceso = ingredientes;
                if (ingredientes.Count > 0) 
                {
                    foreach (Ingrediente ingrediente in ingredientes)
                    {
                        MateriaPrima materia = new MateriaPrima();
                        foreach (MateriaPrima materi in materiasPrimasActuales) 
                        {
                            if (materi.idMateriaPrima.Equals(ingrediente.idmateriaPrima)) 
                            {
                                materia = materi;
                            }
                        
                        }

                        if (materia.fechaCaducidad < DateTime.Now) 
                        {
                            Console.WriteLine(materia.fechaCaducidad + " < " + DateTime.Now);

                            return -1;
                        
                        }

                        if (materia.idMateriaPrima != null) 
                        {


                            if (ingrediente.gramos > 0)
                            {
                                cantidad = (int)(materia.gramos / ingrediente.gramos);
                            }
                            else if (ingrediente.mililitros > 0)
                            {
                                cantidad = (int)(materia.mililitros / ingrediente.mililitros);
                            }
                            else
                            {
                                cantidad = (int)(materia.unidades / ingrediente.unidades);
                            }

                            if (existencias == 0 || cantidad <= existencias) 
                            {

                                existencias = cantidad;
                            }
                        }
                      
                    }
                
                }
            }

            return existencias;
        }

        private void btnIngresarFactura_Click(object sender, EventArgs e)
        {
            if (!EsNulo(txtProductoFactura.Text))
            {
                ingresarVenta();
            }
            else
            {
                MessageBox.Show("Elija un producto");
            }
            
        }

        public void ingresarVenta() 
        {
            string fecha = DateTime.Now.ToShortDateString();
            CodigoFactura = ObtenerDigitosFecha(fecha) + servicioFactura.obtenerCodigoFactura();
            Vendidos venta = new Vendidos(productoSeleccionado.ID, CodigoFactura, Int32.Parse(comboCantidad.Text), subTotal);
            bool existe = false;
            foreach (Vendidos vent in ventasFacturando) 
            {
                if (vent.id_producto.Equals(venta.id_producto)) 
                {
                    existe = true;
                
                
                }
            }
            if (!existe)
            {
                ventasFacturando.Add(venta);
                listaFactura.Items.Add(productoSeleccionado.ID + " -. " + productoSeleccionado.PRODUCTO + " x " + comboCantidad.Text + "   -----------------------   " + subTotal + " $");
                total += subTotal;
                labelTotal.Text = total + " $";

                foreach (Ingrediente ingrediente in ingredientesEnProceso)
                {


                    for (int i = 0; i < materiasPrimasActuales.Count; i++)
                    {
                        if (materiasPrimasActuales[i].idMateriaPrima.Equals(ingrediente.idmateriaPrima))
                        {

                            if (ingrediente.gramos > 0)
                            {
                                materiasPrimasActuales[i].gramos = materiasPrimasActuales[i].gramos - (ingrediente.gramos * Int32.Parse(comboCantidad.Text));
                            }
                            else if (ingrediente.mililitros > 0)
                            {
                                materiasPrimasActuales[i].mililitros = materiasPrimasActuales[i].mililitros - (ingrediente.mililitros * Int32.Parse(comboCantidad.Text));
                            }
                            else
                            {
                                materiasPrimasActuales[i].unidades = materiasPrimasActuales[i].unidades - (ingrediente.unidades * Int32.Parse(comboCantidad.Text));
                            }

                            materiasPrimasParaActualizar.Add(materiasPrimasActuales[i]);

                        }

                    }

                }


            }
            else 
            {
                MessageBox.Show("Producto ya ingresado");
            
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            labelCodigoFactura.Text = CodigoFactura;
            labelTotal2.Text = labelTotal.Text;
            tabControl1.SelectedIndex = 5;
            cambio = -total;
            labelCambio.Text = $"{cambio} $";
        }

        private void txtDinero_TextChanged(object sender, EventArgs e)
        {
            if (!EsNulo(txtDinero.Text))
            {
                dineroDado = float.Parse(txtDinero.Text);
                cambio = dineroDado - total;
            }
            else
            {
                cambio = -total;
            }
            labelCambio.Text = cambio + " $";
        }

        private void btnFacturar2_Click(object sender, EventArgs e)
        {
            Cliente cliente = servicioCLiente.BuscarCliente(txtCedulaCliente.Text);
            if (cliente != null)
            {
                if (cambio >= 0)
                {
                    Factura factura = new Factura(CodigoFactura, DateTime.Now, total, cliente, "1");
                    string msg = servicioFactura.Insert(factura);
                    int msg2 = servicioVenta.Insert(ventasFacturando);
                    string msg3 = servicioMateriaPrima.Update(materiasPrimasParaActualizar);
                    MessageBox.Show("Factura: " + msg + " vendidos: " + msg2 + " actualizados: " + msg3);
                    generarPDF();
                    LimpiarFactura();
                }
                else
                {
                    MessageBox.Show("Saldo Insuficiente");
                }



            }
            else 
            
            {
                MessageBox.Show("CLiente no registrado");
            }

        }

        public void LimpiarFactura() 
        {

             productoSeleccionado = new ProductoDTO();
             subTotal = 0;
             total = 0;
            dineroDado = 0;
             cambio = 0;
           ventasFacturando = new List<Vendidos>();
            CodigoFactura = null;
             materiasPrimasActuales = new List<MateriaPrima>();
             ingredientesEnProceso = new List<Ingrediente>();
             materiasPrimasParaActualizar = new List<MateriaPrima>();
            listaFactura.Items.Clear();
            cargarProductosVenta();
            txtProductoFactura.Text = "";
            tabControl1.SelectedIndex = 3;
        }

        private void desplegar_Click(object sender, EventArgs e)
        {
            if (groupCliente.Height == 81)
            {
                groupCliente.Height = 320;

              
            }
            else
            {
                groupCliente.Height = 81;
               

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime inicio = DateDesde.Value;
            DateTime fin = DateHasta.Value;
            if (inicio <= fin)
            {
                List<Factura> facturas = servicioFactura.obtenerFacturasDesdeHasta(inicio,fin);
                if (facturas.Count > 0)
                {
                    List<FacturaDTO> facturasDTO = servicioFactura.convertirDTO(facturas);
                    grillaFacturas.DataSource = facturasDTO;
                    grillaFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    FacturasInfo infoFacturas = servicioFactura.infoFacturas(facturas);
                    labelInfo1.Text = "Número de facturas: "+infoFacturas.ventas;
                    labelInfo2.Text = "Dinero ingresado: "+infoFacturas.dinero +" $";
                    labelInfo3.Text = "";
                    grillaVendidos.DataSource = new List<VendidoDTO>();
                }
                else 
                {
                    MessageBox.Show("No se encuentran registros de esas fechas");
                    grillaFacturas.DataSource = facturas;
                    labelInfo1.Text = "Número de facturas: " + 0;
                    labelInfo2.Text = "Dinero ingresado: " + 0 + " $";
                    labelInfo3.Text = "";

                }

            }
            else
            {
                Console.WriteLine("La fecha de inicio debe ser anterior a la fecha final.");
            }

        }

        private void grillaFacturas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = grillaFacturas.Rows[e.RowIndex];
                FacturaDTO facturaselectedDTO = (FacturaDTO)row.DataBoundItem;
                List<Factura> facturas = servicioFactura.convertirDTOaNormal(new List<FacturaDTO>(){ facturaselectedDTO});
                Factura facturaselected = facturas[0];
                List<Vendidos> ventas = servicioVenta.obtenerVentasConFactura(facturaselected.id_factura);
                List<VendidoDTO> vendidosDTO = servicioVenta.convertriDTO(ventas);
                grillaVendidos.DataSource= vendidosDTO;
                grillaVendidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
               
                labelInfo1.Text = "Código Factura: " + facturaselected.id_factura;
                labelInfo2.Text = $"Dinero ingresado: {facturaselected.precioTotal} $";
                labelInfo3.Text = "Cliente: " + facturaselected.cliente.nombre;
            }
                

        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }


        //Funciones extra
        public string ObtenerDigitosFecha(string fecha)
        {
            string fechaSinSlash = fecha.Replace("/", "");
            string digitosFecha = "";

            foreach (char caracter in fechaSinSlash)
            {
                if (char.IsDigit(caracter))
                {
                    digitosFecha += caracter;
                }
            }

            return digitosFecha;
        }
        public void generarPDF()
        {
            Document document = new Document();
            string folderPath = @"facturas";
            string filePath = Path.Combine(folderPath, CodigoFactura + ".pdf");

            Directory.CreateDirectory(folderPath);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

            document.Open();
            // Información adicional
            Paragraph infoParagraph = new Paragraph();
            infoParagraph.Alignment = Element.ALIGN_CENTER;

            infoParagraph.Add(new Chunk("VILAS POP FACTURA"));
            infoParagraph.Add(Chunk.NEWLINE);

            infoParagraph.Add(new Chunk("Dirección: Calle 18Abis #35-55, Valledupar"));
            infoParagraph.Add(Chunk.NEWLINE);
            infoParagraph.Add(new Chunk("Número de factura: " + CodigoFactura));
            infoParagraph.Add(Chunk.NEWLINE);
            infoParagraph.Add(Chunk.NEWLINE);
            document.Add(infoParagraph);


            Paragraph paragraph = new Paragraph();
            paragraph.Alignment = Element.ALIGN_RIGHT;
            foreach (var item in listaFactura.Items)
            {
                paragraph.Add(new Chunk(item.ToString()));
                paragraph.Add(Chunk.NEWLINE);
            }

            // Agregar el párrafo al documento
            document.Add(paragraph);
            Paragraph moneyParagraph = new Paragraph();
            moneyParagraph.Alignment = Element.ALIGN_RIGHT;
            moneyParagraph.Add(Chunk.NEWLINE);
            moneyParagraph.Add(new Chunk("Dinero recibido: " + txtDinero.Text + " $"));
            moneyParagraph.Add(Chunk.NEWLINE);
            moneyParagraph.Add(new Chunk("Cambio entregado: " + labelCambio.Text));

            document.Add(moneyParagraph);

            // Cerrar el documento
            document.Close();

            Process.Start(filePath);

            // Mostrar un mensaje de éxito
            MessageBox.Show("Archivo PDF generado correctamente.");

        }

        private void txtNombreMateriaP_KeyUp(object sender, KeyEventArgs e)
        {
            
        }
        
        private bool EsNulo (string valor)
        {
            if ((valor == null) || (valor == "")) return true;
            return false;
        }

        private bool EsNumero(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == (char)Keys.Back)
                {
                    e.Handled = false;
                    return e.Handled;
                }
            }
            else 
            {
                e.Handled= false;
            }
            return e.Handled;
        }

        private void txtDinero_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = EsNumero(e);
        }

        private void txtCantidadIngrediente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = EsNumero(e);
        }

        private void txtPrecioProductoR_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = EsNumero(e);
        }

        private void registrarClientes_Click(object sender, EventArgs e)
        {

            Cliente cliente = servicioCLiente.BuscarCliente(txtCedulaCliente.Text);
            if (cliente == null)
            {
                Cliente clienteNuevo = new Cliente(
                    txtCedulaCliente.Text,
                    txtNombreCliente.Text,
                    txtApellidoCliente.Text,
                    txtTelefonoCliente.Text,
                    txtCorreoCliente.Text);

                string msg = servicioCLiente.Insert(clienteNuevo);

                MessageBox.Show("Cliente registrado");
            }
            else 
            {
                MessageBox.Show("Cliente ya registrado");
            
            }
            groupCliente.Height = 81;
        }



        private void btnFacturacion_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 8;
        }

        private void btnMateriaP_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 6;
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }

        private void radioGramos_CheckedChanged(object sender, EventArgs e)
        {
            txtGramosM.Enabled = radioGramos.Checked;
            txtMililitrosM.Enabled = radioMililitros.Checked;
            txtMililitrosM.Text = "0";
            txtUnidadesM.Enabled = radioUnidades.Checked;
            txtUnidadesM.Text = "0";
            //if (radioGramos.Checked)
            //{

            //    DesactivarCamposRadioButton(txtMililitrosM);
            //    DesactivarCamposRadioButton(txtUnidadesM);
            //}
        }


        private void DesactivarCamposRadioButton(TextBox CampoNoSeleccionado)
        {
            CampoNoSeleccionado.Enabled = false;
            CampoNoSeleccionado.Text = string.Empty;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void radioMililitros_CheckedChanged(object sender, EventArgs e)
        {
            txtGramosM.Enabled = radioGramos.Checked;
            txtGramosM.Text = "0";
            txtMililitrosM.Enabled = radioMililitros.Checked;
            txtUnidadesM.Enabled = radioUnidades.Checked;
            txtUnidadesM.Text = "0";
        }

        private void radioUnidades_CheckedChanged(object sender, EventArgs e)
        {
            txtGramosM.Enabled = radioGramos.Checked;
            txtGramosM.Text = "0";
            txtMililitrosM.Enabled = radioMililitros.Checked;
            txtMililitrosM.Text = "0";
            txtUnidadesM.Enabled = radioUnidades.Checked;
        }

        public void Regresar() 
        {
            tabControl1.SelectedIndex = 0;
            CargarProveedores();
            CargarMateriasP();
            cargarProductosVenta();
            cargarComboIngredientes();

        }

        private void btnRegresar5_Click(object sender, EventArgs e)
        {
            Regresar();
        }

        private void btnRegresar4_Click(object sender, EventArgs e)
        {
            Regresar();
        }

        private void btnRegresar3_Click(object sender, EventArgs e)
        {
            Regresar();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Regresar();
        }

        private void btnRegresar2_Click(object sender, EventArgs e)
        {
            Regresar();

        }

        private void productosTabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = tablaProductos.Rows[e.RowIndex];
            productoSeleccionado = (ProductoDTO)row.DataBoundItem;
            productName.Text = productoSeleccionado.PRODUCTO;
            ProductPrice.Text = productoSeleccionado.PRECIO;
            idProducto.Text = productoSeleccionado.ID;
            List<Ingrediente> ingrdientes = new List<Ingrediente>();
            ingrdientes = servicioIngrediente.obtenerIngredientesPorReceta(productoSeleccionado.ID);
            List<IngredienteDTO> ingredientesDTO = servicioIngrediente.convertirDTO(ingrdientes);
            tablaIngredientes.DataSource = ingredientesDTO;

        }

        private void btnActualizarProducto_Click(object sender, EventArgs e)
        {
            if (!EsNulo(productName.Text) && (!EsNulo(ProductPrice.Text)))
            {
                if (float.Parse(ProductPrice.Text) != 0)
                {
                    Producto producto = new Producto(idProducto.Text, productName.Text, float.Parse(ProductPrice.Text));
                    servicioProducto.Update(producto);
                    cargarProductosVenta();
                    MessageBox.Show("Producto Actualizado");

                }
                else 
                {
                    MessageBox.Show("No puede tener un costo de 0");

                }


            }
            else 
            {
                MessageBox.Show("Debe seleccionar un producto");
            }
            }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {

            if (ingredienteSelected != null)
            {
                DialogResult result = MessageBox.Show("¿Desea eliminar el ingrediente?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    servicioIngrediente.Eliminar(ingredienteSelected);
                    MessageBox.Show("Ingrediente Eliminado");
                    List<Ingrediente> ingrdientes = new List<Ingrediente>();
                    ingrdientes = servicioIngrediente.obtenerIngredientesPorReceta(idProducto.Text);
                    List<IngredienteDTO> ingredientesDTO = servicioIngrediente.convertirDTO(ingrdientes);
                    tablaIngredientes.DataSource = ingredientesDTO;
                    ingredienteSelected = null;

                }
               

            }
            else 
            {
                MessageBox.Show("Debe seleccionar un ingrediente");

            }


            

            
        }

        private void tablaIngredientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = tablaIngredientes.Rows[e.RowIndex];
            IngredienteDTO ingredienteDTO = (IngredienteDTO)row.DataBoundItem;
            List<Ingrediente> ingredientes =  servicioIngrediente.convertirDTOaNormal(new List<IngredienteDTO>(){ ingredienteDTO});
            ingredienteSelected = ingredientes[0];
        }

        private void btnIngresar2_Click(object sender, EventArgs e)
        {
            if ((!EsNulo(txtCantidadIngr.Text)))
            {
                Ingrediente ingrediente = new Ingrediente();
                MateriaPrima materiaP = servicioMateriaPrima.obtenerMateriaPrimaConNombre(comboMateriaP2.Text);
                List<Ingrediente> ingrs = servicioIngrediente.obtenerIngredientesPorReceta(productoSeleccionado.ID);
                bool continuar = true;
                foreach(Ingrediente ingr in ingrs) 
                {
                    if (ingr.idmateriaPrima.Equals(materiaP.idMateriaPrima)) 
                    {

                        continuar = false;
                    }
                }
                if (continuar)
                {
                    ingrediente.idmateriaPrima = materiaP.idMateriaPrima;

                    ingrediente.idReceta = idProducto.Text;


                    if (comboUnidad2.Text.Equals("gr"))
                    {
                        ingrediente.gramos = float.Parse(txtCantidadIngr.Text);
                        ingrediente.unidades = 0;
                        ingrediente.mililitros = 0;
                    }
                    else if (comboUnidad2.Text.Equals("ml"))
                    {
                        ingrediente.gramos = 0;
                        ingrediente.unidades = 0;
                        ingrediente.mililitros = float.Parse(txtCantidadIngr.Text);
                    }
                    else
                    {
                        ingrediente.gramos = 0;
                        ingrediente.unidades = Int32.Parse(txtCantidadIngr.Text);
                        ingrediente.mililitros = 0;

                    }

                    string msg = servicioIngrediente.Insert(new List<Ingrediente>() { ingrediente });
                    MessageBox.Show("Ingrediente Ingresado");
                    List<Ingrediente> ingrdientes = new List<Ingrediente>();
                    ingrdientes = servicioIngrediente.obtenerIngredientesPorReceta(idProducto.Text);
                    List<IngredienteDTO> ingredientesDTO = servicioIngrediente.convertirDTO(ingrdientes);
                    tablaIngredientes.DataSource = ingredientesDTO;
                    ingredienteSelected = null;

                }
                else 
                {

                    MessageBox.Show("Ya este ingrediente está incluido, eliminalo e ingresalo con la nueva cantidad.");

                }


            }
            else 
            {
                MessageBox.Show("Debe ingresar una cantidad");


            }


        }

        private void grillaMateriaP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = grillaMateriaP.Rows[e.RowIndex];
            MateriaPrimaDTO materiaPDTO = (MateriaPrimaDTO)row.DataBoundItem;
            materiaPrimaSeleccionada = servicioMateriaPrima.obtenerMateriaPrimaConID(materiaPDTO.ID);

        }

        private void btnEliminarMP_Click(object sender, EventArgs e)
        {
            if (materiaPrimaSeleccionada != null)
            {
                DialogResult result = MessageBox.Show("¿Desea desechar la materia Prima?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) 
                {
                string msg = servicioMateriaPrima.UpdateCero(Int32.Parse(materiaPrimaSeleccionada.idMateriaPrima));
                materiaPrimaSeleccionada = null;
                MessageBox.Show("Materia Prima eliminada");
                
                }

            }
            else 
            {
                MessageBox.Show("Debe seleccionar una materia prima");

            }
        }

        private void btnRegresarP_Click(object sender, EventArgs e)
        {
            Regresar();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btnCancelarFactura_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea desechar la factura actual?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LimpiarFactura();

            }
          
        }

        private void comboMateriaP_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                MateriaPrima materiaP = servicioMateriaPrima.obtenerMateriaPrimaConNombre(comboMateriaP.Text);
                if (materiaP != null) 
                {
                if (materiaP.gramos > 0)
                {
                    comboUnidad.Items.Clear();
                    comboUnidad.Text = "gr";
                }
                else if (materiaP.mililitros > 0)
                {
                    comboUnidad.Items.Clear();
                    comboUnidad.Text = "ml";
                }
                else
                {
                    comboUnidad.Items.Clear();
                    comboUnidad.Text = "ud";
                }
            }
               

            
         
        }

        private void comboMateriaP2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MateriaPrima materiaP = servicioMateriaPrima.obtenerMateriaPrimaConNombre(comboMateriaP2.Text);
            if (materiaP != null)
            {
                if (materiaP.gramos > 0)
                {
                    comboUnidad2.Items.Clear();
                    comboUnidad2.Text = "gr";
                }
                else if (materiaP.mililitros > 0)
                {
                    comboUnidad2.Items.Clear();
                    comboUnidad2.Text = "ml";
                }
                else
                {
                    comboUnidad2.Items.Clear();
                    comboUnidad2.Text = "ud";
                }
            }

        }

        private void txtPrecioProductoR_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrecioProductoR_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = EsNumero(e);
        }

        private void ProductPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = EsNumero(e);
        }

        private void txtCostoM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = EsNumero(e);
        }

        private void txtGramosM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = EsNumero(e);
        }

        private void txtMililitrosM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = EsNumero(e);
        }

        private void txtUnidadesM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = EsNumero(e);
        }
    }
}
