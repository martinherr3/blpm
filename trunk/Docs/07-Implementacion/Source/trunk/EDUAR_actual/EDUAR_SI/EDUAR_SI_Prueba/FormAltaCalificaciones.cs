using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EDUAR_SI_BusinessLogic;
using EDUAR_Entities;

namespace EDUAR_SI_Prueba
{
    public partial class FormAltaCalificaciones : Form
    {
        private string config = EntidadConfiguracion.configTransaccional;
            
        private Configuraciones cfg = null;

        private BLProcesarDatos objProcesarDatos = null;

        private List<EntidadCalificacion> listaEntidades = null;
        
        public FormAltaCalificaciones()
        {
            InitializeComponent();

            cfg = new Configuraciones();
            cfg.valor = config;

            objProcesarDatos = new BLProcesarDatos(config);
        }

        private void FormAltaCalificaciones_Load(object sender, EventArgs e)
        {
            var dictCursos = new Dictionary<int, string>();

            dictCursos.Add(0, "Seleccione curso");

            foreach (Curso curso in objProcesarDatos.obtenerCursosBDTransaccional(cfg))
            {
                dictCursos.Add(curso.idCursoTransaccional, curso.nivel.idNivelTransaccional + "º " + curso.nombre);
            }

            ddlCursos.DataSource = new BindingSource(dictCursos, null);
            ddlCursos.DisplayMember = "Value";
            ddlCursos.ValueMember = "Key";
        }

        private void LimpiarFormulario()
        {
            ddlCursos.SelectedIndex = 0;
        }

        private void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<int, string> idCursoSeleccionado = (KeyValuePair<int, string>)ddlCursos.SelectedItem;
            int aux = idCursoSeleccionado.Key;

            List<Asignatura> listadoAsignaturas = objProcesarDatos.obtenerAsignaturasCursoCicloLectivoActual(cfg, aux);

            var dict = new Dictionary<int, string>();

            dict.Add(0, "Seleccione asignatura");

            foreach (Asignatura asignatura in listadoAsignaturas)
            {
                dict.Add(asignatura.idAsignaturaTransaccional, asignatura.nombre);
            }

            ddlAsignatura.DataSource = new BindingSource(dict, null);
            ddlAsignatura.DisplayMember = "Value";
            ddlAsignatura.ValueMember = "Key";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegistrarCalificacion_Click(object sender, EventArgs e)
        {

        }

        private void ddlAsignatura_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridViewComboBoxColumn columnScores = new DataGridViewComboBoxColumn();
            columnScores.HeaderText = "Score";

            DataTable data = new DataTable();

            data.Columns.Add(new DataColumn("Value", typeof(Int32)));
            data.Columns.Add(new DataColumn("Description", typeof(string)));
            
            for (int i=1;i<11;i++) data.Rows.Add(i,i);
            
            columnScores.DataSource = data;
            columnScores.ValueMember = "Value";
            columnScores.DisplayMember = "Description"; 
            
            KeyValuePair<int, string> idCursoSeleccionado = (KeyValuePair<int, string>)ddlCursos.SelectedItem;
            int auxCurso = idCursoSeleccionado.Key;

            KeyValuePair<int, string> idAsignaturaSeleccionada = (KeyValuePair<int, string>)ddlAsignatura.SelectedItem;
            int auxAsignatura = idAsignaturaSeleccionada.Key;

            List<Alumno> listadoAlumnos = objProcesarDatos.obtenerAlumnosCursoCicloLectivoActual(cfg, auxCurso);
            
            listaEntidades = new List<EntidadCalificacion>();

            foreach (Alumno alumno in listadoAlumnos)
            {
                EntidadCalificacion entidad = new EntidadCalificacion();

                entidad.legajo = alumno.idAlumnoTransaccional;
                entidad.alumno = alumno.apellido + ", " + alumno.nombre;
                //entidad.asignatura = ddlAsignatura.SelectedIndex;
                entidad.calificacion = 0;
                
                listaEntidades.Add(entidad);
            }

            dgvAlumnos.DataSource = listaEntidades;
            
        }

    }
}
