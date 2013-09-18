using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EDUAR_Entities;
using EDUAR_SI_BusinessLogic;

namespace EDUAR_SI_Prueba
{
    public partial class FormAltaSanciones : Form
    {
        private string config = EntidadConfiguracion.configTransaccional;

        private Configuraciones cfg = null;

        private BLProcesarDatos objProcesarDatos = null;

        public FormAltaSanciones()
        {
            InitializeComponent();

            cfg = new Configuraciones();
            cfg.valor = config;

            objProcesarDatos = new BLProcesarDatos(config);
        }

        private void FormAltaSanciones_Load(object sender, EventArgs e)
        {
            var dictCursos = new Dictionary<int, string>();
            var dictMotivoSancion = new Dictionary<int, string>();
            var dictTipoSancion = new Dictionary<int, string>();

            dictCursos.Add(0, "Seleccione curso");

            foreach (Curso curso in objProcesarDatos.obtenerCursosBDTransaccional(cfg))
            {
                dictCursos.Add(curso.idCursoTransaccional, curso.nivel.idNivelTransaccional + "º " + curso.nombre);
            }

            ddlCursos.DataSource = new BindingSource(dictCursos, null);
            ddlCursos.DisplayMember = "Value";
            ddlCursos.ValueMember = "Key";

            dictMotivoSancion.Add(0, "Seleccione motivo");

            foreach (MotivoSancion motivoSancion in objProcesarDatos.obtenerMotivoSancionBDTransaccional(cfg))
            {
                dictMotivoSancion.Add(motivoSancion.idMotivoSancionTransaccional, motivoSancion.descripcion);
            }

            ddlMotivosSancion.DataSource = new BindingSource(dictMotivoSancion, null);
            ddlMotivosSancion.DisplayMember = "Value";
            ddlMotivosSancion.ValueMember = "Key";

            dictTipoSancion.Add(0, "Seleccione tipo");

            foreach (TipoSancion tipoSancion in objProcesarDatos.obtenerTipoSancionBDTransaccional(cfg))
            {
                dictTipoSancion.Add(tipoSancion.idTipoSancionTransaccional, tipoSancion.nombre);
            }

            ddlTiposSancion.DataSource = new BindingSource(dictTipoSancion, null);
            ddlTiposSancion.DisplayMember = "Value";
            ddlTiposSancion.ValueMember = "Key";

            for (int i = 0; i < 16; i++)
            {
                ddlCantidadSanciones.Items.Add(i);
            }
        }

        private void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<int, string> idCursoSeleccionado = (KeyValuePair<int, string>)ddlCursos.SelectedItem;
            int aux = idCursoSeleccionado.Key;

            List<Alumno> listadoAlumnos = objProcesarDatos.obtenerAlumnosCursoCicloLectivoActual(cfg, aux);

            var dict = new Dictionary<int, string>();

            dict.Add(0, "Seleccione alumno");

            foreach (Alumno alumno in listadoAlumnos)
            {
                dict.Add(alumno.idAlumnoTransaccional, alumno.apellido + ", " + alumno.nombre);
            }

            ddlAlumnos.DataSource = new BindingSource(dict, null);
            ddlAlumnos.DisplayMember = "Value";
            ddlAlumnos.ValueMember = "Key";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LimpiarFormulario()
        {
            ddlCursos.SelectedIndex = 0;
            ddlMotivosSancion.SelectedIndex = 0;
            ddlTiposSancion.SelectedIndex = 0;
            ddlCantidadSanciones.SelectedIndex = 0;
        }

        private void btnRegistrarSancion_Click(object sender, EventArgs e)
        {
            if (ddlCantidadSanciones.SelectedIndex > -1 &&
                ddlAlumnos.SelectedIndex > 0 &&
                ddlMotivosSancion.SelectedIndex > 0 &&
                ddlCursos.SelectedIndex > 0 &&
                ddlTiposSancion.SelectedIndex > 0
            )
            {
                //dtpFecha.MinDate
                //dtpFecha.MaxDate
                //string valor_fecha = dtpFecha.Value.ToString("dd/MM/yyyy");

                KeyValuePair<int, string> idCursoSeleccionado = (KeyValuePair<int, string>)ddlCursos.SelectedItem;
                int var_curso = idCursoSeleccionado.Key;

                KeyValuePair<int, string> idAlumnoSeleccionado = (KeyValuePair<int, string>)ddlAlumnos.SelectedItem;
                int var_alumno = idAlumnoSeleccionado.Key;

                KeyValuePair<int, string> idTipoSeleccionado = (KeyValuePair<int, string>)ddlTiposSancion.SelectedItem;
                int var_tipo = idTipoSeleccionado.Key;

                KeyValuePair<int, string> idMotivoSeleccionado = (KeyValuePair<int, string>)ddlMotivosSancion.SelectedItem;
                int var_motivo = idMotivoSeleccionado.Key;

                string var_cantidades = ddlCantidadSanciones.SelectedItem.ToString();

                Sancion unaSancion = new Sancion();

                unaSancion.alumno.alumno.idAlumnoTransaccional = var_alumno;
                unaSancion.alumno.cursoCicloLectivo.curso.idCursoTransaccional = var_curso;
                unaSancion.tipoSancion.idTipoSancionTransaccional = var_tipo;
                unaSancion.motivoSancion.idMotivoSancionTransaccional = var_motivo;
                unaSancion.cantidad = Convert.ToInt32(var_cantidades);
                unaSancion.fecha = dtpFecha.Value;

                objProcesarDatos.registrarSancionBDTransaccional(cfg, unaSancion);

                LimpiarFormulario();
            }
        }
    }
}
