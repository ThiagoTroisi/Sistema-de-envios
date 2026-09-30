namespace SistemaDeEnviosGUI
{
    partial class GestionEnviosForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtDescripcion = new TextBox();
            lblPaquete = new Label();
            lblDescripcion = new Label();
            lblPeso = new Label();
            txtPeso = new TextBox();
            lblAncho = new Label();
            txtAncho = new TextBox();
            lblAlto = new Label();
            txtAlto = new TextBox();
            lblLargo = new Label();
            txtLargo = new TextBox();
            lblEmailR = new Label();
            txtEmailR = new TextBox();
            lblApellidoR = new Label();
            txtApellidoR = new TextBox();
            lblNombreR = new Label();
            txtNombreR = new TextBox();
            lblDNIR = new Label();
            txtDNIR = new TextBox();
            lblRemitente = new Label();
            lblTelefonoR = new Label();
            txtTelefonoR = new TextBox();
            btnBuscarRemitente = new Button();
            btnBuscarDestinatario = new Button();
            lblTelefonoD = new Label();
            txtTelefonoD = new TextBox();
            lblEmailD = new Label();
            txtEmailD = new TextBox();
            lblApellidoD = new Label();
            txtApellidoD = new TextBox();
            lblNombreD = new Label();
            txtNombreD = new TextBox();
            lblDNID = new Label();
            txtDNID = new TextBox();
            lblDestinatario = new Label();
            btnRegistrarEnvio = new Button();
            lblProvincia = new Label();
            txtProvincia = new TextBox();
            lblCP = new Label();
            txtCP = new TextBox();
            lblCiudad = new Label();
            txtCiudad = new TextBox();
            lblDireccion = new Label();
            txtDireccion = new TextBox();
            lblDestino = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnSalir = new Button();
            dataGridViewEnvios = new DataGridView();
            lblTodosLosEnvios = new Label();
            btnCancelarEnvio = new Button();
            btnModificarEnvio = new Button();
            btnAplicar = new Button();
            btnCancelar = new Button();
            dateTimePickerDesde = new DateTimePicker();
            dateTimePickerHasta = new DateTimePicker();
            cboEstado = new ComboBox();
            lblEstado = new Label();
            lblCodigoSeguimiento = new Label();
            txtCodigoSeguimiento = new TextBox();
            label7 = new Label();
            lblDesde = new Label();
            lblHasta = new Label();
            lblDNIRFiltro = new Label();
            lblDNIDFiltro = new Label();
            txtDNIRFiltro = new TextBox();
            txtDNIDFiltro = new TextBox();
            btnLimpiarFiltros = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnvios).BeginInit();
            SuspendLayout();
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(87, 32);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(214, 23);
            txtDescripcion.TabIndex = 1;
            // 
            // lblPaquete
            // 
            lblPaquete.AutoSize = true;
            lblPaquete.Location = new Point(12, 9);
            lblPaquete.Name = "lblPaquete";
            lblPaquete.Size = new Size(56, 15);
            lblPaquete.TabIndex = 2;
            lblPaquete.Text = "PAQUETE";
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(12, 35);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(69, 15);
            lblDescripcion.TabIndex = 3;
            lblDescripcion.Text = "Descripción";
            // 
            // lblPeso
            // 
            lblPeso.AutoSize = true;
            lblPeso.Location = new Point(39, 64);
            lblPeso.Name = "lblPeso";
            lblPeso.Size = new Size(32, 15);
            lblPeso.TabIndex = 5;
            lblPeso.Text = "Peso";
            // 
            // txtPeso
            // 
            txtPeso.Location = new Point(87, 61);
            txtPeso.Name = "txtPeso";
            txtPeso.Size = new Size(111, 23);
            txtPeso.TabIndex = 4;
            // 
            // lblAncho
            // 
            lblAncho.AutoSize = true;
            lblAncho.Location = new Point(39, 122);
            lblAncho.Name = "lblAncho";
            lblAncho.Size = new Size(42, 15);
            lblAncho.TabIndex = 9;
            lblAncho.Text = "Ancho";
            // 
            // txtAncho
            // 
            txtAncho.Location = new Point(87, 119);
            txtAncho.Name = "txtAncho";
            txtAncho.Size = new Size(111, 23);
            txtAncho.TabIndex = 8;
            // 
            // lblAlto
            // 
            lblAlto.AutoSize = true;
            lblAlto.Location = new Point(39, 93);
            lblAlto.Name = "lblAlto";
            lblAlto.Size = new Size(29, 15);
            lblAlto.TabIndex = 7;
            lblAlto.Text = "Alto";
            // 
            // txtAlto
            // 
            txtAlto.Location = new Point(87, 90);
            txtAlto.Name = "txtAlto";
            txtAlto.Size = new Size(111, 23);
            txtAlto.TabIndex = 6;
            // 
            // lblLargo
            // 
            lblLargo.AutoSize = true;
            lblLargo.Location = new Point(39, 151);
            lblLargo.Name = "lblLargo";
            lblLargo.Size = new Size(37, 15);
            lblLargo.TabIndex = 11;
            lblLargo.Text = "Largo";
            // 
            // txtLargo
            // 
            txtLargo.Location = new Point(87, 148);
            txtLargo.Name = "txtLargo";
            txtLargo.Size = new Size(111, 23);
            txtLargo.TabIndex = 10;
            // 
            // lblEmailR
            // 
            lblEmailR.AutoSize = true;
            lblEmailR.Location = new Point(12, 365);
            lblEmailR.Name = "lblEmailR";
            lblEmailR.Size = new Size(36, 15);
            lblEmailR.TabIndex = 22;
            lblEmailR.Text = "Email";
            // 
            // txtEmailR
            // 
            txtEmailR.Enabled = false;
            txtEmailR.Location = new Point(70, 362);
            txtEmailR.Name = "txtEmailR";
            txtEmailR.Size = new Size(128, 23);
            txtEmailR.TabIndex = 21;
            // 
            // lblApellidoR
            // 
            lblApellidoR.AutoSize = true;
            lblApellidoR.Location = new Point(12, 307);
            lblApellidoR.Name = "lblApellidoR";
            lblApellidoR.Size = new Size(51, 15);
            lblApellidoR.TabIndex = 20;
            lblApellidoR.Text = "Apellido";
            // 
            // txtApellidoR
            // 
            txtApellidoR.Enabled = false;
            txtApellidoR.Location = new Point(70, 304);
            txtApellidoR.Name = "txtApellidoR";
            txtApellidoR.Size = new Size(128, 23);
            txtApellidoR.TabIndex = 19;
            // 
            // lblNombreR
            // 
            lblNombreR.AutoSize = true;
            lblNombreR.Location = new Point(12, 278);
            lblNombreR.Name = "lblNombreR";
            lblNombreR.Size = new Size(51, 15);
            lblNombreR.TabIndex = 18;
            lblNombreR.Text = "Nombre";
            // 
            // txtNombreR
            // 
            txtNombreR.Enabled = false;
            txtNombreR.Location = new Point(70, 275);
            txtNombreR.Name = "txtNombreR";
            txtNombreR.Size = new Size(128, 23);
            txtNombreR.TabIndex = 17;
            // 
            // lblDNIR
            // 
            lblDNIR.AutoSize = true;
            lblDNIR.Location = new Point(12, 249);
            lblDNIR.Name = "lblDNIR";
            lblDNIR.Size = new Size(27, 15);
            lblDNIR.TabIndex = 16;
            lblDNIR.Text = "DNI";
            // 
            // txtDNIR
            // 
            txtDNIR.Location = new Point(70, 246);
            txtDNIR.Name = "txtDNIR";
            txtDNIR.Size = new Size(128, 23);
            txtDNIR.TabIndex = 15;
            txtDNIR.TextChanged += txtDNIR_TextChanged;
            // 
            // lblRemitente
            // 
            lblRemitente.AutoSize = true;
            lblRemitente.Location = new Point(12, 224);
            lblRemitente.Name = "lblRemitente";
            lblRemitente.Size = new Size(67, 15);
            lblRemitente.TabIndex = 13;
            lblRemitente.Text = "REMITENTE";
            // 
            // lblTelefonoR
            // 
            lblTelefonoR.AutoSize = true;
            lblTelefonoR.Location = new Point(12, 336);
            lblTelefonoR.Name = "lblTelefonoR";
            lblTelefonoR.Size = new Size(52, 15);
            lblTelefonoR.TabIndex = 24;
            lblTelefonoR.Text = "Teléfono";
            // 
            // txtTelefonoR
            // 
            txtTelefonoR.Enabled = false;
            txtTelefonoR.Location = new Point(70, 333);
            txtTelefonoR.Name = "txtTelefonoR";
            txtTelefonoR.Size = new Size(128, 23);
            txtTelefonoR.TabIndex = 23;
            // 
            // btnBuscarRemitente
            // 
            btnBuscarRemitente.Location = new Point(204, 246);
            btnBuscarRemitente.Name = "btnBuscarRemitente";
            btnBuscarRemitente.Size = new Size(82, 23);
            btnBuscarRemitente.TabIndex = 25;
            btnBuscarRemitente.Text = "Buscar";
            btnBuscarRemitente.UseVisualStyleBackColor = true;
            btnBuscarRemitente.Click += btnBuscarRemitente_Click;
            // 
            // btnBuscarDestinatario
            // 
            btnBuscarDestinatario.Location = new Point(533, 246);
            btnBuscarDestinatario.Name = "btnBuscarDestinatario";
            btnBuscarDestinatario.Size = new Size(82, 23);
            btnBuscarDestinatario.TabIndex = 37;
            btnBuscarDestinatario.Text = "Buscar";
            btnBuscarDestinatario.UseVisualStyleBackColor = true;
            btnBuscarDestinatario.Click += btnBuscarDestinatario_Click;
            // 
            // lblTelefonoD
            // 
            lblTelefonoD.AutoSize = true;
            lblTelefonoD.Location = new Point(341, 336);
            lblTelefonoD.Name = "lblTelefonoD";
            lblTelefonoD.Size = new Size(52, 15);
            lblTelefonoD.TabIndex = 36;
            lblTelefonoD.Text = "Teléfono";
            // 
            // txtTelefonoD
            // 
            txtTelefonoD.Enabled = false;
            txtTelefonoD.Location = new Point(399, 333);
            txtTelefonoD.Name = "txtTelefonoD";
            txtTelefonoD.Size = new Size(128, 23);
            txtTelefonoD.TabIndex = 35;
            // 
            // lblEmailD
            // 
            lblEmailD.AutoSize = true;
            lblEmailD.Location = new Point(341, 365);
            lblEmailD.Name = "lblEmailD";
            lblEmailD.Size = new Size(36, 15);
            lblEmailD.TabIndex = 34;
            lblEmailD.Text = "Email";
            // 
            // txtEmailD
            // 
            txtEmailD.Enabled = false;
            txtEmailD.Location = new Point(399, 362);
            txtEmailD.Name = "txtEmailD";
            txtEmailD.Size = new Size(128, 23);
            txtEmailD.TabIndex = 33;
            // 
            // lblApellidoD
            // 
            lblApellidoD.AutoSize = true;
            lblApellidoD.Location = new Point(341, 307);
            lblApellidoD.Name = "lblApellidoD";
            lblApellidoD.Size = new Size(51, 15);
            lblApellidoD.TabIndex = 32;
            lblApellidoD.Text = "Apellido";
            // 
            // txtApellidoD
            // 
            txtApellidoD.Enabled = false;
            txtApellidoD.Location = new Point(399, 304);
            txtApellidoD.Name = "txtApellidoD";
            txtApellidoD.Size = new Size(128, 23);
            txtApellidoD.TabIndex = 31;
            // 
            // lblNombreD
            // 
            lblNombreD.AutoSize = true;
            lblNombreD.Location = new Point(341, 278);
            lblNombreD.Name = "lblNombreD";
            lblNombreD.Size = new Size(51, 15);
            lblNombreD.TabIndex = 30;
            lblNombreD.Text = "Nombre";
            // 
            // txtNombreD
            // 
            txtNombreD.Enabled = false;
            txtNombreD.Location = new Point(399, 275);
            txtNombreD.Name = "txtNombreD";
            txtNombreD.Size = new Size(128, 23);
            txtNombreD.TabIndex = 29;
            // 
            // lblDNID
            // 
            lblDNID.AutoSize = true;
            lblDNID.Location = new Point(341, 249);
            lblDNID.Name = "lblDNID";
            lblDNID.Size = new Size(27, 15);
            lblDNID.TabIndex = 28;
            lblDNID.Text = "DNI";
            // 
            // txtDNID
            // 
            txtDNID.Location = new Point(399, 246);
            txtDNID.Name = "txtDNID";
            txtDNID.Size = new Size(128, 23);
            txtDNID.TabIndex = 27;
            txtDNID.TextChanged += txtDNID_TextChanged;
            // 
            // lblDestinatario
            // 
            lblDestinatario.AutoSize = true;
            lblDestinatario.Location = new Point(341, 224);
            lblDestinatario.Name = "lblDestinatario";
            lblDestinatario.Size = new Size(84, 15);
            lblDestinatario.TabIndex = 26;
            lblDestinatario.Text = "DESTINATARIO";
            // 
            // btnRegistrarEnvio
            // 
            btnRegistrarEnvio.Location = new Point(679, 333);
            btnRegistrarEnvio.Name = "btnRegistrarEnvio";
            btnRegistrarEnvio.Size = new Size(111, 39);
            btnRegistrarEnvio.TabIndex = 38;
            btnRegistrarEnvio.Text = "Registrar envío";
            btnRegistrarEnvio.UseVisualStyleBackColor = true;
            btnRegistrarEnvio.Click += btnRegistrarEnvio_Click;
            // 
            // lblProvincia
            // 
            lblProvincia.AutoSize = true;
            lblProvincia.Location = new Point(339, 121);
            lblProvincia.Name = "lblProvincia";
            lblProvincia.Size = new Size(56, 15);
            lblProvincia.TabIndex = 47;
            lblProvincia.Text = "Provincia";
            // 
            // txtProvincia
            // 
            txtProvincia.Location = new Point(401, 118);
            txtProvincia.Name = "txtProvincia";
            txtProvincia.Size = new Size(128, 23);
            txtProvincia.TabIndex = 46;
            // 
            // lblCP
            // 
            lblCP.AutoSize = true;
            lblCP.Location = new Point(339, 92);
            lblCP.Name = "lblCP";
            lblCP.Size = new Size(22, 15);
            lblCP.TabIndex = 45;
            lblCP.Text = "CP";
            // 
            // txtCP
            // 
            txtCP.Location = new Point(401, 89);
            txtCP.Name = "txtCP";
            txtCP.Size = new Size(128, 23);
            txtCP.TabIndex = 44;
            // 
            // lblCiudad
            // 
            lblCiudad.AutoSize = true;
            lblCiudad.Location = new Point(339, 63);
            lblCiudad.Name = "lblCiudad";
            lblCiudad.Size = new Size(45, 15);
            lblCiudad.TabIndex = 43;
            lblCiudad.Text = "Ciudad";
            // 
            // txtCiudad
            // 
            txtCiudad.Location = new Point(401, 60);
            txtCiudad.Name = "txtCiudad";
            txtCiudad.Size = new Size(128, 23);
            txtCiudad.TabIndex = 42;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Location = new Point(339, 34);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(57, 15);
            lblDireccion.TabIndex = 41;
            lblDireccion.Text = "Dirección";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(401, 31);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(128, 23);
            txtDireccion.TabIndex = 40;
            // 
            // lblDestino
            // 
            lblDestino.AutoSize = true;
            lblDestino.Location = new Point(339, 9);
            lblDestino.Name = "lblDestino";
            lblDestino.Size = new Size(54, 15);
            lblDestino.TabIndex = 39;
            lblDestino.Text = "DESTINO";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(204, 63);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 48;
            label1.Text = "kilogramos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(204, 92);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 49;
            label2.Text = "metros";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(204, 121);
            label3.Name = "label3";
            label3.Size = new Size(44, 15);
            label3.TabIndex = 50;
            label3.Text = "metros";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(204, 151);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 51;
            label4.Text = "metros";
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(1485, 483);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(111, 39);
            btnSalir.TabIndex = 52;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // dataGridViewEnvios
            // 
            dataGridViewEnvios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEnvios.Location = new Point(679, 31);
            dataGridViewEnvios.Name = "dataGridViewEnvios";
            dataGridViewEnvios.Size = new Size(917, 296);
            dataGridViewEnvios.TabIndex = 53;
            dataGridViewEnvios.SelectionChanged += dataGridViewEnvios_SelectionChanged;
            // 
            // lblTodosLosEnvios
            // 
            lblTodosLosEnvios.AutoSize = true;
            lblTodosLosEnvios.Location = new Point(679, 9);
            lblTodosLosEnvios.Name = "lblTodosLosEnvios";
            lblTodosLosEnvios.Size = new Size(93, 15);
            lblTodosLosEnvios.TabIndex = 54;
            lblTodosLosEnvios.Text = "Todos los envíos";
            // 
            // btnCancelarEnvio
            // 
            btnCancelarEnvio.Location = new Point(796, 333);
            btnCancelarEnvio.Name = "btnCancelarEnvio";
            btnCancelarEnvio.Size = new Size(111, 39);
            btnCancelarEnvio.TabIndex = 55;
            btnCancelarEnvio.Text = "Cancelar envío";
            btnCancelarEnvio.UseVisualStyleBackColor = true;
            btnCancelarEnvio.Click += btnCancelarEnvio_Click;
            // 
            // btnModificarEnvio
            // 
            btnModificarEnvio.Location = new Point(913, 333);
            btnModificarEnvio.Name = "btnModificarEnvio";
            btnModificarEnvio.Size = new Size(111, 39);
            btnModificarEnvio.TabIndex = 56;
            btnModificarEnvio.Text = "Modificar envío";
            btnModificarEnvio.UseVisualStyleBackColor = true;
            btnModificarEnvio.Click += btnModificarEnvio_Click;
            // 
            // btnAplicar
            // 
            btnAplicar.Location = new Point(1368, 333);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(111, 39);
            btnAplicar.TabIndex = 57;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = true;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(1485, 333);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(111, 39);
            btnCancelar.TabIndex = 58;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // dateTimePickerDesde
            // 
            dateTimePickerDesde.Checked = false;
            dateTimePickerDesde.Location = new Point(1021, 415);
            dateTimePickerDesde.Name = "dateTimePickerDesde";
            dateTimePickerDesde.Size = new Size(234, 23);
            dateTimePickerDesde.TabIndex = 59;
            dateTimePickerDesde.ValueChanged += dateTimePickerDesde_ValueChanged;
            // 
            // dateTimePickerHasta
            // 
            dateTimePickerHasta.Checked = false;
            dateTimePickerHasta.Location = new Point(1021, 480);
            dateTimePickerHasta.Name = "dateTimePickerHasta";
            dateTimePickerHasta.Size = new Size(234, 23);
            dateTimePickerHasta.TabIndex = 60;
            dateTimePickerHasta.ValueChanged += dateTimePickerHasta_ValueChanged;
            // 
            // cboEstado
            // 
            cboEstado.FormattingEnabled = true;
            cboEstado.Location = new Point(679, 415);
            cboEstado.Name = "cboEstado";
            cboEstado.Size = new Size(134, 23);
            cboEstado.TabIndex = 61;
            cboEstado.SelectedIndexChanged += cboEstado_SelectedIndexChanged;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(679, 397);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(45, 15);
            lblEstado.TabIndex = 62;
            lblEstado.Text = "Estado:";
            // 
            // lblCodigoSeguimiento
            // 
            lblCodigoSeguimiento.AutoSize = true;
            lblCodigoSeguimiento.Location = new Point(679, 462);
            lblCodigoSeguimiento.Name = "lblCodigoSeguimiento";
            lblCodigoSeguimiento.Size = new Size(134, 15);
            lblCodigoSeguimiento.TabIndex = 63;
            lblCodigoSeguimiento.Text = "Código de seguimiento:";
            // 
            // txtCodigoSeguimiento
            // 
            txtCodigoSeguimiento.Location = new Point(717, 480);
            txtCodigoSeguimiento.Name = "txtCodigoSeguimiento";
            txtCodigoSeguimiento.Size = new Size(96, 23);
            txtCodigoSeguimiento.TabIndex = 64;
            txtCodigoSeguimiento.TextChanged += txtCodigoSeguimiento_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(679, 483);
            label7.Name = "label7";
            label7.Size = new Size(37, 15);
            label7.TabIndex = 65;
            label7.Text = "ENV -";
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Location = new Point(1021, 397);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(42, 15);
            lblDesde.TabIndex = 66;
            lblDesde.Text = "Desde:";
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Location = new Point(1021, 462);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(40, 15);
            lblHasta.TabIndex = 67;
            lblHasta.Text = "Hasta:";
            // 
            // lblDNIRFiltro
            // 
            lblDNIRFiltro.AutoSize = true;
            lblDNIRFiltro.Location = new Point(852, 397);
            lblDNIRFiltro.Name = "lblDNIRFiltro";
            lblDNIRFiltro.Size = new Size(84, 15);
            lblDNIRFiltro.TabIndex = 68;
            lblDNIRFiltro.Text = "DNI remitente:";
            // 
            // lblDNIDFiltro
            // 
            lblDNIDFiltro.AutoSize = true;
            lblDNIDFiltro.Location = new Point(852, 462);
            lblDNIDFiltro.Name = "lblDNIDFiltro";
            lblDNIDFiltro.Size = new Size(95, 15);
            lblDNIDFiltro.TabIndex = 69;
            lblDNIDFiltro.Text = "DNI destinatario:";
            // 
            // txtDNIRFiltro
            // 
            txtDNIRFiltro.Location = new Point(852, 415);
            txtDNIRFiltro.Name = "txtDNIRFiltro";
            txtDNIRFiltro.Size = new Size(128, 23);
            txtDNIRFiltro.TabIndex = 70;
            txtDNIRFiltro.TextChanged += txtDNIRFiltro_TextChanged;
            // 
            // txtDNIDFiltro
            // 
            txtDNIDFiltro.Location = new Point(852, 480);
            txtDNIDFiltro.Name = "txtDNIDFiltro";
            txtDNIDFiltro.Size = new Size(128, 23);
            txtDNIDFiltro.TabIndex = 71;
            txtDNIDFiltro.TextChanged += txtDNIDFiltro_TextChanged;
            // 
            // btnLimpiarFiltros
            // 
            btnLimpiarFiltros.Location = new Point(1144, 333);
            btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            btnLimpiarFiltros.Size = new Size(111, 39);
            btnLimpiarFiltros.TabIndex = 72;
            btnLimpiarFiltros.Text = "Limpiar filtros";
            btnLimpiarFiltros.UseVisualStyleBackColor = true;
            btnLimpiarFiltros.Click += btnLimpiarFiltros_Click;
            // 
            // GestionEnviosForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1608, 534);
            Controls.Add(btnLimpiarFiltros);
            Controls.Add(txtDNIDFiltro);
            Controls.Add(txtDNIRFiltro);
            Controls.Add(lblDNIDFiltro);
            Controls.Add(lblDNIRFiltro);
            Controls.Add(lblHasta);
            Controls.Add(lblDesde);
            Controls.Add(label7);
            Controls.Add(txtCodigoSeguimiento);
            Controls.Add(lblCodigoSeguimiento);
            Controls.Add(lblEstado);
            Controls.Add(cboEstado);
            Controls.Add(dateTimePickerHasta);
            Controls.Add(dateTimePickerDesde);
            Controls.Add(btnCancelar);
            Controls.Add(btnAplicar);
            Controls.Add(btnModificarEnvio);
            Controls.Add(btnCancelarEnvio);
            Controls.Add(lblTodosLosEnvios);
            Controls.Add(dataGridViewEnvios);
            Controls.Add(btnSalir);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblProvincia);
            Controls.Add(txtProvincia);
            Controls.Add(lblCP);
            Controls.Add(txtCP);
            Controls.Add(lblCiudad);
            Controls.Add(txtCiudad);
            Controls.Add(lblDireccion);
            Controls.Add(txtDireccion);
            Controls.Add(lblDestino);
            Controls.Add(btnRegistrarEnvio);
            Controls.Add(btnBuscarDestinatario);
            Controls.Add(lblTelefonoD);
            Controls.Add(txtTelefonoD);
            Controls.Add(lblEmailD);
            Controls.Add(txtEmailD);
            Controls.Add(lblApellidoD);
            Controls.Add(txtApellidoD);
            Controls.Add(lblNombreD);
            Controls.Add(txtNombreD);
            Controls.Add(lblDNID);
            Controls.Add(txtDNID);
            Controls.Add(lblDestinatario);
            Controls.Add(btnBuscarRemitente);
            Controls.Add(lblTelefonoR);
            Controls.Add(txtTelefonoR);
            Controls.Add(lblEmailR);
            Controls.Add(txtEmailR);
            Controls.Add(lblApellidoR);
            Controls.Add(txtApellidoR);
            Controls.Add(lblNombreR);
            Controls.Add(txtNombreR);
            Controls.Add(lblDNIR);
            Controls.Add(txtDNIR);
            Controls.Add(lblRemitente);
            Controls.Add(lblLargo);
            Controls.Add(txtLargo);
            Controls.Add(lblAncho);
            Controls.Add(txtAncho);
            Controls.Add(lblAlto);
            Controls.Add(txtAlto);
            Controls.Add(lblPeso);
            Controls.Add(txtPeso);
            Controls.Add(lblDescripcion);
            Controls.Add(lblPaquete);
            Controls.Add(txtDescripcion);
            Name = "GestionEnviosForm";
            Text = "Gestion de Envios";
            Load += GestionEnviosForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnvios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtDescripcion;
        private Label lblPaquete;
        private Label lblDescripcion;
        private Label lblPeso;
        private TextBox txtPeso;
        private Label lblAncho;
        private TextBox txtAncho;
        private Label lblAlto;
        private TextBox txtAlto;
        private Label lblLargo;
        private TextBox txtLargo;
        private Label lblEmailR;
        private TextBox txtEmailR;
        private Label lblApellidoR;
        private TextBox txtApellidoR;
        private Label lblNombreR;
        private TextBox txtNombreR;
        private Label lblDNIR;
        private TextBox txtDNIR;
        private Label lblRemitente;
        private Label lblTelefonoR;
        private TextBox txtTelefonoR;
        private Button btnBuscarRemitente;
        private Button btnBuscarDestinatario;
        private Label lblTelefonoD;
        private TextBox txtTelefonoD;
        private Label lblEmailD;
        private TextBox txtEmailD;
        private Label lblApellidoD;
        private TextBox txtApellidoD;
        private Label lblNombreD;
        private TextBox txtNombreD;
        private Label lblDNID;
        private TextBox txtDNID;
        private Label lblDestinatario;
        private Button btnRegistrarEnvio;
        private Label lblProvincia;
        private TextBox txtProvincia;
        private Label lblCP;
        private TextBox txtCP;
        private Label lblCiudad;
        private TextBox txtCiudad;
        private Label lblDireccion;
        private TextBox txtDireccion;
        private Label lblDestino;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnSalir;
        private DataGridView dataGridViewEnvios;
        private Label lblTodosLosEnvios;
        private Button btnCancelarEnvio;
        private Button btnModificarEnvio;
        private Button btnAplicar;
        private Button btnCancelar;
        private DateTimePicker dateTimePickerDesde;
        private DateTimePicker dateTimePickerHasta;
        private ComboBox cboEstado;
        private Label lblEstado;
        private Label lblCodigoSeguimiento;
        private TextBox txtCodigoSeguimiento;
        private Label label7;
        private Label lblDesde;
        private Label lblHasta;
        private Label lblDNIRFiltro;
        private Label lblDNIDFiltro;
        private TextBox txtDNIRFiltro;
        private TextBox txtDNIDFiltro;
        private Button btnLimpiarFiltros;
    }
}