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
    public partial class FormAltaInasistencias : Form
    {
        private string config = EntidadConfiguracion.configTransaccional;

        private Configuraciones cfg = null;

        private BLProcesarDatos objProcesarDatos = null;

        private List<EntidadAsistencia> listaEntidades = null;

        public FormAltaInasistencias()
        {
            InitializeComponent();

            cfg = new Configuraciones();
            cfg.valor = config;

            objProcesarDatos = new BLProcesarDatos(config);
        }

        private void FormAltaInasistencias_Load(object sender, EventArgs e)
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
            DataGridViewCheckBoxColumn cbcPresente = new DataGridViewCheckBoxColumn();
            cbcPresente.HeaderText = "Presente";
            cbcPresente.FalseValue = "0";
            cbcPresente.TrueValue = "1";

            DataGridViewCheckBoxColumn cbcMediaFalta = new DataGridViewCheckBoxColumn();
            cbcMediaFalta.HeaderText = "Llegada tarde";
            cbcMediaFalta.FalseValue = "0";
            cbcMediaFalta.TrueValue = "1";

            DataGridViewCheckBoxColumn cbcAusente = new DataGridViewCheckBoxColumn();
            cbcAusente.HeaderText = "Ausente";
            cbcAusente.FalseValue = "0";
            cbcAusente.TrueValue = "1";

            DataGridViewColumn cbcAlumno = new DataGridViewColumn();
            cbcAlumno.HeaderText = "Alumno";

            KeyValuePair<int, string> idCursoSeleccionado = (KeyValuePair<int, string>)ddlCursos.SelectedItem;
            int aux = idCursoSeleccionado.Key;

            List<Alumno> listadoAlumnos = objProcesarDatos.obtenerAlumnosCursoCicloLectivoActual(cfg, aux);

            //var dict = new Dictionary<int, string>();         

            listaEntidades = new List<EntidadAsistencia>();

            foreach (Alumno alumno in listadoAlumnos)
            {
                //dict.Add(alumno.idAlumnoTransaccional, alumno.apellido + ", " + alumno.nombre);

                EntidadAsistencia entidad = new EntidadAsistencia();

                entidad.legajo = alumno.idAlumnoTransaccional;
                entidad.alumno = alumno.apellido + ", " + alumno.nombre;
                entidad.presente = true;
                entidad.tarde = false;
                entidad.ausente = false;

                listaEntidades.Add(entidad);
            }

            dgvAlumnos.DataSource = listaEntidades;

            //dgvAlumnos.DataSource = new BindingSource(dict,null);

            //dgvAlumnos.DataMember = "Value";
            //dgvAlumnos.ValueMember = "Key";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegistrarInasistencia_Click(object sender, EventArgs e)
        {
            //TODO: Asegurarse de que todos los alumnos seleccionados son los ausentes (Para eso está el legajo en el formulario)

            List<Asistencia> listaAsistencias = new List<Asistencia>();

            if (ddlCursos.SelectedIndex > 0)
            {
                KeyValuePair<int, string> idCursoSeleccionado = (KeyValuePair<int, string>)ddlCursos.SelectedItem;
                int var_curso = idCursoSeleccionado.Key;

                Asistencia asistencia = null;

                foreach (EntidadAsistencia entidadAsistencia in listaEntidades)
                {
                    asistencia = new Asistencia();

                    int var_tipo_asistencia = 1;
                    int validadorSeleccionUnica = 0;

                    //Verifico que al menos se haya seleccionado una de las opciones
                    if (entidadAsistencia.presente) validadorSeleccionUnica++;
                    if (entidadAsistencia.tarde) validadorSeleccionUnica++;
                    if (entidadAsistencia.ausente) validadorSeleccionUnica++;

                    //Valido que se haya seleccionado una sola opción únicamente
                    if(validadorSeleccionUnica == 1)
                    {
                        if (entidadAsistencia.tarde) var_tipo_asistencia = 5;
                        if (entidadAsistencia.ausente) var_tipo_asistencia = 3;

                        asistencia.fecha = dtpFecha.Value;

                        asistencia.tipoAsistencia.idTipoAsistenciaTransaccional = var_tipo_asistencia;

                        asistencia.alumno.alumno.idAlumnoTransaccional = entidadAsistencia.legajo;
                        asistencia.alumno.cursoCicloLectivo.curso.idCursoTransaccional = var_curso;

                        asistencia.alumno.idAlumnoCursoCicloLectivoTransaccional = entidadAsistencia.legajo;

                        listaAsistencias.Add(asistencia);
                    }
                }

                //Si cada una de las entidades ha sido seleccionada, procedemos a persistir las asistencias del día, sino no se hace nada.
                if (listaAsistencias.Count == listaEntidades.Count)
                {
                    foreach (Asistencia asistenciaEspecifica in listaAsistencias)
                    {
                        objProcesarDatos.registrarAsistenciaBDTransaccional(cfg, asistenciaEspecifica);
                    }
                }
            } //Fin validador selección de curso
            else MessageBox.Show("No se ha seleccionado un curso");
            LimpiarFormulario();
        }

    }
}
